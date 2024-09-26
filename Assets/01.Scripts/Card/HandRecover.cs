using CardDefine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardRecord
{
    public int HandIdx { get; private set; }
    public int CardID { get; private set; }
    public string CardName { get; private set; }
    public CombineLevel CombineLevel { get; private set; }

    public CardRecord(int handIdx, int cardIdx, string cName, CombineLevel combineLv)
    {
        HandIdx = handIdx;  
        CardID = cardIdx;
        CardName = cName;
        CombineLevel = combineLv;
    }
}

public class HandRecover : MonoBehaviour
{
    [SerializeField] private SkillCardManagement _skillCardManagement;
    private List<CardBase> _inWaitZoneCardList => _skillCardManagement.InCardZoneList;

    [SerializeField] private Transform _cardHandZone;
    [SerializeField] private Transform _resotreCardZone;

    public void RevertHand(CardBase card)
    {
        if (card.CardID != _inWaitZoneCardList[_inWaitZoneCardList.Count - 1].CardID) return;

        CostCalculator.GetCost(card.AbilityCost);

        CardRecord myRec = card.CardRecordList.FirstOrDefault(x => x.CardID == card.CardID);
        BattleReader.InHandCardList.Insert(myRec.HandIdx, card);
        card.transform.SetParent(_cardHandZone);

        _inWaitZoneCardList.Remove(card);
        RestoreNotExistCard(card.CardRecordList);

        card.IsOnActivationZone = false;
        _skillCardManagement.SetSkillCardInHandZone();
        BattleReader.AbilityTargetSystem.TargettingCancle(card.CardID);
    }

    public void RevertAllHand()
    {
        if (_inWaitZoneCardList.Count == 0) return;

        CardBase oldCard = _inWaitZoneCardList[0];

        foreach(var card in _inWaitZoneCardList)
        {
            CardRecord myRec = oldCard.CardRecordList.FirstOrDefault(x => x.CardID == card.CardID);
            BattleReader.InHandCardList.Insert(myRec.HandIdx, card);
            card.transform.SetParent(_cardHandZone);
        }

        _inWaitZoneCardList.Clear();

        oldCard.transform.SetParent(_cardHandZone);

    }

    private void RestoreNotExistCard(List<CardRecord> recordList)
    {
        foreach(var rc in recordList)
        {
            CardBase recordCard = BattleReader.InHandCardList.
            FirstOrDefault(c => c.CardID == rc.CardID);

            if (recordCard == null)
            {
                CardBase cb = Instantiate(DeckManager.Instance.GetCard(rc.CardName), _cardHandZone);
                cb.SetInfo(rc.CardID, rc.CombineLevel);
                cb.transform.position = _resotreCardZone.position;

                BattleReader.InHandCardList.Insert(rc.HandIdx, cb);
            }
            else
            {
                if(recordCard.CombineLevel != rc.CombineLevel)
                {
                    recordCard.CombineLevel = rc.CombineLevel;
                }
            }
        }
    }
}
