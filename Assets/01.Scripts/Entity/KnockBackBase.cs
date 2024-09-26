using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KnockBackBase
{
    protected KnockBackSystem _system;
    protected Entity _owner;
    public KnockBackBase(KnockBackSystem system, Entity entity)
    {
        _system = system;
        _owner = entity;
    }
    public abstract void Knockback(int dmg);
    public virtual void ResetPos(Vector3 knockBackBeforePos)
    {
        _owner.transform.DOMove(knockBackBeforePos, 0.2f).SetEase(Ease.OutExpo);
    }
    protected float CalculateKnockBackValue(int dmg)
    {
        float value = 4 * (1f - 1f / Mathf.Sqrt(0.05f * dmg + 1f));
        return value;
    }
}
