using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CakeRank
{
    Normal = 1,
    Epic = 3,
    Legend= 5
}

[System.Serializable]
public class CakeData
{
    public string CakeName;
    public bool IsFavorites;
    public int Count = 1;

    public CakeRank Rank;

    public CakeData(string _cakeName,  bool _isFavorites)
    {
        CakeName = _cakeName;
        IsFavorites = _isFavorites;
    }
}

public class BakeryData : CanSaveData
{
    public List<CakeData> CakeDataList = new List<CakeData>();

    public override void SetInitialValue()
    {

    }
}
