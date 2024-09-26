using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using Image= UnityEngine.UI.Image;

public class AccumulateObject : MonoBehaviour
{
    public Vector3 movePos;
    public Image skillImage;

    private Vector3 _fullScale = Vector3.one;
    private Vector3 _startPos = new Vector3(0.0f, 0.0f, 0.0f);

    private RectTransform _currentRectTransform;

    private Sequence _objectTween;

    private void Awake()
    {
        _currentRectTransform = GetComponent<RectTransform>();
        if(_currentRectTransform == null)
        {
            Debug.LogError("RectTransform is not founded");
        }
    }

    public void OpenMenu()
    {
        _objectTween = DOTween.Sequence();

        _objectTween.Append(_currentRectTransform.DOScale(_fullScale, 0.1f).SetEase(Ease.OutExpo));
        _objectTween.Join(_currentRectTransform.DOAnchorPos(movePos, 0.1f).SetEase(Ease.OutExpo));

        _objectTween.OnComplete(() =>
        {
            _objectTween.Kill();
        });
    }

    public void CloseMenu()
    {
        _objectTween = DOTween.Sequence();

        _objectTween.Append(_currentRectTransform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InCirc));
        _objectTween.Join(_currentRectTransform.DOAnchorPos(_startPos,0.1f).SetEase(Ease.InCirc));

        _objectTween.OnComplete(() =>
        {
            _objectTween.Kill();
        });
    }
}
