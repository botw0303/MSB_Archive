using Fallencake.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DungeonCardCreator : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private Transform _backFacePrefab;
    [SerializeField] private DungeonCard _dungeonCardPrefab;
    [SerializeField] private Transform[] _dungeonCardTrmPosArr;
    private readonly int _canSelectDungeonCardCount = 3;

    [Header("Events")]
    [SerializeField] private UnityEvent<Transform> _backFaceAppearEvent;
    [SerializeField] private UnityEvent<Transform> _backFaceDisAppearEvent;
    [SerializeField] private UnityEvent<Transform> _dungeonCardAppearEvent;
    [SerializeField] private UnityEvent<DungeonCard[]> _dungeonCardProductionEvent;
    [SerializeField] private UnityEvent<DungeonCard, DungeonCard[]> _dungeonCardSelectionEvent;

    public void DungeonCardAppear()
    {
        StartCoroutine(StartSelectingDungeonCard());
    }

    private IEnumerator StartSelectingDungeonCard()
    {
        Transform[] backFaceTrms = new Transform[_canSelectDungeonCardCount];
        DungeonCard[] dungeonCards = new DungeonCard[_canSelectDungeonCardCount];

        CardInfo[] dungeonInfoArr = DeckManager.Instance.GetCardInfoList();

        dungeonInfoArr.Shuffle();

        for (int i = 0; i < _canSelectDungeonCardCount; i++)
        {
            Transform bf = Instantiate(_backFacePrefab, transform);
            bf.localPosition = _dungeonCardTrmPosArr[i].localPosition;

            backFaceTrms[i] = bf;
            _backFaceAppearEvent?.Invoke(bf);
        }

        yield return new WaitForSeconds(0.4f);

        for (int i = 0; i < _canSelectDungeonCardCount; i++)
        {
            _backFaceAppearEvent?.Invoke(backFaceTrms[i]);
            yield return new WaitForSeconds(0.3f);
            _backFaceDisAppearEvent?.Invoke(backFaceTrms[i]);
            yield return new WaitForSeconds(0.15f);

            DungeonCard card = Instantiate(_dungeonCardPrefab, transform);
            card.transform.localPosition = _dungeonCardTrmPosArr[i].localPosition;
            card.SetInfo(dungeonInfoArr[i]);
            _dungeonCardAppearEvent?.Invoke(card.VisualTrm);

            CardBase info = DeckManager.Instance.GetCard(dungeonInfoArr[i].CardName);
            card.SetInfoText(info.CardInfo.AbillityInfo, info.CardInfo.CardName, info.AbilityCost.ToString());

            card.OnClickEvent += (u) => card.CanSelect = true;

            dungeonCards[i] = card;
        }

        foreach (var tarotCard in dungeonCards)
        {
            tarotCard.OnClickEvent += (obj) =>
            _dungeonCardSelectionEvent?.Invoke(tarotCard, dungeonCards);
        }

        yield return new WaitForSeconds(0.32f);
        _dungeonCardProductionEvent?.Invoke(dungeonCards);
    }
}
