using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectPanelLock : MonoBehaviour
{
    [SerializeField] private Image _lockPanel;
    [SerializeField] private GameObject _doNotEnterTxt;

    private void Awake()
    {
        _lockPanel = GetComponent<Image>();
        _doNotEnterTxt = transform.Find("LockText").gameObject;
    }

    public void UnLockStageWithProduction()
    {
        _doNotEnterTxt.SetActive(false);

        Sequence panelUnLockSeq = DOTween.Sequence();
        panelUnLockSeq.Append(_lockPanel.DOFade(0, 0.4f));
        panelUnLockSeq.AppendCallback(()=> _lockPanel.gameObject.SetActive(false));
    }

    public void UnLockStageWithOutProduction()
    {
        _lockPanel.gameObject.SetActive(false);
    }
}
