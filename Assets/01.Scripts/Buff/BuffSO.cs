using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NormalBuff
{
    public StatType type;
    public List<int> values;
    public int turn;
}
[System.Serializable]
public struct StackBuff
{
    public StackEnum type;
    public List<int> values;
}


[CreateAssetMenu(menuName = "SO/Buff")]
public class BuffSO : ScriptableObject
{
    private Entity _owner;
    private CharacterStat _stat;

    [HideInInspector] public List<NormalBuff> statBuffs = new();
    [HideInInspector] public List<SpecialBuff> specialBuffs = new();
    [HideInInspector] public List<StackBuff> stackBuffs = new();

    private List<NormalBuff> applyBuff = new();
    private int _combineLevel = 0;

    [TextArea]
    public string buffInfo;

    public void SetOwner(Entity owner, int combineLevel)
    {
        _owner = owner;
        _stat = owner.CharStat;
        _combineLevel = combineLevel;
        specialBuffs.ForEach(b => b.SetOwner(owner));
    }

    public void AppendBuff()
    {
        foreach (var b in statBuffs)
        {
            _stat.IncreaseStatBy(b.values[_combineLevel], _stat.GetStatByType(b.type));
        }

        foreach (var b in specialBuffs)
        {
            _owner.BuffStatCompo.ActivateSpecialBuff(b);
        }
    }

    public void RefreshBuff(int combineLevel)
    {
        _combineLevel = combineLevel;
        foreach (var b in statBuffs)
        {
            _stat.IncreaseStatBy(b.values[_combineLevel], _stat.GetStatByType(b.type));
        }

        foreach (var b in specialBuffs)
        {
            b.Refresh(_combineLevel);
        }
    }

    public void Update()
    {
        for (int i = 0; i < applyBuff.Count; i++)
        {
            NormalBuff buff = applyBuff[i];
            if (--buff.turn <= 0)
            {
                _stat.DecreaseStatBy(buff.values[_combineLevel], _stat.GetStatByType(buff.type));
                i--;
                applyBuff.RemoveAt(i);
                continue;
            }
            applyBuff[i] = buff;
        }

        foreach (var b in specialBuffs)
        {
            b.UpdateBuff(_combineLevel);
        }
    }

    public void PrependBuff()
    {
        foreach (var b in applyBuff)
        {
            _stat.DecreaseStatBy(b.values[_combineLevel], _stat.GetStatByType(b.type));
        }
    }
    public BuffSO Clone()
    {
        BuffSO so = Instantiate(this);
        so.specialBuffs = specialBuffs.ConvertAll(b => b.Clone());
        return so;
    }
}
