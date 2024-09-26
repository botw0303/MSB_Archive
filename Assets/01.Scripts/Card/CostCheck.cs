using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CostCheck : MonoBehaviour
{
    [SerializeField] private GameObject _costObject;
    [SerializeField] private GameObject _accCostObject;
    private Tween _numberingTween;
    private Tween _accNumTween;
    private int _targetCost;
    private int _accTargetCost;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _accCostText;
    [SerializeField] private Slider _costSlider;

    private void Start()
    {
        CostCalculator.MoneyChangeEvent += HandleCheckCost;
        HandleCheckCost(CostCalculator.CurrentMoney);

        CostCalculator.ExtraManaChangeEvent += HandleCheckExMana;
        HandleCheckExMana(CostCalculator.CurrentExMana);

        CostCalculator.AccumulateChangeEvent += HandleCheckAccumulateCost;

        TurnCounter.PlayerTurnStartEvent += HandleCalculateExMana;
        TurnCounter.PlayerTurnStartEvent += HandleEnableCostObj;

        TurnCounter.PlayerTurnEndEvent += HandleDisableCostObj;
        TurnCounter.PlayerTurnEndEvent += HandleAccChange;
    }

    private void OnDisable()
    {
        CostCalculator.MoneyChangeEvent -= HandleCheckCost;
        CostCalculator.ExtraManaChangeEvent -= HandleCheckExMana;
        CostCalculator.AccumulateChangeEvent -= HandleCheckAccumulateCost;

        TurnCounter.PlayerTurnStartEvent -= HandleEnableCostObj;
        TurnCounter.PlayerTurnEndEvent -= HandleDisableCostObj;
        TurnCounter.PlayerTurnEndEvent -= HandleAccChange;
    }

    private void HandleEnableCostObj(bool  vlaue)
    {
        _costObject.SetActive(true);
        _accCostObject.SetActive(true);
    }

    private void HandleDisableCostObj()
    {
        _costObject.SetActive(false);
        _accCostObject.SetActive(false);
    }

    private void HandleCalculateExMana(bool a)
    {
        CostCalculator.GetExMana(CostCalculator.CurrentMoney);
        CostCalculator.CurrentMoney = CostCalculator.MaxMoney;
        CostCalculator.GetCost(0);
    }

    private void HandleCheckCost(int currentMoney)
    {
        _numberingTween.Kill();
        _targetCost = currentMoney;

        _costText.transform.DOScale(Vector3.one * 1.2f, 0.2f).OnComplete(() =>
        _costText.transform.DOScale(Vector3.one, 0.2f));

        int currentMarkingNum = Convert.ToInt16(_costText.text);
        print((float)currentMoney * 0.1f);
        _costSlider.value = (float)currentMoney * 0.1f;
        _numberingTween = DOTween.To(() => currentMarkingNum,
                                      m => _costText.text = m.ToString(),
                                      _targetCost, 0.5f);
    }

    private void HandleCheckAccumulateCost(int value)
    {
        if (_accCostText == null) return;
        _accNumTween.Kill();

        CostCalculator.CurrentAccumulateMoney += value;
        _accTargetCost = CostCalculator.CurrentAccumulateMoney;

        _accCostText.transform.DOScale(Vector3.one * 1.2f, 0.2f).OnComplete(() =>
        _accCostText.transform.DOScale(Vector3.one, 0.2f));

        int currentMarkingNum = Convert.ToInt16(_accCostText.text);
        _accNumTween = DOTween.To(() => currentMarkingNum,
                                      m => _accCostText.text = m.ToString(),
                                      _accTargetCost, 0.5f);
    }

    public void HandleAccChange()
    {
        HandleCheckCost(0);
        HandleCheckAccumulateCost(CostCalculator.CurrentMoney);
    }

    private void HandleCheckExMana(int currentMana)
    {
        
    }
}
