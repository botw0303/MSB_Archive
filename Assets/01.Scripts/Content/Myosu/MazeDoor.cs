using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MazeDoor : MonoBehaviour, IPointerEnterHandler, 
                                       IPointerExitHandler,
                                       IPointerClickHandler
{
    public StageDataSO AssignedStageInfo { get; set; }
    public MazeStatSO UpgradeStatInfo { get; set; }

    private CanvasGroup _visual;
    public CanvasGroup Visual
    {
        get
        {
            if(_visual == null)
            {
                _visual = GetComponent<CanvasGroup>();
            }
            return _visual;
        }
    }
    [SerializeField] private Transform _doorTrm;
    [SerializeField] private CompensationBubble _comBubble;

    [SerializeField] private UnityEvent<MazeDoor> _doorHoverEvent;
    [SerializeField] private UnityEvent<MazeDoor> _doorHoverOutEvent;
    [SerializeField] private UnityEvent<MazeDoor> _doorSelectEvent;

    private Vector3 _normalScale;
    private Tween _hoverTween;
    private Tween _shakeTween;

    public bool CanInteractible { get; set; } = true;

    private void Start()
    {
        _normalScale = transform.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!CanInteractible) return;

        _hoverTween.Kill();

        _hoverTween = transform.DOScale(transform.localScale * 1.1f, 0.3f);
        _shakeTween = transform.DOShakeRotation(1f, 3, 10).SetLoops(-1);

        if(AssignedStageInfo != null)
        {
            _comBubble.SpeachUpBubble(AssignedStageInfo.compensation.Item.itemIcon, $"X50");
        }
        else
        {
            _comBubble.SpeachUpBubble(UpgradeStatInfo.icon, $"+{UpgradeStatInfo.addValue}");
        }

        _doorHoverEvent?.Invoke(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!CanInteractible) return;

        _hoverTween.Kill();
        _shakeTween.Kill();

        transform.rotation = Quaternion.identity;
        _hoverTween = transform.DOScale(_normalScale, 0.3f);
        _comBubble.SpeachDownBubble();

        _doorHoverOutEvent?.Invoke(this);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        CanInteractible = false;

        _comBubble.SpeachDownBubble();
        _hoverTween?.Kill();
        _shakeTween?.Kill();
        UIManager.Instance.GetSceneUI<MyosuUI>().HideText();

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(_normalScale * 1.3f, 1));
        seq.Join(transform.DOLocalMoveX(0, 1));
        seq.Join(_doorTrm.DOLocalRotateQuaternion(Quaternion.Euler(0, -90, 0), 1));
        seq.AppendCallback(() => 
        {
            if(AssignedStageInfo != null)
            {
                StageManager.Instanace.SelectStageData = AssignedStageInfo;
                GameManager.Instance.ChangeScene(SceneType.battle);
            }
            else
            {
                AdventureData data = DataManager.Instance.LoadData<AdventureData>(DataKeyList.adventureDataKey);

                switch (UpgradeStatInfo.mazeInUpgradeStat)
                {
                    case MazeInUpgradeStat.Hp:
                        GameManager.Instance.stat.hpAddValue += UpgradeStatInfo.addValue;
                        data.MazeHpAddvalue += UpgradeStatInfo.addValue;
                        break;
                    case MazeInUpgradeStat.Atk:
                        GameManager.Instance.stat.atkAddValue += UpgradeStatInfo.addValue;
                        data.MazeAtkAddValue += UpgradeStatInfo.addValue;
                        break;
                    case MazeInUpgradeStat.Cost:
                        CostCalculator.MaxMoney += UpgradeStatInfo.addValue;
                        data.MazeCostAddValue += UpgradeStatInfo.addValue;
                        break;
                }

                DataManager.Instance.SaveData(data, DataKeyList.adventureDataKey);
                GameManager.Instance.ChangeScene(SceneType.Myosu);
            }
        });

        _doorSelectEvent?.Invoke(this);
    }
}
