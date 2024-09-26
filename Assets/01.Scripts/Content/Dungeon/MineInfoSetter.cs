using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MineInfoSetter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _stageFloorText;
    [SerializeField] private TextMeshProUGUI _stageNameText;
    private MineInfoContainer _container;

    private AdventureData _mineData = new AdventureData();

    private void Awake()
    {
        _container = GetComponent<MineInfoContainer>();
    }

    private void Start()
    {
        if(DataManager.Instance.IsHaveData(DataKeyList.adventureDataKey))
        {
            _mineData = DataManager.Instance.LoadData<AdventureData>(DataKeyList.adventureDataKey);
        }

        int challingingFloor = Convert.ToInt16(_mineData.ChallingingMineFloor);
        MineInfo info = _container.GetInfoByFloor(challingingFloor);

        if(info.stageData.isClearThisStage)
        {
            _mineData.ChallingingMineFloor = $"{challingingFloor += 1}";
            DataManager.Instance.SaveData(_mineData, DataKeyList.adventureDataKey);
            info = _container.GetInfoByFloor(challingingFloor);
        }

        UIManager.Instance.GetSceneUI<MineUI>().CurrentStage = info;

        _stageFloorText.text = $"{info.Floor}Ãþ";
        _stageNameText.text = info.stageData.stageName;

        StageManager.Instanace.SelectStageData = info.stageData;
    }
}
