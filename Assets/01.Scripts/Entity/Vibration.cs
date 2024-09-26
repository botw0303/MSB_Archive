using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibration : KnockBackBase
{
    public Vibration(KnockBackSystem system, Entity entity) : base(system, entity)
    {
    }

    public override void Knockback(int dmg)
    {
        Vector3 originPos = _owner.transform.position;
        Sequence seq = DOTween.Sequence();
        for (int i = 0; i < 7; i++)
        {
            seq.Append(_owner.transform.DOMove(originPos + new Vector3(_system.filpX ? .1f : -.1f, 0), 0.05f).SetEase(Ease.OutQuad));
            seq.Append(_owner.transform.DOMove(originPos + new Vector3(_system.filpX ? -.1f : .1f, 0), 0.05f).SetEase(Ease.OutQuad));
        }
        seq.Append(_owner.transform.DOMove(originPos, 0.025f).SetEase(Ease.OutQuad));
    }

}
