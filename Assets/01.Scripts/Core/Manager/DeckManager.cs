using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager : MonoSingleton<DeckManager>
{
    [SerializeField] private CardBase[] _totalCardArr;
    private Dictionary<string, CardBase> _getCardDic = new();
    public (DeckElement, int) SaveDummyDeck { get; set; }

    private int _canSelectDungeonDeckChance = 5;
    public List<CardBase> DungeonDeckList { get; private set; } = new List<CardBase>();
    public List<CardBase> NewDungeonCardList { get; private set; } = new List<CardBase>();

    private void Awake()
    {
        foreach(CardBase card in _totalCardArr)
        {
            _getCardDic.Add(card.CardInfo.CardName, card);
        }
    }
    public CardInfo[] GetCardInfoList()
    {
        return _totalCardArr.Select(x => x.CardInfo).ToArray();
    }
    public CardBase GetCard(string cardName)
    {
        return _getCardDic[cardName];
    }
    public List<string> GetDeckData(List<CardBase> deck)
    {
        List<string> data = new List<string>();

        foreach(CardBase card in deck)
        {
            data.Add(card.CardInfo.CardName);
        }

        return data;
    }
    public List<CardBase> GetDeck(List<string> deckData)
    {
        List<CardBase> _deck = new ();

        foreach(string cardName in deckData)
        {
            _deck.Add(_getCardDic[cardName]);
        }

        return _deck;
    }
    public CardInfo GetRandomCardInfo()
    {
        return _totalCardArr[Random.Range(0, _totalCardArr.Length)].CardInfo;
    }
    public void SetDungeonDeck(CardBase card)
    {
        _canSelectDungeonDeckChance--;
        NewDungeonCardList.Add(card);
        DungeonDeckList.Add(card);
    }
    public int GetDungeonDeckMakeChance()
    {
        return _canSelectDungeonDeckChance;
    }
    public void SetDungeonDeckMakeChance(int num)
    {
        _canSelectDungeonDeckChance = num;
    }
}
