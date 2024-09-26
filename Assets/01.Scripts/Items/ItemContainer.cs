using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    private Dictionary<string, ItemDataSO> _itemDataDic = new();
    private Dictionary<string, ItemDataBreadSO> _cakeDataDic = new();

    [SerializeField] private ItemDataSO[] _itemDataArr;
    [SerializeField] private ItemDataBreadSO[] _cakeDataArr;

    private void Awake()
    {
        foreach (var itemData in _itemDataArr)
        {
            _itemDataDic.Add(itemData.itemName, itemData);
        }

        foreach(var cake in _cakeDataArr)
        {
            _cakeDataDic.Add(cake.itemName, cake);
            _itemDataDic.Add(cake.itemName, cake);
        }
    }

    public ItemDataSO GetItemDataByName(string name)
    {
        return _itemDataDic[name];
    }

}
