using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CakeInventoryElement : MonoBehaviour, IPointerClickHandler
{
    [Header("����ũ ����")]
    [SerializeField] private Image _backgroundImg;
    [SerializeField] private Image _cakeImg;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _countText;
    private CakeCollocation _cakeCollocation;

    [Header("����ũ")]
    [SerializeField] private GameObject _usingMask;

    private bool _isSelectThisCake;
    private ItemDataBreadSO _myCommonBreadData;
    private CakeData _data;
    private CakeInventoryPanel _cakeInvenPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isSelectThisCake) return;

        _isSelectThisCake = true;
        _cakeCollocation.CollocateCake(this, _myCommonBreadData, _data);
        _usingMask.SetActive(true);
        _cakeInvenPanel.FadePanel(false, ()=> _cakeInvenPanel.gameObject.SetActive(false));
    }

    public void SetInfo(ItemDataSO info, 
                        int count,
                        CakeCollocation cakeCollocation,
                        CakeInventoryPanel cakeInvenPanel,
                        CakeData data)
    {
        _myCommonBreadData = info as ItemDataBreadSO;
        _cakeCollocation = cakeCollocation;
        _cakeInvenPanel = cakeInvenPanel;
        _data = data;

        _cakeImg.sprite = info.itemIcon;
        _nameText.text = $"{data.Rank}\n{info.itemName}";
        _countText.text = $"<size=\"35\">X{count}</size>";
    }
    public void SetCount(int count)
    {
        _countText.text = $"<size=\"35\">X{count}</size>";
    }
}
