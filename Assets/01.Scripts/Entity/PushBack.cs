using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBack : KnockBackBase
{
    public PushBack(KnockBackSystem system, Entity entity) : base(system, entity)
    {
    }

    public override void Knockback(int dmg)
    {
        Vector3 knockbackPos = _owner.transform.position;
        knockbackPos.x += (_system.filpX ? -1 : 1) * CalculateKnockBackValue(dmg);
        _owner.transform.DOMove(knockbackPos, 0.025f).SetEase(Ease.OutQuad);
    }
}
