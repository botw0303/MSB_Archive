using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSkill : MonoBehaviour
{
    [SerializeField] private RectTransform _systemTrm;
    [SerializeField] private Vector2 _downPos;
    [SerializeField] private Vector2 _defaultPos;

    public void SystemDown(bool du)
    {
        _systemTrm.DOAnchorPos(_downPos, 0.5f);
    }

    public void SystemUp()
    {
        _systemTrm.DOAnchorPos(_defaultPos, 0.5f);
    }
}
