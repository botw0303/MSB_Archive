using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Inventory : MonoSingleton<Inventory>
{
    [SerializeField] private List<ItemDataSO> _inventoryList = new List<ItemDataSO>();

    public ExpansionList<ItemDataIngredientSO> GetIngredientInThisBattle { get; set; } = 
       new ExpansionList<ItemDataIngredientSO>();

    private ItemContainer _itemContainer;

    private void Start()
    {
        _itemContainer = GetComponent<ItemContainer>();

        SceneManager.sceneLoaded += HandleClearGetIngList;
        SceneManager.sceneLoaded += HandleSaveItems;

        SavingItemData data = new SavingItemData();
        if (DataManager.Instance.IsHaveData(DataKeyList.itemDataKey))
        {
            data = DataManager.Instance.LoadData<SavingItemData>(DataKeyList.itemDataKey);
        }

        foreach (var item in data.itemDataList)
        {
            _inventoryList.Add(_itemContainer.GetItemDataByName(item));
        }
    }

    private void HandleSaveItems(Scene arg0, LoadSceneMode mode)
    {
        List<string> itemNameList = _inventoryList.Select(i => i.itemName).ToList();
        SavingItemData data = new SavingItemData();
        data.itemDataList = itemNameList;
        DataManager.Instance.SaveData(data, DataKeyList.itemDataKey);
    }

    private void HandleClearGetIngList(Scene arg0, LoadSceneMode mode)
    {
        GetIngredientInThisBattle.Clear();
    }

    public List<ItemDataSO> GetSpecificTypeItemList(ItemType itemType)
    {
        return _inventoryList.Where(x => x.itemType == itemType).ToList();
    }

    public bool IsHaveItem(ItemDataSO itemData)
    {
        return _inventoryList.Contains(itemData);
    }

    public void AddItem(ItemDataSO item, int count = 1)
    { 
        if(_inventoryList.Contains(item))
        {
            item.haveCount += count;
            return;
        }

        item.haveCount = 1;
        _inventoryList.Add(item);
    }

    public void RemoveItem(ItemDataSO item, int count = 1)
    {
        if (_inventoryList.Contains(item))
        {
            item.haveCount = Mathf.Clamp(item.haveCount - count, 0, int.MaxValue);
            if(item.haveCount <= 0)
            {
                _inventoryList.Remove(item);
            }
        }
        else
        {
            Debug.LogError("아이템 없음!");
        }
    }

    public ItemDataSO GetItemInfo(string itemName)
    {
        return _itemContainer.GetItemDataByName(itemName);
    }

    public void SaveCurrentData()
    {
        SavingItemData data = new SavingItemData();
        List<string> list = _inventoryList.Select(i => i.itemName).ToList();
        data.itemDataList = list;

        DataManager.Instance.SaveData(data, DataKeyList.itemDataKey);
    }
}
