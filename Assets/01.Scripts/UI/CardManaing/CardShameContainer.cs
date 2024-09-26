using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardShameContainer : MonoBehaviour
{
    [SerializeField] private List<CardInfo> _cardShameDataContainer;
    [SerializeField] private UnityEvent<List<CardInfo>> _cardElementGeneratingEvent;

    private CanUseCardData _canUseCardData = new CanUseCardData();

    private void Start()
    {
        if(DataManager.Instance.IsHaveData(DataKeyList.canUseCardDataKey))
        {
            _canUseCardData = 
            DataManager.Instance.LoadData<CanUseCardData>(DataKeyList.canUseCardDataKey);
        }
        
        foreach(var cardName in _canUseCardData.CanUseCardsList)
        {
            _cardShameDataContainer.Add(DeckManager.Instance.GetCard(cardName).CardInfo);
        }

        _cardElementGeneratingEvent?.Invoke(_cardShameDataContainer);
    }

    public CardShameElementSO GetCardShameData(CardInfo info)
    {
        return _cardShameDataContainer.Find(x => x == info).cardShameData;
    }
}
