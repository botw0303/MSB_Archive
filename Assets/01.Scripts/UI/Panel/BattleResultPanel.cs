using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleResultPanel : PanelUI
{
    [Header("��Ʋ ����Ʈ �г�")]
    [SerializeField] private BattleController _battleController;

    [Header("����")]
    [SerializeField] private TextMeshProUGUI _clearText;
    [SerializeField] private Transform _clearStamp;
    [SerializeField] private Transform _defeatRain;

    [Header("�������� ����")]
    [SerializeField] private Sprite[] _iconSpriteArr;
    [SerializeField] private Transform _stagePanelTrm;
    [SerializeField] private Image _stageIconImage;
    [SerializeField] private TextMeshProUGUI _stageNameText;

    [Header("Ŭ���� �����")]
    [SerializeField] private Transform _conditionPanel;
    [SerializeField] private Image _conditionResultIcon;
    [SerializeField] private TextMeshProUGUI _conditionText;

    [Header("���� ������")]
    [SerializeField] private Transform _itemScrollTrm;
    [SerializeField] private BattleResultProfilePanel _itemProfile;
    [SerializeField] private Transform _itemProfileTrm;

    [SerializeField] private UnityEvent _clearEvent;
    [SerializeField] private UnityEvent _defaetEvent;

    public void LookResult(bool isClear,
                           StageType stageType,
                           string stageName,
                           string conditionName)
    {
        MaestrOffice.Camera.orthographic = true;
        MaestrOffice.EffectCamera.orthographic = true;

        if (!isClear)
        {
            _clearText.text = "스테이지 패배..";
        }
        else
        {
            StageManager.Instanace.SelectStageData.StageClear();
        }

        _stageIconImage.sprite = _iconSpriteArr[(int)stageType];
        _stageNameText.text = stageName;
        _conditionText.text = conditionName;

        Sequence seq = DOTween.Sequence();
        seq.Append(_clearText.transform.DOLocalMoveX(730, 0.4f).SetEase(Ease.OutBack));
        seq.Insert(0.15f, _stagePanelTrm.DOLocalMoveX(540, 0.4f).SetEase(Ease.OutBack));
        seq.Insert(0.25f, _conditionPanel.DOLocalMoveX(535, 0.4f).SetEase(Ease.OutBack));
        seq.Insert(0.35f, _itemScrollTrm.DOLocalMoveX(500, 0.4f).SetEase(Ease.OutBack));

        if (isClear)
        {
            seq.AppendCallback(() => _clearStamp.gameObject.SetActive(true));
            seq.Join(_clearStamp.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, -14), 0.4f));
            seq.Join(_clearStamp.DOScale(Vector3.one, 0.4f).SetEase(Ease.InBack));

            seq.Join(_conditionResultIcon.DOColor(isClear ? new Color(0f, 1f, 0f, 221f / 255f) : new Color(1f, 1f, 1f, 221f / 255f), 0.15f).SetEase(Ease.InBack));
        }
        else
        {

        }

        seq.AppendCallback(() =>
        {
            foreach (Enemy e in _battleController.DeathEnemyList)
            {
                EnemyStat es = e.CharStat as EnemyStat;
                BattleResultProfilePanel bp = Instantiate(_itemProfile, _itemProfileTrm);
                bp.SetProfile(es.DropItem.itemIcon);
            }

            _battleController.DeathEnemyList.Clear();
        });

        if (isClear)
        {
            seq.AppendCallback(() => _clearEvent?.Invoke());
        }
        else
        {
            seq.AppendCallback(() => _defaetEvent?.Invoke());
        }
    }

    public void GotoPoolAllEnemy()
    {
        foreach (var e in _battleController.OnFieldMonsterArr)
        {
            if (e != null)
                PoolManager.Instance.Push(e);
        }
    }
}
