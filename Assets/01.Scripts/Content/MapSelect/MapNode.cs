using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MapNode : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool _isStageBubbleReverse;
    [SerializeField] private StageDataSO _stageData;

    private int _stageNumber;
    public int StageNumber
    {
        get
        {
            return _stageNumber;
        }
        set
        {
            _stageNumber = value;
            StageManager.Instanace.SelectStageNumber = value;
        }
    }

    private void Start()
    {
        float upperY = transform.localPosition.y + 10;
        transform.DOLocalMoveY(upperY, 1f).SetLoops(-1, LoopType.Yoyo);

        _stageData = Instantiate(_stageData);
        _stageData.Clone();
    }

    public void ClickThisNode()
    {
        StageManager.Instanace.CreateStageInfoBubble(_stageData.stageName, 
                                                     _stageData.stageNumber);

        StageManager.Instanace.SelectStageData = _stageData;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ClickThisNode();
    }
}

