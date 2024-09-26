using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BuffingType
{
    Targetting,
    Healing,
    AtkUp,
    DefUp,
    CriPercentUp,
    CriDamageUp,
    Frozen,
    Lightning,
    Faint,
    AtkDown,
    DefDown,
    Smelting,
    MusicDef,
    MusicAtk,
    MusicFaint
}

public class CombatMarkingData
{
    public BuffingType buffingType;
    public string buffingInfo;
    public int durationTurn;

    public CombatMarkingData(BuffingType buffType, string buffInfo, int duration)
    {
        buffingType = buffType;
        buffingInfo = buffInfo;
        durationTurn = Mathf.Clamp(duration, 1, int.MaxValue);
    }
}

public class BuffingMarkSetter : MonoBehaviour
{
    [SerializeField] private BuffingMark _buffingMarkPrefab;
    [SerializeField] private Sprite[] _buffingMarkTextureArr;
    [SerializeField] private string[] _buffingNameArr;
    [SerializeField] private RectTransform _contentTrm;

    private ExpansionList<BuffingMark> _buffingMarkList = new ();
    private List<CombatMarkingData> _currentMarkingDataList = new();

    public Transform BuffingPanelTrm { get; set; }

    private void Awake()
    {
        _buffingMarkList.ListAdded += HandleContentIncrease;
        _buffingMarkList.ListRemoved += HandleContentDecrease;
    }

    private void HandleContentDecrease(object sender, EventArgs e)
    {
        _contentTrm.sizeDelta -= new Vector2(44, 0);
    }

    private void HandleContentIncrease(object sender, EventArgs e)
    {
        _contentTrm.sizeDelta += new Vector2(44, 0);
    }

    private void Start()
    {
        TurnCounter.EnemyTurnEndEvent += DecountBuffDuration;
    }
    private void OnDestroy()
    {
        TurnCounter.EnemyTurnEndEvent -= DecountBuffDuration;
    }

    public void DecountBuffDuration()
    {
        foreach(var md in _currentMarkingDataList)
        {
            md.durationTurn -= 1;
            if(md.durationTurn <= 0 )
            {
                RemoveBuffingMark(md);
            }
        }
    }

    public void AddBuffingMark(CombatMarkingData markingData, int addCount = 1)
    {
        BuffingMark target = _buffingMarkList.Find(x => x.CombatMarkingData.buffingInfo == markingData.buffingInfo);
        if (target == null)
        {
            BuffingMark buffingMark = Instantiate(_buffingMarkPrefab, _contentTrm);

            int idx = (int)markingData.buffingType;
            buffingMark.SetInfo(_buffingMarkTextureArr[idx],
                                _buffingNameArr[idx], markingData, BuffingPanelTrm);

            _buffingMarkList.Add(buffingMark);

            buffingMark.TokenCount += addCount;
        }
        else
        {
            target.TokenCount += addCount;
        }

        _currentMarkingDataList.Add(markingData);
    }

    public void RemoveBuffingMark(CombatMarkingData markingData, int RemoveCount = 1)
    {
        BuffingMark target = _buffingMarkList.Find(x => x.CombatMarkingData.buffingType == markingData.buffingType);
        if (target == null) return;

        if(target.TokenCount - RemoveCount <= 0) 
        {
            _buffingMarkList.Remove(target);

            Destroy(target.gameObject);
        }
        else
        {
            target.TokenCount = target.TokenCount - RemoveCount;
        }
    }

    public void RemoveSpecificBuffingType(BuffingType buffingType)
    {
        BuffingMark target = _buffingMarkList.Find(x => x.CombatMarkingData.buffingType == buffingType);

        if(target != null)
        {
            _buffingMarkList.Remove(target);

            Destroy(target.gameObject);
        }
    }
}
