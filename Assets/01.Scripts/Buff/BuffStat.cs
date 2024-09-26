using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void OnHitDamage<T1, T2>(T1 t1, ref T2 t2);
public delegate void OnHitDamageAfter<T1, T2, T3>(T1 dealer, T2 health, ref T3 damage);

public class BuffStat
{
    private Entity _owner;

    public OnHitDamage<Entity, int> OnHitDamageEvent;
    public OnHitDamageAfter<Entity, Health, int> OnHitDamageAfterEvent;

    public Dictionary<Type, SpecialBuff> specialBuffDic = new();
    private List<BuffSO> _buffDic = new();
    private Dictionary<StackEnum, int> _stackDic = new();



    public BuffStat(Entity entity)
    {
        _owner = entity;
        _buffDic = new();
        specialBuffDic = new();
        foreach (StackEnum t in Enum.GetValues(typeof(StackEnum)))
        {
            _stackDic.Add(t, 0);
        }
        //_owner.BeforeChainingEvent.AddListener(UpdateBuff);
    }
    public void AddBuff(BuffSO so, int durationTurn, int combineLevel = 0)
    {
        BuffSO buff;
        if (_buffDic.Contains(so))
        {
            buff = _buffDic[_buffDic.IndexOf(so)];
            buff.PrependBuff();
            buff.RefreshBuff(combineLevel);
            //_buffDic[so] = durationTurn;
        }
        else
        {
            buff = so.Clone();
            buff.SetOwner(_owner, combineLevel);
            buff.AppendBuff();
            
            _buffDic.Add(buff);
        }
    }
    //public void AddBuff(CardBase card,BuffSO so)
    //{
    //    so.SetOwner(_owner, (int)card.CombineLevel);
    //    if (_buffDic.ContainsKey(so))
    //    {
    //        so.PrependBuff();
    //        so.RefreshBuff((int)card.CombineLevel);
    //        _buffDic[so] = CardShameContainer.G card.CardInfo.cardShameData.cardLevel;
    //    }
    //    else
    //    {
    //        so.AppendBuff();
    //        _buffDic.Add(so, durationTurn);
    //    }
    //}
    #region ����
    public void AddStack(StackEnum type, int cnt)
    {
        _stackDic[type] += cnt;
    }
    public int GetStack(StackEnum type) => _stackDic[type];
    public void RemoveStack(StackEnum type, int cnt)
    {
        _stackDic[type] -= cnt;
    }
    public void ClearStack(StackEnum type) => _stackDic[type] = 0;
    #endregion
    public void ActivateSpecialBuff(SpecialBuff buff)
    {
        if (specialBuffDic.ContainsKey(buff.GetType())) return;
        specialBuffDic.Add(buff.GetType(), buff);
        buff.Init();
        switch (buff)
        {
            case IOnTakeDamage i:
                {
                    //if (!_owner.OnAttack.Contains(i))
                        //_owner.OnAttack.Add(i);
                }
                break;
            case IOnHitDamage i:
                {
                    OnHitDamageEvent += i.HitDamage;
                }
                break;
            case IOnEndSkill i:
                {
                    CardReader.SkillCardManagement.useCardEndEvnet.AddListener(i.EndSkill);
                }
                break;
            case IOnHitDamageAfter i:
                {
                    OnHitDamageAfterEvent += i.HitDamageAfter;
                }
                break;
        }
    }
    public void CompleteBuff(SpecialBuff special)
    {
        if (!specialBuffDic.ContainsKey(special.GetType())) return;

        switch (special)
        {
            case IOnTakeDamage i:
                {
                    //if (_owner.OnAttack.Contains(i))
                        //_owner.OnAttack.Remove(i);
                }
                break;
            case IOnHitDamage i:
                {
                    OnHitDamageEvent -= i.HitDamage;
                }
                break;
            case IOnEndSkill i:
                {
                    CardReader.SkillCardManagement.useCardEndEvnet.RemoveListener(i.EndSkill);
                }
                break;
            case IOnHitDamageAfter i:
                {
                    OnHitDamageAfterEvent -= i.HitDamageAfter;
                }
                break;
        }
        specialBuffDic.Remove(special.GetType());
    }

    public void UpdateBuff()
    {
        foreach (var d in _buffDic)
        {
            d.Update();
        }
    }
    public void ClearStat()
    {
        foreach (var d in _buffDic)
        {
            d.PrependBuff();
        }
        _buffDic.Clear();
        foreach (var d in specialBuffDic.Values.ToList())        
        {
            CompleteBuff(d);
        }
        specialBuffDic.Clear();
        TurnCounter.RoundStartEvent -= UpdateBuff;
    }
    public bool ContainBuff(Type specialBuff)
    {
        return specialBuffDic.ContainsKey(specialBuff);
    }
}
