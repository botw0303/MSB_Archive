using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardDefine;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

public class ActivationChecker : MonoBehaviour
{
    [SerializeField] private CanvasGroup _dragFilter;
    [SerializeField] private BattleController _battleController;

    private int _selectIDX;
    
    private void Update()
    {
        if (_battleController.IsGameEnd) return;

        CheckActivation();

        if (!IsPointerOnCard()) return;
        BindMouse();
    }

    private void BindMouse()
    {
        if(BattleReader.IsOnTargetting) return;

        if (Input.GetMouseButton(0) && BattleReader.OnBinding && BattleReader.OnPointerCard.CanUseThisCard)
        {
            if(BattleReader.OnPointerCard.Paneling)
            {
                BattleReader.OnPointerCard.BattlePanelDown();
            }

            // 카드 Transform = 선택한 카드의 Transform
            Transform cardTrm = BattleReader.OnPointerCard.transform;

            // 마우스 위치값 받기. Canvas의 Rendermode가 Camera기 때문에
            // RectTransformUtility를 활용한다.
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle
            (UIManager.Instance.CanvasTrm, Input.mousePosition, Camera.main, out pos);

            Vector3 mousePos = pos;

            // 마우스와 카드의 x값의 차를 구함 
            float distance = (mousePos - cardTrm.localPosition).x;
            // 구한 거리의 차를 이용하여 기울일 각도의 Z값을 구함
            float rotation = Mathf.Clamp(distance, -20, 20);

            // 카드의 기울일 각도를 구함
            Vector3 euler = cardTrm.localEulerAngles;
            euler.z = rotation;

            mousePos.z = 0;
            // 구한 기울임을 적용함
            // 딱딱한 움직임을 주지 않기 위헤서 Lerp를 사용함
            cardTrm.localEulerAngles = Vector3.Lerp(cardTrm.localEulerAngles, euler, Time.time);
            // 카드의 위치를 마우스 위치로 이동시킴. 마찬가지로 딱딱한 움직임을 주지 않기 위해 Lerp사용
            cardTrm.localPosition = Vector3.Lerp(cardTrm.localPosition, mousePos, Time.deltaTime * 20);
        }
    }

    private void SelectOnPointerCard()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach(RaycastResult result in results)
        {
            if(result.gameObject.transform.parent.TryGetComponent<CardBase>(out CardBase c))
            {
                if (!BattleReader.OnBinding || c.CanUseThisCard)
                {
                    RectTransform rt = c.transform as RectTransform;
                    BattleReader.OnPointerCard = c;
                    rt.SetAsLastSibling();
                }
                break;
            }
        }
    }

    private void CheckActivation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectOnPointerCard();
            if (!BattleReader.OnPointerCard || !BattleReader.OnPointerCard.CanUseThisCard) return;

            _selectIDX = BattleReader.GetIdx(BattleReader.OnPointerCard);
            BattleReader.CaptureHand();

            BattleReader.OnPointerCard.CardRecordList.Clear();
            foreach (var c in BattleReader.InHandCardList)
            {
                CardRecord record = new CardRecord
                (
                    BattleReader.InHandCardList.IndexOf(c),
                    c.CardID,
                    c.CardInfo.CardName,
                    c.CombineLevel
                );

                BattleReader.OnPointerCard.CardRecordList.Add(record);
            }

            BattleReader.OnBinding = true;

            _dragFilter.DOKill();
            _dragFilter.DOFade(0.5f, 0.1f);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _dragFilter.DOKill();
            _dragFilter.DOFade(0f, 0.1f);

            BattleReader.OnBinding = false;
            Activation();
        }
    }

    private void Activation()
    {
        if (!IsPointerOnCard() || !BattleReader.OnPointerCard.CanUseThisCard) return;

        if (BattleReader.OnPointerCard.transform.localPosition.y > 20)
        {
            if(!CostCalculator.CanUseCost(BattleReader.OnPointerCard.AbilityCost, BattleReader.OnPointerCard.CardInfo.CardType == CardType.SKILL))
            {
                BattleReader.InGameError.ErrorSituation("코스트가 부족합니다!");

                BattleReader.ResetByCaptureHand();
                BattleReader.OnPointerCard.SetUpCard(BattleReader.GetHandPos(BattleReader.OnPointerCard), true);
                return;
            }
            
            CostCalculator.UseCost(BattleReader.OnPointerCard.AbilityCost, BattleReader.OnPointerCard.CardInfo.CardType == CardType.SKILL);

            if (BattleReader.OnPointerCard.CardInfo.CardType == CardType.SKILL)
            {
                BattleReader.SkillCardManagement.SetSkillCardInCardZone(BattleReader.OnPointerCard);
            }
            else
            {
                BattleReader.SpellCardManagement.UseAbility(BattleReader.OnPointerCard);
            }
        }
        else //셔플
        {
            if(BattleReader.GetIdx(BattleReader.OnPointerCard) == _selectIDX
            || BattleReader.OnPointerCard == BattleReader.ShufflingCard)
            {
                BattleReader.OnPointerCard.SetUpCard(BattleReader.GetHandPos(BattleReader.OnPointerCard), true);
                return;
            }

            if(!CostCalculator.CanUseCost(1, true))
            {
                BattleReader.ShuffleInHandCard(BattleReader.OnPointerCard, BattleReader.ShufflingCard);
                BattleReader.InGameError.ErrorSituation("코스트가 부족합니다!");
                BattleReader.OnPointerCard.SetUpCard(BattleReader.GetHandPos(BattleReader.OnPointerCard), true);
                return;
            }

            CostCalculator.UseCost(1, true);
            BattleReader.OnPointerCard.SetUpCard(BattleReader.GetHandPos(BattleReader.OnPointerCard), true);
        }
    }

    private bool IsPointerOnCard()
    {
        return BattleReader.OnPointerCard != null;
    }
}
