using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct SkillData
{
    public string skillName;
    public Sprite sprite;
    public int cost;
    public ParticleSystem fx;
}

public class AccumulateCost : MonoBehaviour
{
    [SerializeField] private List<GameObject> _menuObjects = new List<GameObject>();
    [SerializeField] private SkillData[] _sprites;

    private List<Action> _actionList = new List<Action>();

    private void Start()
    {
        ActionGenerate();
    }

    private void ActionGenerate()
    {
        _actionList.Add(() =>
        {
            Heal();
        });

        _actionList.Add(() =>
        {
            Suffle();
        });
    }

    private void ExecuteEffect(int index)
    {
        _sprites[index].fx.Play();
    }

    public void Heal()
    {
        if (CostCalculator.CurrentAccumulateMoney < _sprites[0].cost) return;
        CostCalculator.AccumulateChangeEvent?.Invoke(-_sprites[0].cost);
        BattleController.Instance.Player.HealthCompo.ApplyHeal(10);

        ExecuteEffect(0);
    }

    public void Suffle()
    {
        if (CostCalculator.CurrentAccumulateMoney < _sprites[1].cost) return;

        CostCalculator.AccumulateChangeEvent?.Invoke(-_sprites[1].cost);

        int count = BattleReader.CountOfCardInHand();
        foreach (CardBase card in BattleReader.GetHandCards())
        {
            BattleReader.CardDrawer.DestroyCard(card);
        }
        BattleReader.GetHandCards().Clear();
        BattleReader.CardDrawer.DrawCard(count, true);
        ExecuteEffect(1);
    }
}
