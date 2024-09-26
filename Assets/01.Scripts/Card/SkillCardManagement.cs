using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillCardManagement : CardManagement,ITurnAction
{
    [SerializeField] private TargettingMaskCreater _maskCreater;
    private ExpansionList<CardBase> InCardZoneCatalogue = new ExpansionList<CardBase>();
    public List<CardBase> InCardZoneList => InCardZoneCatalogue;

    [Header("����� ���ð�")]
    [SerializeField] private Transform _cardWaitZone;
    [SerializeField] private float _waitTurmValue = 85f;
    [SerializeField] private Transform _cardInfoTrm;
    private CardInfoPanel _cardInfoPanel;

    [Header("�ߵ��� ���ð�")]
    [SerializeField] private Transform _activationCardZone;
    [SerializeField] private Vector2 _lastCardPos;
    [SerializeField] private float _activationTurmValue = 155f;

    [Header("�̺�Ʈ")]
    private bool _isInChaining;
    public UnityEvent useCardEndEvnet;
    public UnityEvent beforeChainingEvent;
    [SerializeField] private UnityEvent _afterChanningEvent;
    [SerializeField] private UnityEvent<bool> _acceptBtnSwitchEvent;
    [SerializeField] private UnityEvent<bool> _InverseAcceptBtnSwitchEvent;
    [SerializeField] private UnityEvent _checkStageClearEvent;
    [SerializeField] private UnityEvent<bool> _setupHandCardEvent;

    private CardBase _activeCard = null;

    private void Start()
    {
        InCardZoneCatalogue.ListAdded += HandleCheckAcceptBtn;

        TurnCounter.EnemyTurnEndEvent += () => _checkStageClearEvent?.Invoke();
        TurnCounter.PlayerTurnEndEvent += () => _checkStageClearEvent?.Invoke();

        BattleController.Instance.turnSeq[TurnType.Player].OnSequenceEnd += EndChainningCard;
    }
    private void HandleCheckAcceptBtn(object sender, EventArgs e)
    {
        _acceptBtnSwitchEvent?.Invoke(InCardZoneCatalogue.Count != 0);
        _InverseAcceptBtnSwitchEvent?.Invoke(InCardZoneCatalogue.Count == 0);
    }
    public void SetupCardsInActivationZone()
    {
        BattleReader.AbilityTargetSystem.ChainFadeControl(0);
        BattleReader.AbilityTargetSystem.FadingAllChainTarget(0);

        _setupHandCardEvent?.Invoke(false);
        _acceptBtnSwitchEvent?.Invoke(false);
        int maxCount = InCardZoneCatalogue.Count;

        for (int i = 0; i < maxCount; i++)
        {
            float x = _lastCardPos.x - (_activationTurmValue * (maxCount - i - 1));
            Vector2 targetPos = new Vector2(x, _lastCardPos.y);
            Transform selectTrm = InCardZoneCatalogue[i].transform;

            selectTrm.SetParent(_activationCardZone);

            Sequence seq = DOTween.Sequence();
            seq.Append(selectTrm.DOLocalRotate(new Vector3(0, 0, 10), 0.1f));
            seq.Append(selectTrm.DOLocalMove(targetPos, 0.5f).SetEase(Ease.InOutBack));
            seq.Join(selectTrm.DOLocalRotate(Vector3.zero, 0.5f));

            if (i == maxCount - 1)
            {
                seq.InsertCallback(1, () => 
                {
					BattleController.Instance.StartTurnSequence(TurnType.Player,0f, 2f);
					//ChainingSkill();
				});
            }
        }
    }

    public void ChainingSkill()
    {
        if (_isInChaining)
            useCardEndEvnet?.Invoke();
        DamageTextManager.Instance.PushAllText();
        if (!_isInChaining && InCardZoneCatalogue.Count != 0)
        {
            beforeChainingEvent?.Invoke();
            _isInChaining = true;
        }

        CardBase selectCard = InCardZoneCatalogue[0];
        InCardZoneCatalogue.Remove(selectCard);

        _activeCard = selectCard;

        selectCard.ActiveInfo();
        UseAbility(selectCard);
    }
    private void EndChainningCard()
	{
        useCardEndEvnet?.Invoke();

        _afterChanningEvent?.Invoke();
        _activeCard = null;
        _isInChaining = false;

        foreach (Transform t in _activationCardZone)
        {
            Destroy(t.gameObject);
        }

        //_setupHandCardEvent?.Invoke(true);

        BattleReader.AbilityTargetSystem.AllChainClear();
    }
    public override void UseAbility(CardBase selectCard)
    {
        selectCard.battleController.CameraController.
        StartCameraSequnce(selectCard.CardInfo.cameraSequenceData);

        selectCard.Abillity();
    }

    public void SetSkillCardInCardZone(CardBase selectCard)
    {
        selectCard.CanUseThisCard = false;

        selectCard.transform.SetParent(_cardWaitZone);

        BattleReader.RemoveCardInHand(BattleReader.OnPointerCard);
		InCardZoneCatalogue.Add(selectCard);
		selectCard.IsOnActivationZone = true;

        selectCard.transform.DOScale(1.1f, 0.3f);
        
        GenerateCardPosition(selectCard);
        BattleReader.CombineMaster.CombineGenerate();
        BattleReader.CaptureHand();

        BattleController.Instance.AddFirstAction(TurnType.Player, this);
    }

    public void SetSkillCardInHandZone()
    {
        for (int i = 0; i < InCardZoneCatalogue.Count - 1; i++)
        {
            Transform selectTrm = InCardZoneCatalogue[i].transform;
            selectTrm.DOLocalMove(new Vector2(selectTrm.localPosition.x - 100f, 150), 0.3f);
        }

        BattleReader.CombineMaster.CombineGenerate();
        BattleReader.CaptureHand();
    }

    public void GenerateCardPosition(CardBase selectCard)
    {
        selectCard.transform.rotation = Quaternion.identity;
        BattleReader.AbilityTargetSystem.AllGenerateChainPos(true);
        Sequence seq = DOTween.Sequence();

        int maxIdx = InCardZoneCatalogue.Count - 1;
        if (!(maxIdx <= 0))
        {
            seq.Append(selectCard.transform.
            DOLocalMove(new Vector2(InCardZoneCatalogue[maxIdx - 1].transform.localPosition.x
                                    + 100, 320), 0.3f));
        }
        else if(maxIdx >= 0)
        {
            seq.Append(selectCard.transform.DOLocalMove(new Vector3(0, 320, 0), 0.3f));
        }

        seq.Join(selectCard.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.3f));

        for (int i = 0; i < maxIdx; i++)
        {
            Transform selectTrm = InCardZoneCatalogue[i].transform;
            seq.Join(selectTrm.DOLocalMove(new Vector2(selectTrm.localPosition.x - 100f, 320), 0.3f));
        }
        seq.AppendCallback(() => 
        {
            BattleReader.AbilityTargetSystem.SetMouseAndCardArrowBind(BattleReader.OnPointerCard);
            BattleReader.AbilityTargetSystem.AllGenerateChainPos(false);
        });
    }

    public void SetCardInfo(CardInfo info, bool isSet)
    {
        if (isSet)
        {
            _cardInfoPanel = PoolManager.Instance.Pop(PoolingType.CardInfoPanel) as CardInfoPanel;
            _cardInfoPanel.SetInfo(info, _cardInfoTrm);
        }
        else
        {
            _cardInfoPanel.UnSetInfo();
        }

    }

	public IEnumerator Execute()
	{
        ChainingSkill();
        yield return new WaitUntil(() => !_activeCard.IsActivingAbillity);

	}

	public bool CanUse()
	{
        return !BattleController.Instance.Player.HealthCompo.IsDead;
	}
}
