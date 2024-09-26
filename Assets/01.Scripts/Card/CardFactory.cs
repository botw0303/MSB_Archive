using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardFactory : MonoBehaviour
{
    [SerializeField] private Transform _cardSpawnTrm;
    [SerializeField] private Transform _cardParent;
    private Queue<CardBase> _toDrawCatalog = new Queue<CardBase>();

    private bool _canDraw;
    public bool CanDraw
    {
        get { return _canDraw; }
        set 
        { 
            _canDraw = value;
            if(_canDraw && _toDrawCatalog.Count != 0)
            {
                DrawCardLogic(_toDrawCatalog.Dequeue());
            }
        }
    }
    private BattleController _battleController;
    public BattleController BattleController
    {
        get
        {
            if (_battleController != null) return _battleController;
            _battleController = FindObjectOfType<BattleController>();
            return _battleController;
        }
    }
    private int _factoryID;

    private HandRecover _handRecover;
    public HandRecover HandRecover
    {
        get
        {
            if (_handRecover != null) return _handRecover;
            _handRecover = FindObjectOfType<HandRecover>();
            return _handRecover;
        }
    }

    public void DrawCard(int count, bool isRandom = true)
    {
        CanDraw = false;

        if(isRandom)
        {
            for (int i = 0; i < count; i++)
            {
                CardBase selectInfo = BattleReader.GetRandomCardInDeck();

                print(selectInfo.name);

                _toDrawCatalog.Enqueue(selectInfo);
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                CardBase selectInfo = BattleReader.GetCardInDeck();
                print(selectInfo.name);
                _toDrawCatalog.Enqueue(selectInfo);
            }
        }

        DrawCardLogic(_toDrawCatalog.Dequeue());
    }
    private void DrawCardLogic(CardBase selectInfo)
    {
        CardBase spawnCard = Instantiate(selectInfo, _cardParent);
        spawnCard.CardID = _factoryID;
        BattleReader.CardProductionMaster.OnCardIdling(spawnCard);

        spawnCard.OnPointerSetCardAction += BattleReader.CardProductionMaster.OnSelectCard;
        spawnCard.OnPointerInitCardAction += BattleReader.CardProductionMaster.QuitSelectCard;

        spawnCard.battleController = this.BattleController;
        spawnCard.RecoverEvent += HandRecover.RevertHand;
        BattleReader.AddCardInHand(spawnCard);
        spawnCard.transform.position = _cardSpawnTrm.position;
        spawnCard.SetUpCard(BattleReader.GetPosOnTopDrawCard(), true);

        _factoryID++;
    }

    public void DestroyCard(CardBase card)
    {
        Destroy(card.gameObject);
    }
}
