using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct TutorialPanel
{
    public Sprite img;                          // Panel's image
    [TextArea]
    public string desc;                         // Panel's description
}

public abstract class TutorialBase : MonoBehaviour
{
    [SerializeField]
    protected Image _curPanelImg;               // Currently displayed image
    [SerializeField]
    protected TextMeshProUGUI _curPanelDesc;    // Currently displayed description

    [SerializeField]
    protected List<TutorialPanel> _panelList;   // All images & descriptions of tutorial

    protected int _panelIdx = 0;                // Index of currently displayed image & description

    // Increase/Decrease index
    public void FlipPageNext() => _panelIdx = Mathf.Clamp(_panelIdx + 1, 0, _panelList.Count - 1);
    public void FlipPageBefore() => _panelIdx = Mathf.Clamp(_panelIdx - 1, 0, _panelList.Count - 1);

    public void OnEnable()
    {
        _panelIdx = 0;
        UpdatePanel();
    }

    // Change displayed image & description
    public void UpdatePanel()
    {
        try
        {
            int idx = _panelIdx % _panelList.Count;

            if (!(_panelList[idx].img == null
              || _panelList[idx].desc == null))
            {
                _curPanelImg.sprite = _panelList[idx].img;
                _curPanelDesc.text = _panelList[idx].desc;
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}