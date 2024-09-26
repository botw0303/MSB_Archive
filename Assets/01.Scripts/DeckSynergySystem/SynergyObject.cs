using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SynergyObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameTxt;
    [SerializeField] private TextMeshProUGUI _descTxt;

    public void SetName(string str)
    {
        _nameTxt.text = str;
    }

    public void SetDesc(string str)
    {
        _descTxt.text = str;
    }
}
