using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThrowKiwiController : ThrowController
{
    public override void Init()
    {
    }
    public override void Throw(Enemy kiwi, Entity target, Action OnEnd = null)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(target.transform.position, flyTime));
        seq.AppendCallback(() => target.HealthCompo.ApplyDamage(kiwi.CharStat.GetDamage(), kiwi));
        seq.Append(transform.DOJump(kiwi.transform.position, 2, 1, flyTime));
        seq.AppendCallback(OnEnd.Invoke);
    }
}
