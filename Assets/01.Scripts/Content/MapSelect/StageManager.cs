using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChapterDefine;
using DG.Tweening;
using System.Linq;
using System;

public class StageManager : MonoBehaviour
{
    private static StageManager _instance;
    public static StageManager Instanace
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType<StageManager>();
            if (_instance == null)
            {
                Debug.LogError("Not Exist StageManager");
            }
            return _instance;
        }
    }

    public MapDataSO SelectMapData { get; set; }
    public StageDataSO SelectStageData { get; set; }
    public List<CardBase> SelectDeck { get; set; }
    [SerializeField] private StageBubble _stageBubblePrefab;
    private StageBubble _stageBubbleObject;

    public bool isOnPanel;
    public bool isOnLoadMap;

    public int SelectStageNumber { get; set; }
    public GameObject LoadMapObject { get; set; }

    public void CreateStageInfoBubble(string stageName, string chapterName)
    {
        if (_stageBubbleObject != null)
        {
            Destroy(_stageBubbleObject.gameObject);
        }

        Transform parent = LoadMapObject.transform.Find("BubbleParent");
        _stageBubbleObject = Instantiate(_stageBubblePrefab, parent);

        _stageBubbleObject.SetInfo(stageName, chapterName);
    }

}
