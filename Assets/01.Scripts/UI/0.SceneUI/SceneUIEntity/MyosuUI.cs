using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyosuUI : SceneUI
{
    [SerializeField] private CrossRoadsAppearText _appearText;

    [SerializeField]
    private GameObject _tutorialPanel;

    public override void SceneUIStart()
    {
        base.SceneUIStart();
        SceneObserver.BeforeSceneType = SceneType.Lobby;

        CheckOnFirst cf = DataManager.Instance.LoadData<CheckOnFirst>(DataKeyList.checkIsFirstPlayGameDataKey);
        if (!cf.isFirstOnEnterMaze)
        {
            _tutorialPanel.SetActive(true);
            cf.isFirstOnEnterMaze = true;
            DataManager.Instance.SaveData(cf, DataKeyList.checkIsFirstPlayGameDataKey);
        }

        AdventureData data = DataManager.Instance.LoadData<AdventureData>(DataKeyList.adventureDataKey);
        GameManager.Instance.stat.atkAddValue = data.MazeAtkAddValue;
        GameManager.Instance.stat.hpAddValue = data.MazeHpAddvalue;
        CostCalculator.CurrentMoney += data.MazeCostAddValue;
    }

    public void ApearText()
    {
        _appearText.Show();
    }

    public void HideText()
    {
        _appearText.Hide();
    }

    public void GoToBattleScene()
    {
        GameManager.Instance.ChangeScene(SceneType.battle);
    }
}
