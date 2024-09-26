using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckRemainLoadStone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Transform _panelTrm;
    [SerializeField] private TextMeshProUGUI _panelText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _panelText.text = $"X{UIManager.Instance.GetSceneUI<CardManagingUI>().LoadStoneCount} ³²À½";

        _panelTrm.DOKill();
        _panelTrm.DOScale(1, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _panelTrm.DOKill();
        _panelTrm.DOScale(0, 0.1f);
    }
}
