using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pianissimo : MonoBehaviour
{
    public Transform target;
    private float _speed = 0.0f;
    private Vector3 _dir;

    public bool isTriggered = false;

    private void Update()
    {
        if (!isTriggered)
            transform.position += _dir * _speed;
    }

    public void Ready()
    {
        _dir = (target.position - transform.position).normalized;

        float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalRotateQuaternion(Quaternion.Euler(0, angle+45, 0), 1f));
        seq.AppendCallback(() =>
        {
            Attack();
        });
    }

    private void Attack()
    {
        _speed = 1f;
    }
}
