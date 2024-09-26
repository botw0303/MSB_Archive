using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CostPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CardInfoBattlePanel _cardInfoBattlePanel;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverTweening();
        EnablePanelTween();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OutHoverTweening();
        DisablePanelTween();
    }

    private void OnHoverTweening()
    {
        transform.DOKill();
        transform.DOScale(1.2f, 0.2f);
        transform.DOLocalRotate(new Vector3(0, 0, -10), 0.2f);
    }
    private void OutHoverTweening()
    {
        transform.DOKill();
        transform.DOScale(1f, 0.2f);
        transform.DOLocalRotate(Vector3.zero, 0.2f);
    }

    private void EnablePanelTween()
    {
        _cardInfoBattlePanel = PoolManager.Instance.Pop(PoolingType.CardBattlePanel) as CardInfoBattlePanel;
        RectTransform trm = _cardInfoBattlePanel.transform as RectTransform;
        trm.SetAsFirstSibling();

        trm.SetAsFirstSibling();
        trm.SetParent(transform.parent);
        trm.transform.localPosition = Vector2.zero;

        _cardInfoBattlePanel.SetUp("타르트는 단게 좋아!", 
                                   "당이 전부 떨어지면 싸울 수 없습니다!");
    }

    private void DisablePanelTween()
    {
        _cardInfoBattlePanel.SetDown();
    }
}
