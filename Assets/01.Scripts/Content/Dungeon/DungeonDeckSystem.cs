using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DungeonDeckSystem : MonoBehaviour
{
    [SerializeField] private Image _backGround;
    [SerializeField] private UnityEvent _dungeonCardsAppearEvent;

    [SerializeField] private GameObject _board;
    [SerializeField] private Transform _newCardTrm;
    [SerializeField] private Image _newCardPrefab;

    private bool _canSelectCard = false;
    private CardInfo _cardInfo;

    public void SelectCard(DungeonCard info, DungeonCard[] infos)
    {
        _cardInfo = info.CardInfo;
        _canSelectCard = true;
    }

    private void Start()
    {
        bool canAddCard = DeckManager.Instance.GetDungeonDeckMakeChance() > 0;

        _board.SetActive(!canAddCard);

        if (canAddCard)
        {
            DeckManager.Instance.NewDungeonCardList.Clear();
            StartCoroutine(MakeDungeonDeckCo());
        }
        else
        {
            SettingNewCards();
        }
    }

    private IEnumerator MakeDungeonDeckCo()
    {
        _backGround.DOColor(Color.gray, 0.2f);

        while(DeckManager.Instance.GetDungeonDeckMakeChance() > 0)
        {
            _dungeonCardsAppearEvent?.Invoke();

            yield return new WaitUntil(() => _canSelectCard);

            _canSelectCard = false;
            CardBase card = DeckManager.Instance.GetCard(_cardInfo.CardName);
            DeckManager.Instance.SetDungeonDeck(card);

            yield return new WaitForSeconds(1);
        }

        _backGround.DOColor(Color.white, 0.2f);
        LookDungeonDeck();
    }

    private void LookDungeonDeck()
    {
        _board.SetActive(true);
        SettingNewCards();
    }

    private void SettingNewCards()
    {
        foreach (var c in DeckManager.Instance.NewDungeonCardList)
        {
            Image newCard = Instantiate(_newCardPrefab, _newCardTrm);

            TextMeshProUGUI cost = newCard.transform.Find("CostText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cname = newCard.transform.Find("CardName").GetComponent<TextMeshProUGUI>();

            newCard.sprite = c.CardInfo.CardVisual;
            cost.text = c.AbilityCost.ToString();
            cname.text = c.CardInfo.CardName;
        }
    }
}
