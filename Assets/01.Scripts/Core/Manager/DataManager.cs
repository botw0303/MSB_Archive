using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoSingleton<DataManager>
{
    private List<string> _datakeyList = new List<string>();
    private readonly string _dataKeyFilePath = Path.Combine(Application.dataPath, "DataKeys.json");

    public CheckOnFirst CheckOnFirstData { get; set; }

    private void Awake()
    {
        // ���� ��ο� ����� Ű������ �ִٸ�
        if(File.Exists(_dataKeyFilePath))
        {
            // Ű������ �����Ͽ� ����Ʈ�� ��´�.
            string[] keyArr = File.ReadAllText(_dataKeyFilePath).Split(",");
            int saveFileCount = keyArr.Length - 1;

            for (int i = 0; i < saveFileCount; i++)
            {
                _datakeyList.Add(keyArr[i]);
            }
        }
    }

    // ������ ���� �� �����Ϸ��� ������ ����, ������ Ű �ʿ�
    public void SaveData(CanSaveData saveData, string dataKey) 
    {
        // ������ Ű�� ����� �̷��� �ִ� ���������� Ȯ��
        if(!IsHaveData(dataKey))
        {
            string prevData = "";
            if(File.Exists(_dataKeyFilePath))
            {
                // �̹� �����ϴ� ������ Ű���� �̸� prevData�� ��� ���´�.
                prevData = File.ReadAllText(_dataKeyFilePath);
            }

            //���ο� ������ Ű ����
            string saveKey = prevData + $"{dataKey},";


            // ���ο� ������ Ű�� ���Ե� ������ Ű���� �ٽ� ����
            File.WriteAllText(_dataKeyFilePath, saveKey);
            _datakeyList.Add(dataKey);
        }

        // ������ ���� ��θ� �ҷ����� �װ��� ����
        File.WriteAllText(GetFilePath(dataKey), JsonUtility.ToJson(saveData));
    }

    // ������ �ε� �� �̸� ����� ������ Ű �ʿ�
    public T LoadData<T>(string dataKey) where T : CanSaveData
    {
        // ������ Ű�� ����� �̷��� �ִ� ���������� Ȯ��
        if (!IsHaveData(dataKey))
        {
            // ������ Ű�� ���� �� ������ ����Ѵ�.
            Debug.LogWarning($"Error! No exit data key!! Key name : {dataKey}");
            return default(T);
        }

        // �����Ͱ� ������ �� ������ �о� �������ش�.
        return JsonUtility.FromJson<T>(File.ReadAllText(GetFilePath(dataKey)));
    }

    // ������ Ű�� Ȱ���� ������ ������ ������ Ȯ��
    public bool IsHaveData(string dataKey)
    {
        return _datakeyList.Contains(dataKey);
    }

    // ������ Ű�� �޾� �ش� ������ ���ٸ� ���� �� ��θ� ����
    private string GetFilePath(string dataKey)
    {
        return Path.Combine(Application.dataPath, $"{dataKey}.json");
    }

    public void ResetData()
    {
        if (File.Exists(_dataKeyFilePath))
        {
            string[] keyArr = File.ReadAllText(_dataKeyFilePath).Split(",");
            int saveFileCount = keyArr.Length - 1;

            for(int i = 0; i <  saveFileCount; i++)
            {
                if (keyArr[i] == "PlayersDeck" || keyArr[i] == "CanUseCardsDataKey") continue;
                if(File.Exists(GetFilePath(keyArr[i])))
                {
                    if (keyArr[i] == "AdventureKEY")
                    {
                        File.WriteAllText(GetFilePath(keyArr[i]), "");
                        File.WriteAllText(GetFilePath(keyArr[i]), "{\"ChallingingMineFloor\":\"1\",\"IsLookUnLockProductionArr\":[true,false,false,false,false,false],\"InChallingingMazeLoad\":\"1\",\"MazeHpAddvalue\":0,\"MazeAtkAddValue\":10,\"MazeCostAddValue\":10,\"InChallingingStageCount\":\"1-1\"}");
                    }
                    else
                    {
                        File.Delete(GetFilePath(keyArr[i]));
                    }
                }
            }
        }
    }
}
