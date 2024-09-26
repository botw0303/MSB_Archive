using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThrowMelon : ThrowController
{
    private SpriteRenderer spriteRenderer;
    private float rotateValue;

    [SerializeField] private float rotateSpeed = 1f;

    private void Awake()
    {
        spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
    }
    public override void Init()
    {
        Color c = spriteRenderer.color;
        c.a = 1;
        spriteRenderer.color = c;
        transform.rotation = Quaternion.identity;
    }
    private void Update()
    {
        rotateValue += Time.deltaTime * rotateSpeed;
        transform.Rotate(transform.forward, rotateValue);
    }
    public override void Throw(Enemy enemy, Entity target, Action callback = null)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOJump(target.transform.position, 1, 1, flyTime));
        seq.AppendCallback(() => target.HealthCompo.ApplyDamage(enemy.CharStat.GetDamage(), enemy));
        seq.Append(transform.DOJump(target.transform.position + new Vector3(-3, 2), 1, 1, 0.2f));
        seq.Join(spriteRenderer.DOFade(0, 0.2f));
        seq.AppendCallback(() => { callback?.Invoke(); });
    }
}
