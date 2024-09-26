using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatMarkManagement : MonoBehaviour 
{
    private Dictionary<int, List<(Entity, CombatMarkingData)>> _markingDataDic = new();

    public List<Entity> GetEntityOnTarget(int cardID)
    {
        List<Entity> value = _markingDataDic[cardID].Select(x=> x.Item1).ToList();
        return value;
    }

    public void AddBuffingData(Entity entity, int cardID, CombatMarkingData markingData, int count = 1)
    {
        if(entity.HealthCompo.IsDead) return;

        Debug.Log(cardID);
        entity.BuffSetter.AddBuffingMark(markingData);

        if (!_markingDataDic.ContainsKey(cardID))
        {
            _markingDataDic.Add(cardID, new List<(Entity, CombatMarkingData)>());
        }
        _markingDataDic[cardID].Add((entity, markingData));
    }

    public void RemoveBuffingData(Entity entity, int cardID, BuffingType markingType, int count = 1)
    {
        if (!_markingDataDic.ContainsKey(cardID)) return;

        var data =
        _markingDataDic[cardID].FirstOrDefault(x => x.Item2.buffingType == markingType);

        if (data.Item2 == null)
        {
            Debug.LogError($"Error : Cant Remove : {markingType}Data, It dose not exist");
            return;
        }

        entity.BuffSetter.RemoveBuffingMark(data.Item2);
        _markingDataDic[cardID].Remove(data);
    }
}
