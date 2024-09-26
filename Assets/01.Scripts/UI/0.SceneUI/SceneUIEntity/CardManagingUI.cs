using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardManagingUI : SceneUI
{
    public int LoadStoneCount => Inventory.Instance.GetItemInfo("불푸레 결석").haveCount;
    public int ToUseGoods => CurrentCardShameElementInfo.cardLevel * 50;

    public CardShameElementSO CurrentCardShameElementInfo { get; private set; }
    public SelectToManagingCardElement SelectCardElement { get; private set; }

    [SerializeField] private UnityEvent<float> _onPressLevelUpEvent;
    [SerializeField] private UnityEvent<CardInfo> _onSelectToManagingCardEvent;

    [SerializeField]
    private GameObject _tutorialPanel;

    public void PressLevelUpButton()
    {
        if (CurrentCardShameElementInfo.cardLevel >= 5)
        {
            ErrorText et = PoolManager.Instance.Pop(PoolingType.ErrorText) as ErrorText;
            et.Erroring("카드가 최대 레벨입니다");
            return;
        }

        if(CanUseGoods(ToUseGoods))
        {
            float currentEXP = CurrentCardShameElementInfo.cardExp += ToUseGoods * 0.4f;
            _onPressLevelUpEvent?.Invoke(currentEXP);
        }
    }

    public void OnSelectToManagingCard(SelectToManagingCardElement selectCardElement)
    {
        if(SelectCardElement != null)
        {
            SelectCardElement.UnSelectCard();
        }

        SelectCardElement = selectCardElement;

        CurrentCardShameElementInfo = selectCardElement.CardInfo.cardShameData;
        _onSelectToManagingCardEvent?.Invoke(selectCardElement.CardInfo);
    }

    public bool CanUseGoods(int toUseGoods)
    {
        return LoadStoneCount >= toUseGoods;
    }

    public override void SceneUIStart()
    {
        base.SceneUIStart();

        CheckOnFirst cf = DataManager.Instance.LoadData<CheckOnFirst>(DataKeyList.checkIsFirstPlayGameDataKey);
        if (!cf.isFirstOnCardUpgrade)
        {
            _tutorialPanel.SetActive(true);
            cf.isFirstOnCardUpgrade = true;
            DataManager.Instance.SaveData(cf, DataKeyList.checkIsFirstPlayGameDataKey);
        }
    }
}
