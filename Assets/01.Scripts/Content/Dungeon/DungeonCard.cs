using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DungeonCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Action<DungeonCard> OnHoverEvent;
    public Action<DungeonCard> OnDesecendEvent;
    public Action<DungeonCard> OnClickEvent;

    [SerializeField] private Image _visual;
    public Image Visual
    {
        get
        {
            if (_visual != null) return _visual;
            _visual = GetComponentInChildren<Image>();
            if(_visual == null)
            {
                Debug.LogError("Not ContainVisual");
                return null;
            }
            return _visual;
        }
    }
    public Transform VisualTrm => Visual.transform;

    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private TextMeshProUGUI _costText;

    public bool CanSelect { get; set; }
    public bool OnPointerThisCard { get; private set; }

    public CardInfo CardInfo { get; private set; }
    public string Cost { get; private set; }

    public void SetInfo(CardInfo cardInfo)
    {
        _visual.sprite = cardInfo.CardVisual;
        CardInfo = cardInfo;
    }

    public void SetInfoText(string info, string cardName, string cardCost)
    {
        _nameText.text = cardName;
        _infoText.text = info;
        _costText.text = cardCost;

        Cost = cardCost;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerThisCard = true;
        OnHoverEvent?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerThisCard = false;
        OnDesecendEvent?.Invoke(this);
    }
}
