using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class BattleProduction : MonoBehaviour
{
    [SerializeField] private StageInfoPanel _stageInfoPanel;
    [SerializeField] private UnityEvent<TsumegoInfo> _clearChekcerSetEvent;
    [SerializeField] private UnityEvent<StageDataSO> _panelSetEvent;
    [SerializeField] private UnityEvent _battleStartEvent;
    protected PlayerAppear _playerAppear;
    private BattleContent _content;

    private void Start()
    {
        _content = GameManager.Instance.GetContent<BattleContent>();
        _playerAppear = FindObjectOfType<PlayerAppear>();
        FindObjectOfType<BattleBackground>()?.SetBG();
        if (StageManager.Instanace.SelectStageData.stageCutScene is not null)
        {
            _content.cutScene.endCutScene += OnEndCutScnen;
        }
        else
        {
            StartCoroutine(ProductionCo(false));
        }
    }

    private void OnEndCutScnen(PlayableDirector d)
    {
        StartCoroutine(ProductionCo(true));
        _content.cutScene.endCutScene -= OnEndCutScnen;
    }

    protected IEnumerator ProductionCo(bool spawnP)
    {
        _panelSetEvent?.Invoke(StageManager.Instanace.SelectStageData);


        _stageInfoPanel.PanelSetUp();
        yield return new WaitForSeconds(1.7f);
        _battleStartEvent?.Invoke();
        _clearChekcerSetEvent?.Invoke(StageManager.Instanace.SelectStageData.clearCondition);

        if (spawnP != true)
            _playerAppear.Action();

    }
}
