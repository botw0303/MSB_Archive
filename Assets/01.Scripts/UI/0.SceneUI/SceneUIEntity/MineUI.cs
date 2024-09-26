using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineUI : SceneUI
{
    public MineInfo CurrentStage { get; set; }

    [SerializeField]
    protected GameObject _tutorialPanel;

    [SerializeField] private MineInfo[] _mineInfoArr;

    public override void SceneUIStart()
    {
        base.SceneUIStart();

        SceneObserver.BeforeSceneType = SceneType.Lobby;

        CheckOnFirst cf = DataManager.Instance.LoadData<CheckOnFirst>(DataKeyList.checkIsFirstPlayGameDataKey);
        if (!cf.isFirstOnEnterDungeon)
        {
            //_tutorialPanel?.SetActive(true);
            cf.isFirstOnEnterDungeon = true;
            DataManager.Instance.SaveData(cf, DataKeyList.checkIsFirstPlayGameDataKey);
        }

        if(DeckManager.Instance.GetDungeonDeckMakeChance() == 5)
        {
            foreach(var m in _mineInfoArr)
            {
                m.IsClearThisStage = false;
            }
        }

        GameManager.Instance.stat.atkAddValue = 0;
        GameManager.Instance.stat.hpAddValue = 0;
        CostCalculator.CurrentMoney = 10;
    }

    public void GotoEditDeck()
    {
        GameManager.Instance.ChangeScene(SceneType.deckBuild);
    }

    public void GoToBattle()
    {
        foreach (var m in _mineInfoArr)
        {
            if(!m.IsClearThisStage)
            {
                StageManager.Instanace.SelectStageData = m.stageData;
                break;
            }
        }

        StageManager.Instanace.SelectDeck = DeckManager.Instance.DungeonDeckList;
        GameManager.Instance.ChangeScene(SceneType.battle);
    }
}
