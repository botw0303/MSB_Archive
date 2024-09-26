using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CardDefine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
using UnityEngine.Events;

public abstract class CardBase : MonoBehaviour,
                                 IPointerClickHandler,
                                 IPointerEnterHandler,
                                 IPointerExitHandler
{
    public int CardID { get; set; }
    public List<CardRecord> CardRecordList { get; set; } = new();
    public Action<CardBase> RecoverEvent { get; set; }
    [SerializeField] private float _toMovePosInSec;
    public RectTransform VisualRectTrm { get; private set; }
    public CardInfo CardInfo => _myCardInfo;
    [SerializeField] private CardInfo _myCardInfo;
    public bool CanUseThisCard { get; set; } = false;
    public bool IsOnActivationZone { get; set; }
    [SerializeField] private CombineLevel _combineLevel;
    public CombineLevel CombineLevel
    {
        get
        {
            return _combineLevel;
        }
        set
        {
            if (value == _combineLevel) return;

            _combineLevel = value;
            Material mat = new Material(_cardMat);

            switch (_combineLevel)
            {
                case CombineLevel.I:
                    mat.SetColor("_sub_color", BattleReader.CombineMaster.Level_1_Color);
                    break;
                case CombineLevel.II:
                    mat.SetColor("_sub_color", BattleReader.CombineMaster.Level_2_Color);
                    break;
                case CombineLevel.III:
                    mat.SetColor("_sub_color", BattleReader.CombineMaster.Level_3_Color);
                    break;
            }

            VisualTrm.GetComponent<Image>().material = mat;
        }
    }
    [SerializeField] private Transform visualTrm;
    public Transform VisualTrm
    {
        get
        {
            return visualTrm;
        }

    }
    private bool _isActivingAbillity;
    public bool IsActivingAbillity
    {
        get
        {
            return _isActivingAbillity;
        }
        set
        {
            _isActivingAbillity = value;

            if (_isActivingAbillity)
            {
                BattleReader.LockHandCard(true);
            }
            else
            {
				ExitThisCard();
			}
        }
    }
    [SerializeField] private Material _cardMat;
    public int AbilityCost => CardManagingHelper.GetCardShame(CardInfo.cardShameData,
                                                                  CardShameType.Cost,
                                                                  (int)CombineLevel);

    [HideInInspector] public BattleController battleController;
    protected Player Player => battleController.Player;

    [SerializeField] protected BuffSO buffSO;
    [SerializeField] protected SEList<SEList<int>> damageArr;

    private TextMeshProUGUI _costText;

    protected List<Entity> targets = new();

    protected Color minimumColor = new Color(255, 255, 255, .1f);
    protected Color maxtimumColor = new Color(255, 255, 255, 1.0f);

    public Action<Transform> OnPointerSetCardAction { get; set; }
    public Action<Transform> OnPointerInitCardAction { get; set; }

    public float CardIdlingAddValue { get; set; }
    public bool OnPointerInCard { get; set; }

    [SerializeField]
    protected AudioClip _soundEffect;

    [SerializeField]
    protected List<float> _skillDurations;
    private CardInfoBattlePanel _cardInfoBattlePanel;
    public bool Paneling { get; private set; }

    public void SetInfo(int cID, CombineLevel cLv)
    {
        CardID = cID;
        CombineLevel = cLv;
    }

    private void Awake()
    {
        VisualRectTrm = VisualTrm.GetComponent<RectTransform>();
        _costText = transform.Find("Visual/CsotText").GetComponent<TextMeshProUGUI>();

        _costText.text = AbilityCost.ToString();
    }

    private void OnDestroy()
    {
        OnPointerSetCardAction = null;
        BattleReader.CardProductionMaster.QuitCardling(this);
    }

    public abstract void Abillity();

    public void ActiveInfo()
    {
        BattleReader.SkillCardManagement.SetCardInfo(CardInfo, true);
        VisualRectTrm.DOScale(1.3f, 0.2f);

        Vector2 pos = transform.localPosition;
        transform.DOLocalMove(new Vector2(pos.x - 50, pos.y + 40), 0.3f);
    }
    private void ExitThisCard()
    {
        Image img = VisualRectTrm.GetComponent<Image>();
        Material mat = new Material(_cardMat);
        img.material = mat;

        Sequence seq = DOTween.Sequence();
        seq.Append(DOTween.To(() => mat.GetFloat("_dissolve_amount"), d => mat.SetFloat("_dissolve_amount", d), -0.1f, 2f));
        seq.InsertCallback(1, () =>
        {
            BattleReader.SkillCardManagement.SetCardInfo(CardInfo, false);
            //BattleReader.SkillCardManagement.ChainingSkill();
            BattleReader.LockHandCard(false);

            Destroy(gameObject);
        });
    }
    public void SetUpCard(float moveToXPos, bool generateCallback)
    {
        CanUseThisCard = false;
        Vector2 movePos = new Vector2(moveToXPos, -400);

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMove(movePos, _toMovePosInSec).SetEase(Ease.OutBack));
        seq.Join(transform.DOLocalRotateQuaternion(Quaternion.identity, _toMovePosInSec).SetEase(Ease.OutBack));
        seq.Join(transform.DOScale(1, _toMovePosInSec).SetEase(Ease.OutBack));
        seq.AppendCallback(() =>
        {
            if (generateCallback)
            {
                BattleReader.CombineMaster.CombineGenerate();
                BattleReader.OnPointerCard = null;
            }
            CanUseThisCard = true;
        });
    }
    public bool CheckCanCombine(out CardBase frontCard)
    {
        if (BattleReader.GetIdx(this) != 0)
        {
            CardBase frontOfThisCard = BattleReader.GetCardinfoInHand(BattleReader.GetIdx(this) - 1);
            if (frontOfThisCard.CardInfo.CardName == _myCardInfo.CardName &&
                frontOfThisCard.CombineLevel == CombineLevel &&
                frontOfThisCard.CombineLevel != CombineLevel.III)
            {
                frontCard = frontOfThisCard;
                return true;
            }
            else
            {
                frontCard = null;
                return false;
            }
        }
        else
        {
            frontCard = null;
            return false;
        }
    }
    private void Shuffling()
    {
        BattleReader.ShuffleInHandCard(BattleReader.OnPointerCard, this);
        SetUpCard(BattleReader.GetHandPos(this), false);
    }
    private void Update()
    {
        if (!CanUseThisCard) return;

        if (BattleReader.OnPointerCard == null ||
            BattleReader.OnPointerCard == this ||
            BattleReader.OnBinding == false) return;

        if (UIFunction.IsImagesOverlapping(BattleReader.OnPointerCard.VisualRectTrm, VisualRectTrm))
        {
            if (BattleReader.OnPointerCard.transform.position.x > transform.position.x
            && BattleReader.GetIdx(BattleReader.OnPointerCard) > BattleReader.GetIdx(this))
            {
                Shuffling();
            }
            else if (BattleReader.OnPointerCard.transform.position.x < transform.position.x
                 && BattleReader.GetIdx(BattleReader.OnPointerCard) < BattleReader.GetIdx(this))
            {
                Shuffling();
            }
        }
    }
    public int GetDamage(CombineLevel level)
    {
        return (int)(((Player.CharStat.GetDamage() + Player.CharStat.atkAddValue) / 100f) * CardManagingHelper.GetCardShame(CardInfo.cardShameData, CardShameType.Damage, (int)level));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsOnActivationZone || BattleReader.OnBinding ||
            BattleReader.IsOnTargetting) return;

        OnPointerSetCardAction?.Invoke(transform);
        OnPointerInCard = true;

        _cardInfoBattlePanel = PoolManager.Instance.Pop(PoolingType.CardBattlePanel) as CardInfoBattlePanel;
        RectTransform trm = _cardInfoBattlePanel.transform as RectTransform;
        trm.SetAsFirstSibling();

        trm.SetAsFirstSibling();
        trm.SetParent(transform);
        trm.transform.localPosition = Vector2.zero;

        Paneling = true;
        _cardInfoBattlePanel.SetUp(CardInfo.CardName, CardInfo.AbillityInfo);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsOnActivationZone || BattleReader.OnBinding ||
            BattleReader.IsOnTargetting) return;

        OnPointerInitCardAction?.Invoke(transform);
        OnPointerInCard = false;

        BattlePanelDown();
    }

    public void BattlePanelDown()
    {
        Paneling = false;
        _cardInfoBattlePanel.SetDown();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsOnActivationZone || BattleReader.IsOnTargetting) return;

        var initList = BattleReader.SkillCardManagement.InCardZoneList as ExpansionList<CardBase>;

        CardBase card = initList[initList.Count - 1];
        if (card != this)
        {
            // Something;
            //initList.Remove(this);
            return;
        }
        BattleController.Instance.turnSeq[TurnType.Player].Remove(CardReader.SkillCardManagement);
        RecoverEvent?.Invoke(this);
        initList.Remove(this);
    }

    public int[] GetDamages()
    {
        int[] damages = new int[3];
        for (int i = 0; i < 3; ++i)
        {
            damages[i] = (Player.CharStat.GetDamage() / 100) * CardManagingHelper.GetCardShame(CardInfo.cardShameData, CardShameType.Damage, i);
        }
        return damages;
    }
}
