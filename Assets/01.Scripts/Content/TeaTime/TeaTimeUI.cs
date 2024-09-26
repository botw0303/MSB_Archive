using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
using TMPro;
using System.Xml.Serialization;
using System;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public struct DirectorByRankInfo
{
    public CakeRank rank;
    public TimelineAsset director;
}
[System.Serializable]
public struct EatDirectorData
{
    public Image cardImage;
    public TextMeshProUGUI cardName;
}
public class TeaTimeUI : SceneUI
{
    [SerializeField] private EatRange _eatRange;
    public EatRange EatRange => _eatRange;

    [SerializeField] private TeaTimeCreamStand _creamStand;
    public TeaTimeCreamStand TeaTimeCreamStand => _creamStand;

    [SerializeField] private CakeCollocation _cakeCollocation;
    public CakeCollocation CakeCollocation => _cakeCollocation;

    [SerializeField] private GetCard _getCard;
    public GetCard GetCard => _getCard;

    private Dictionary<CakeRank, TimelineAsset> _directors = new();
    [SerializeField]
    private List<DirectorByRankInfo> _directorByRank = new();
    [SerializeField]
    private List<EatDirectorData> _directorData = new();

    [SerializeField]
    private GameObject _tutorialPanel;

    [SerializeField]
    private CakeInventory _cakeInven;
    [SerializeField]
    private PlayableDirector _director = null;
    private void Awake()
    {
        foreach (var item in _directorByRank)
        {
            _directors.Add(item.rank, item.director);
        }
    }

    public void SetCard(int idx, CardInfo cardInfo)
    {
        _directorData[idx].cardImage.sprite = cardInfo.CardVisual;
        _directorData[idx].cardName.text = cardInfo.CardName;

        GetCard.GetCakeInfo(cardInfo);
    }

    public void DirectorStart()
    {

        _director.Play();
    }

    public override void SceneUIStart()
    {
        base.SceneUIStart();

        CheckOnFirst cf = DataManager.Instance.LoadData<CheckOnFirst>(DataKeyList.checkIsFirstPlayGameDataKey);
        if (!cf.isFirstOnTeaTime)
        {
            _tutorialPanel.SetActive(true);
            cf.isFirstOnTeaTime = true;
            DataManager.Instance.SaveData(cf, DataKeyList.checkIsFirstPlayGameDataKey);
        }
        _cakeInven.CreateCakeElement();
    }

    public void SetDirector(CakeRank rank)
    {
        _director.playableAsset = _directors[rank];
    }
}
