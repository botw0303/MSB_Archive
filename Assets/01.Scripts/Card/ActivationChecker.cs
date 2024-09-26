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

            // ī�� Transform = ������ ī���� Transform
            Transform cardTrm = BattleReader.OnPointerCard.transform;

            // ���콺 ��ġ�� �ޱ�. Canvas�� Rendermode�� Camera�� ������
            // RectTransformUtility�� Ȱ���Ѵ�.
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle
            (UIManager.Instance.CanvasTrm, Input.mousePosition, Camera.main, out pos);

            Vector3 mousePos = pos;

            // ���콺�� ī���� x���� ���� ���� 
            float distance = (mousePos - cardTrm.localPosition).x;
            // ���� �Ÿ��� ���� �̿��Ͽ� ����� ������ Z���� ����
            float rotation = Mathf.Clamp(distance, -20, 20);

            // ī���� ����� ������ ����
            Vector3 euler = cardTrm.localEulerAngles;
            euler.z = rotation;

            mousePos.z = 0;
            // ���� ������� ������
            // ������ �������� ���� �ʱ� ���켭 Lerp�� �����
            cardTrm.localEulerAngles = Vector3.Lerp(cardTrm.localEulerAngles, euler, Time.time);
            // ī���� ��ġ�� ���콺 ��ġ�� �̵���Ŵ. ���������� ������ �������� ���� �ʱ� ���� Lerp���
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
                BattleReader.InGameError.ErrorSituation("�ڽ�Ʈ�� �����մϴ�!");

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
        else //����
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
                BattleReader.InGameError.ErrorSituation("�ڽ�Ʈ�� �����մϴ�!");
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
