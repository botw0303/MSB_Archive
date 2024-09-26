using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityTargettingSystem : MonoBehaviour
{
    private bool _canBinding = true;
    public bool CanBinding
    {
        get => _canBinding;
        set
        {
            _canBinding = value;
            if(_getTargetArrowDic.ContainsKey(_selectCard))
            {
                _getTargetArrowDic[_selectCard][0].ActiveArrow(value);
            }
        }
    }
    [SerializeField] private BattleController _battleController;
    private Dictionary<TargetEnemyCount, Func<CardBase, int, IEnumerator>> _targetCountingActionDic = new();

    [Header("���콺 ī�� ���ε�")]
    [SerializeField] private AbilityTargetArrow _targetArrowPrefab;
    private Dictionary<CardBase, List<AbilityTargetArrow>> _getTargetArrowDic = new ();
    private CardBase _selectCard;
    public Vector2 mousePos;
    private bool _isBindingMouseAndCard;

    [Header("�� Ȯ��")]
    [SerializeField] private TargettingMaskCreater _maskCreater;
    [SerializeField] private LayerMask _whatIsEnemy;
    [SerializeField] private Transform _chainImPact;
    [SerializeField] private Color _reactionColor;
    private List<ChainSelectTarget> _chainTargetList = new();

    public bool OnTargetting { get; private set; }

    public void AllChainClear()
    {
        foreach(Transform chain in transform)
        {
            Destroy(chain.gameObject);
        }

        _getTargetArrowDic.Clear();
        _chainTargetList.Clear();
    }
    public void AllGenerateChainPos(bool isGenerate)
    {
        List<CardBase> onActiveZoneList = BattleReader.SkillCardManagement.InCardZoneList;

        foreach (CardBase cb in onActiveZoneList)
        {
            if(_getTargetArrowDic.ContainsKey(cb))
            {
                foreach(AbilityTargetArrow ata in _getTargetArrowDic[cb])
                {
                    ata.IsGenerating = isGenerate;
                }
            }
        }
    }
    
    public void ChainFadeControl(float fadeValue)
    {
        List<CardBase> onActiveZoneList = BattleReader.SkillCardManagement.InCardZoneList;

        foreach (CardBase cb in onActiveZoneList)
        {
            if (_getTargetArrowDic.ContainsKey(cb))
            {
                foreach (AbilityTargetArrow ata in _getTargetArrowDic[cb])
                {
                    ata.SetFade(fadeValue);
                }
            }
        }
    }
    public void FadingAllChainTarget(float fadeValue)
    {
        foreach(ChainSelectTarget cst in _chainTargetList)
        {
            cst.SetFade(fadeValue);
        }
    }
    public void SetMouseAndCardArrowBind(CardBase selectCard)
    {
        selectCard.CanUseThisCard = false;
        _battleController.BackGroundFadeOut();

        EnemyTargetting(selectCard);
    }

    private void Start()
    {
        foreach(TargetEnemyCount tec in Enum.GetValues(typeof(TargetEnemyCount)))
        {
            if(tec == TargetEnemyCount.ALL)
            {
                _targetCountingActionDic.Add(tec, HandleALLEnemyTargetting);
            }
            else if(tec == TargetEnemyCount.ME)
            {
                _targetCountingActionDic.Add(tec, HandleMeTargetting);
            }
            else
            {
                _targetCountingActionDic.Add(tec, HandleCountEnemyTargetting);
            }
        }
    }

    private IEnumerator HandleMeTargetting(CardBase selectCard, int count)
    {
        _selectCard = selectCard;
        AbilityTargetArrow ata = Instantiate(_targetArrowPrefab, transform);
        ata.ActiveArrow(false);
        if (!_getTargetArrowDic.ContainsKey(selectCard))
        {
            List<AbilityTargetArrow> atlist = new();
            _getTargetArrowDic.Add(selectCard, atlist);
        }
        
        _getTargetArrowDic[selectCard].Add(ata);

        ata.transform.position = selectCard.transform.position;

        Vector2 screenPoint = MaestrOffice.GetScreenPosToWorldPos(_battleController.Player.transform.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle
        (UIManager.Instance.CanvasTrm, screenPoint, UIManager.Instance.Canvas.worldCamera, 
        out Vector2 anchoredPosition);

        int idx = _getTargetArrowDic[_selectCard].Count - 1;
        _getTargetArrowDic[_selectCard][idx].ArrowBinding(_selectCard.transform, anchoredPosition);
        _getTargetArrowDic[_selectCard][idx].SetFade(0.5f);

        yield return new WaitForSeconds(0.5f);
        _battleController.BackGroundFadeIn();
        OnTargetting = false;

        
    }
    private IEnumerator HandleCountEnemyTargetting(CardBase selectCard, int count)
    {
        BattleReader.IsOnTargetting = true;

        for (int i = 0; i < count; i++)
        {
            _selectCard = selectCard;
            AbilityTargetArrow ata = Instantiate(_targetArrowPrefab, transform);

            if (!_getTargetArrowDic.ContainsKey(selectCard))
            {
                List<AbilityTargetArrow> atlist = new();
                _getTargetArrowDic.Add(selectCard, atlist);
            }

            _getTargetArrowDic[selectCard].Add(ata);
            _isBindingMouseAndCard = true;

            yield return new WaitUntil(() => ata.IsBindSucess);
        }

        BattleReader.IsOnTargetting = false;

        foreach (var e in _battleController.OnFieldMonsterArr)
        {
            _battleController.maskDisableEvent?.Invoke(e);
        }

        _battleController.BackGroundFadeIn();
        OnTargetting = false;
    }
    private IEnumerator HandleALLEnemyTargetting(CardBase selectCard, int count)
    {
        foreach (Enemy e in _battleController.OnFieldMonsterArr)
        {
            if (e is null) continue;

            _selectCard = selectCard;
            AbilityTargetArrow ata = Instantiate(_targetArrowPrefab, transform);
            ata.ActiveArrow(false);
            if (!_getTargetArrowDic.ContainsKey(selectCard))
            {
                List<AbilityTargetArrow> atlist = new();
                _getTargetArrowDic.Add(selectCard, atlist);
            }

            _getTargetArrowDic[selectCard].Add(ata);

            Vector3 pos = _maskCreater.GetTargetMaskDic[e].transform.localPosition;
            int idx = _getTargetArrowDic[_selectCard].Count - 1;
            _getTargetArrowDic[_selectCard][idx].ArrowBinding(_selectCard.transform, pos);
            _getTargetArrowDic[_selectCard][idx].SetFade(0.5f);

            EnemyMarking(e);

            CombatMarkingData data =
            new CombatMarkingData(BuffingType.Targetting,
            $"[{_selectCard.CardInfo.CardName}] ��ų�� \r\n���õǾ����ϴ�.", 1);

            BattleReader.CombatMarkManagement.AddBuffingData(e, selectCard.CardID, data);
        }

        yield return new WaitForSeconds(0.5f);
        foreach (var e in _battleController.OnFieldMonsterArr)
        {
            _battleController.maskDisableEvent?.Invoke(e);
        }
        _battleController.BackGroundFadeIn();
        OnTargetting = false;
    }

    public void TargettingCancle(int cardID)
    {
        foreach (var e in BattleReader.CombatMarkManagement.GetEntityOnTarget(cardID))
        {
            BattleReader.CombatMarkManagement.RemoveBuffingData(e, cardID, BuffingType.Targetting);
        }
    }

    private void EnemyTargetting(CardBase selectCard)
    {
        TargetEnemyCount tec = (TargetEnemyCount)CardManagingHelper.GetCardShame(selectCard.CardInfo.cardShameData,
                                                               CardShameType.Range,
                                                               (int)selectCard.CombineLevel);

        StartCoroutine(_targetCountingActionDic[tec].Invoke(selectCard, (int)tec));
        OnTargetting = true;

        foreach (var e in _battleController.OnFieldMonsterArr)
        {
            if (e is null) return;

            _battleController.maskEnableEvent?.Invoke(e);
        }
    }

    private void BindMouseAndCardWithArrow()
    {
        if(CanBinding)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle
            (UIManager.Instance.CanvasTrm, Input.mousePosition, Camera.main, out mousePos);
        }

        int idx = _getTargetArrowDic[_selectCard].Count - 1;
        _getTargetArrowDic[_selectCard][idx].ArrowBinding(_selectCard.transform, mousePos);
        _getTargetArrowDic[_selectCard][idx].SetFade(0.5f);
    }
    private void CheckSelectEnemy()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (BattleReader.SelectEnemy != null)
            {
                EnemyMarking(BattleReader.SelectEnemy);

                CombatMarkingData data =
                new CombatMarkingData(BuffingType.Targetting,
                $"[{_selectCard.CardInfo.CardName}] ��ų�� \r\n���õǾ����ϴ�.", 1);

                BattleReader.CombatMarkManagement.
                AddBuffingData(BattleReader.SelectEnemy, _selectCard.CardID, data);
            }
        }
    }
    private void EnemyMarking(Enemy e)
    {
        e.ChainningCardList.Add(_selectCard);
		e.SelectedOnAttack(_selectCard);

		int idx = _getTargetArrowDic[_selectCard].Count - 1;
        Tween t = _getTargetArrowDic[_selectCard][idx].ReChainning(() =>
        {
            Instantiate(_chainImPact, e.transform.position, Quaternion.identity);
            DamageTextManager.Instance.PopupReactionText(e.transform.position + new Vector3(0, 1, 0), _reactionColor, "Connect!");
        }, e);

        _isBindingMouseAndCard = false;
    }
    private void Update()
    {
        if(_isBindingMouseAndCard)
        {
            BindMouseAndCardWithArrow();
            CheckSelectEnemy();
        }
    }
}
