using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnCounting : MonoBehaviour
{

    [Header("����")]
    [SerializeField] private TextMeshProUGUI _toPTChangingText;
    [SerializeField] private TextMeshProUGUI _toETChangingText;
    [SerializeField] private TextMeshProUGUI _gameEndText;
    [SerializeField] private Transform _turnChaingLabel;
    [SerializeField] private GameObject _visualObject;
    [SerializeField] private Button _turnSkipBtn;
    [SerializeField] private OnSkill _costChecker;
    private TextMeshProUGUI _selectText;
    private Transform _selectTrm;
    private bool _isTurnChaing = false;

	[Header("����")]
	[SerializeField] private Transform _startPos;
	[SerializeField] private Transform _normalPos;
	[SerializeField] private Transform _endPos;

	private void Start()
	{
        BattleReader.SetDeck(StageManager.Instanace.SelectDeck);
        TurnCounter.PlayerTurnStartEvent += ToPlayerTurnChanging;
        foreach (CardBase card in BattleReader.InDeckCardList)
        {
            if(card.TryGetComponent<IEndowStackSkill>(out IEndowStackSkill endowStackSkill))
            {
                Debug.Log("Reset addition");
                endowStackSkill.ResetAdditionStack();
            }
        }

        _turnSkipBtn.onClick.AddListener(() =>
        {
            ToEnemyTurnChanging(true);
        });
    }

	private void OnDestroy()
	{
		TurnCounter.PlayerTurnStartEvent -= ToPlayerTurnChanging;
	}

	public void BattleEnd(bool isBattleEnd)
	{
		_visualObject.SetActive(!isBattleEnd);
	}

	public void BattleStart()
	{
		TurnCounter.Init();
		CostCalculator.Init();

		Sequence seq = DOTween.Sequence();
		seq.AppendCallback(() =>
		{
			ToPlayerTurnChanging(false);
			TurnCounter.ChangeRound();
		});

		BattleReader.CardDrawer.DrawCard(5);
		//seq.AppendCallback(() => BattleReader.CardDrawer.DrawCard(5));
	}

	public Sequence BattleEndSequence(bool isVictory)
	{
		_gameEndText.transform.localScale = Vector3.one * 1.2f;
        Time.timeScale = 1;
		BattleController.Instance.Player.BuffStatCompo.ClearStat();
        _gameEndText.text = isVictory ? "VICTORY" : "DEFEAT";
        Sequence seq = DOTween.Sequence();
		seq.Append(_turnChaingLabel.DOScaleY(1, 0.4f));

        seq.Join(_gameEndText.DOFade(1, 0.4f));
        seq.AppendInterval(1f);
        seq.Append(_turnChaingLabel.DOScaleY(0, 0.4f));
        seq.Join(_gameEndText.DOFade(0, 0.4f));

        return seq;
    }
	public void ToPlayerTurnChanging(bool isTurnChange)
	{

		if (UIManager.Instance.GetSceneUI<BattleUI>().IsBattleEnd) return;
        _selectTrm = _toPTChangingText.transform;
        _selectText = _toPTChangingText;

        _turnSkipBtn.gameObject.SetActive(true);
        _costChecker.SystemUp();
        TurnChanging(isTurnChange);

	}

	public void ToEnemyTurnChanging(bool isTurnChange)
	{
		if (UIManager.Instance.GetSceneUI<BattleUI>().IsBattleEnd) return;
        _selectTrm = _toETChangingText.transform;
        _selectText = _toETChangingText;
        _turnSkipBtn.gameObject.SetActive(false);
        TurnChanging(isTurnChange);
    }


	private void TurnChanging(bool isTurnChange)
	{
		_selectTrm.transform.localPosition = _startPos.localPosition;
		_selectText.color = new Color(1, 1, 1, 1);

		Sequence seq = DOTween.Sequence();
		seq.Append(_selectTrm.DOLocalMove(_normalPos.localPosition, 0.5f).SetEase(Ease.OutCubic));
		seq.Join(_turnChaingLabel.DOScaleY(1, 0.4f));
		seq.AppendInterval(0.5f);
		seq.Append(_selectTrm.DOLocalMove(_endPos.localPosition, 0.5f).SetEase(Ease.InCubic));
		seq.Join(_selectText.DOFade(0, 0.5f));
		seq.Join(_turnChaingLabel.DOScaleY(0, 0.4f));
		if (isTurnChange)
			seq.AppendCallback(TurnCounter.ChangeTurn);
	}
}
