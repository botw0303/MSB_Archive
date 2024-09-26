using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[Flags]
public enum CardShameType
{
    Damage = 1,
    Buff = 2,
    Debuff = 4,
    Cost = 8,
    Range = 16,
    Turn = 32
}

[Serializable]
public struct CardShameData 
{
    public CardShameType cardShameType;
    public int currentShame;
    public string info;

    public CardShameData(CardShameData data)
    {
        this = data;
    }
}

[Serializable]
public struct IncreaseValuePerLevel
{
    public CardShameType shameType;
    public int perValue;

    public IncreaseValuePerLevel(CardShameType _shameType, int _perValue)
    {
        shameType = _shameType;
        perValue = _perValue;
    }
}

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "SO/Card/Data")]
#endif
public class CardShameElementSO : ScriptableObject
{
    [Header("ī�� ����")]
    public int cardLevel = 1;
    public float cardExp;

    [Header("ī�� ��ġ ����")]
    public List<IncreaseValuePerLevel> cardLevelUppervalueGroup;
    public List<CardShameData> normalValueGroup;

    [Header("ī�� ��ġ [�޹��θ���Ʈ<��������Ʈ<����Ʈ<��ġ>>>]")]
    public List<SEList<SEList<CardShameData>>> cardShameDataList = new ();

    public void ReadData()
    {
        cardShameDataList.Clear ();

        for(int j = 1; j <= 3; j++)
        {
            SEList<SEList<CardShameData>> dataListGroup = new();
            dataListGroup.list = new List<SEList<CardShameData>>();

            for (int i = 0; i < 5; i++)
            {
                SEList<CardShameData> dataList = new SEList<CardShameData>();
                dataList.list = new List<CardShameData> ();
                foreach (var icvpl in cardLevelUppervalueGroup)
                {
                    foreach(CardShameData d in normalValueGroup)
                    {
                        CardShameData data = new CardShameData();

                        data.cardShameType = d.cardShameType;
                        if (d.cardShameType == icvpl.shameType)
                        {
                            data.currentShame = (d.currentShame + (icvpl.perValue * i)) * j;
                        }
                        else
                        {
                            data.currentShame = d.currentShame;
                        }

                        dataList.list.Add(data);
                    }
                }

                dataListGroup.list.Add(dataList);
            }

            cardShameDataList.Add(dataListGroup);
        }

        Debug.Log($"{cardShameDataList.Count} : Generate Complete :)");
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CardShameElementSO))]
public class GenerateCardShameElement : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CardShameElementSO cse = (CardShameElementSO)target;

        if (GUILayout.Button("CardShameDataReading"))
        {
            cse.ReadData();
        }
    }
}
#endif

