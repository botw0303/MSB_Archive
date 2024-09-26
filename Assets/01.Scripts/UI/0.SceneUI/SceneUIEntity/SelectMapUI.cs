using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMapUI : SceneUI
{
    [SerializeField] private ChapterElement[] _chapterElement;
    [SerializeField] private MapSelectPanelLock[] _panelLockArr;
    private AdventureData _adventureData = new AdventureData();

    public override void SceneUIStart()
    {
        base.SceneUIStart();
        SceneObserver.BeforeSceneType = SceneType.Lobby;

        GenerateUnLockPanel();

        GameManager.Instance.stat.atkAddValue = 0;
        GameManager.Instance.stat.hpAddValue = 0;
        CostCalculator.CurrentMoney = 10;
    }

    public AdventureData GetAdventureData()
    {
        return DataManager.Instance.LoadData<AdventureData>(DataKeyList.adventureDataKey);
    }

    private void GenerateUnLockPanel()
    {
        if (DataManager.Instance.IsHaveData(DataKeyList.adventureDataKey))
        {
            _adventureData = DataManager.Instance.LoadData<AdventureData>(DataKeyList.adventureDataKey);
        }

        int chapterIdx = Convert.ToInt16(_adventureData.InChallingingStageCount.Split('-')[0]);
        for (int i = 0; i < chapterIdx; i++)
        {
            //_chapterElement[i].CanTryThisChapter = true;

            if (_adventureData.IsLookUnLockProductionArr[i])
            {
                _panelLockArr[i].UnLockStageWithOutProduction();
            }
            else
            {
                _panelLockArr[i].UnLockStageWithProduction();
                _adventureData.IsLookUnLockProductionArr[i] = true;
            }
        }

        _chapterElement[0].CanTryThisChapter = true;

        DataManager.Instance.SaveData(_adventureData, DataKeyList.adventureDataKey);
    }
}
