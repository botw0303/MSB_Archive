using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetCakePanel : MonoBehaviour
{
    [SerializeField] private Image _cakeVisual;
    [SerializeField] private TextMeshProUGUI _cakenameText;
    [SerializeField] private TextMeshProUGUI _cakeRatingText;

    public void SetUp(ItemDataBreadSO cakeData, int Count)
    {
        gameObject.SetActive(true);

        _cakeVisual.sprite = cakeData.itemIcon;
        _cakenameText.text = cakeData.itemName;
        _cakeRatingText.text = GetRatingText(Count);
    }
    private string GetRatingText(int count)
    {
        switch (count)
        {
            case 10:
                return "己傍利牢 力户!";
            case 50:
                return "肯寒茄 力户!!";
            case 150:
                return "傈汲利牢 力户!!!";
            default:
                return "坷幅";
        }
    }
}
