using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public static class BattleReader
{
    public static List<CardBase> InHandCardList = new List<CardBase>();
    public static List<CardBase> InDeckCardList = new List<CardBase>();
    public static List<(int, CardBase)> captureHandList = new ();

    public static Enemy SelectEnemy { get; set; }

    private static CardFactory _cardDrawer;
    public static CardFactory CardDrawer
    {
        get
        {
            if(_cardDrawer != null) return _cardDrawer;

            _cardDrawer = GameObject.FindObjectOfType<CardFactory>();
            if (_cardDrawer == null)
                UnityEngine.Debug.LogError("Not Found CardDrawer");
            return _cardDrawer;
        }
    }

    private static CombineMaster _combineMaster;
    public static CombineMaster CombineMaster
    {
        get
        {
            if (_combineMaster != null) return _combineMaster;

            _combineMaster = GameObject.FindObjectOfType<CombineMaster>();
            return _combineMaster;
        }
    }

    private static SkillCardManagement _skillCardManagement;
    public static SkillCardManagement SkillCardManagement
    {
        get
        {
            if(_skillCardManagement != null) return _skillCardManagement;
            _skillCardManagement = GameObject.FindObjectOfType<SkillCardManagement>();
            return _skillCardManagement;
        }
    }

    private static SpellCardManagement _spellCardManagement;
    public static SpellCardManagement SpellCardManagement
    {
        get
        {
            if(_spellCardManagement != null) return _spellCardManagement;
            _spellCardManagement = GameObject.FindObjectOfType<SpellCardManagement>();
            return _spellCardManagement;
        }
    }

    private static InGameError _inGameError;
    public static InGameError InGameError
    {
        get
        {
            if(_inGameError != null) return _inGameError;
            _inGameError = GameObject.FindObjectOfType<InGameError>();
            return _inGameError;
        }
    }

    private static AbilityTargettingSystem _abilityTargetSystem;
    public static AbilityTargettingSystem AbilityTargetSystem
    {
        get
        {
            if (_abilityTargetSystem != null) return _abilityTargetSystem;
            _abilityTargetSystem = GameObject.FindObjectOfType<AbilityTargettingSystem>();
            return _abilityTargetSystem;
        }
    }

    private static CardProductionMaster _cardProductionMaster;
    public static CardProductionMaster CardProductionMaster
    {
        get
        {
            if(_cardProductionMaster != null) return _cardProductionMaster;
            _cardProductionMaster = GameObject.FindObjectOfType<CardProductionMaster>();
            return _cardProductionMaster;
        }
    }

    private static CombatMarkManagement _combatMarkManagement;
    public static CombatMarkManagement CombatMarkManagement
    {
        get
        {
            if(_combatMarkManagement != null) return _combatMarkManagement;
            _combatMarkManagement = GameObject.FindObjectOfType<CombatMarkManagement>();
            return _combatMarkManagement;
        }
    }

    public static CardBase OnPointerCard { get; set; }
    public static bool OnBinding { get; set; }

    public static CardBase ShufflingCard { get; set; }
    private static int _deckIdx;

    public static bool IsOnTargetting { get; set; }

    public static void CaptureHand()
    {
        captureHandList.Clear();

        foreach(CardBase cb in InHandCardList)
        {
            captureHandList.Add((cb.CardID, cb));
        }
    }

    public static bool IsSameCaptureHand()
    {
        if(captureHandList.Count != InHandCardList.Count) return false;

        for(int i = 0; i < captureHandList.Count; i++)
        {
            if (captureHandList[i].Item1 != InHandCardList[i].CardID) return false;
        }
        return true;
    }

    public static void SetDeck(List<CardBase> deck)
    {
        _deckIdx = 0;
        InHandCardList.Clear();
        InDeckCardList = deck;
    }

    public static void ResetByCaptureHand()
    {
        InHandCardList.Clear();

        foreach(var cb in captureHandList)
        {
            InHandCardList.Add(cb.Item2);
        }
    }

    public static List<CardBase> GetHandCards()
    {
        return InHandCardList;
    }

    public static void AddCardInHand(CardBase addingCardInfo)
    {
        InHandCardList.Add(addingCardInfo);
    }

    public static void RemoveCardInHand(CardBase removingCardInfo)
    {
        InHandCardList.Remove(removingCardInfo);
    }

    public static int CountOfCardInHand()
    {
        return InHandCardList.Count;
    }

    public static CardBase GetCardinfoInHand(int index)
    {
        if(index < 0 || index > CountOfCardInHand()) return null;

        return InHandCardList[index];
    }

    public static void AddCardInDeck(CardBase addingCardInfo)
    {
        InDeckCardList.Add(addingCardInfo);
    }

    public static void RemoveCardInDeck(CardBase removingCardInfo)
    {
        InDeckCardList.Remove(removingCardInfo);
    }

    public static int CountOfCardInDeck()
    {
        return InDeckCardList.Count;
    }

    
    public static CardBase GetCardInDeck()
    {
        _deckIdx %= InDeckCardList.Count;
        return InDeckCardList[_deckIdx++];
    }

    public static CardBase GetRandomCardInDeck()
    {

        return InDeckCardList[Random.Range(0, InDeckCardList.Count)];
    }

    public static int GetIdx(CardBase handCard)
    {
        return InHandCardList.IndexOf(handCard);
    }

    public static void ShuffleInHandCard(CardBase pointerCard, CardBase shufflingCard)
    {
        ShufflingCard = shufflingCard;

        int idx1 = InHandCardList.IndexOf(pointerCard);
        int idx2 = InHandCardList.IndexOf(shufflingCard);

        (InHandCardList[idx1], InHandCardList[idx2]) =
        (InHandCardList[idx2], InHandCardList[idx1]);
    }

    public static int GetPosOnTopDrawCard()
    {
        return 860 - ((CountOfCardInHand() - 1) * 170);
    }

    public static int GetHandPos(CardBase cardBase)
    {
        int idx = InHandCardList.IndexOf(cardBase);
        return 860 - (idx * 170);
    }

    public static void LockHandCard(bool isLock)
    {
        foreach(CardBase card in InHandCardList)
        {
            card.CanUseThisCard = !isLock;
        }
    }
}
