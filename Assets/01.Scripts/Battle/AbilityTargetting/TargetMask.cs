using DG.Tweening;
using ExtensionFunction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TargetMask : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Enemy MarkingEnemy { get; set; }
    [SerializeField] private Image _targetMarkImg;
    private Tween _fadeTween;

    public void ActiveTargetMark(bool isActive)
    {
        _fadeTween.Kill();
        _fadeTween = _targetMarkImg.DOFade(MaestrOffice.BoolToInt(isActive), 0.2f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!BattleReader.AbilityTargetSystem.OnTargetting) return;

        BattleReader.SelectEnemy = MarkingEnemy;
        BattleReader.AbilityTargetSystem.CanBinding = false;
        BattleReader.AbilityTargetSystem.mousePos = transform.localPosition;
        ActiveTargetMark(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //if (!BattleReader.AbilityTargetSystem.OnTargetting) return;

        BattleReader.SelectEnemy = null;
        BattleReader.AbilityTargetSystem.CanBinding = true;
        ActiveTargetMark(false);
    }

    public Image GetTargetMarkImage()
    {
        return _targetMarkImg;
    }
}
