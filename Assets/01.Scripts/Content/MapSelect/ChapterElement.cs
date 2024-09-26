using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class ChapterElement : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool CanTryThisChapter { get; set; }

    [SerializeField] private MapDataSO _chapterData;
    [SerializeField] private UnityEvent<MapDataSO> _loadMapActiveEvent;

    [Header("������")]
    [SerializeField] private TextMeshProUGUI _chapterNameTxt;
    [SerializeField] private TextMeshProUGUI _chapterInfoTxt;
    [SerializeField] private GameObject _lockPanel;

    private void Start()
    {
        _chapterNameTxt.text = _chapterData.chapterName;
        _chapterInfoTxt.text = _chapterData.chapterInfo;
    }

    public void SelectThisChapter()
    {
        StageManager.Instanace.SelectMapData = _chapterData;
        _loadMapActiveEvent?.Invoke(_chapterData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!CanTryThisChapter)
        {
            ErrorText text = PoolManager.Instance.Pop(PoolingType.ErrorText) as ErrorText;
            text.transform.SetParent(UIManager.Instance.CanvasTrm);
            text.Erroring("�غ� �� �Դϴ�!");

            return;
        }

        SelectThisChapter();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!CanTryThisChapter) return;

        transform.DOKill();
        transform.DOScale(Vector3.one * 1.05f, 0.2f).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!CanTryThisChapter) return;

        transform.DOKill();
        transform.DOScale(Vector3.one, 0.2f);
    }
}
