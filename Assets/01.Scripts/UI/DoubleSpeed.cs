using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoubleSpeed : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Color[] _colors;
    [SerializeField] private string[] _strings;

    private bool _isDoubleSpeed = false;
    private Image _img;

    private void Awake()
    {
        _img = GetComponent<Image>();
    }

    public void DoubleSpeedAction()
    {
        _isDoubleSpeed = !_isDoubleSpeed;

        if(_isDoubleSpeed )
        {
            _img.color = _colors[1];
            _text.text = _strings[1];
            Time.timeScale = 2;
        }
        else
        {
            _img.color = _colors[0];
            _text.text = _strings[0];
            Time.timeScale = 1;
        }
    }
}
