using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadStash : Stash
{
    public BreadStash(Transform slotParentTrm) : base(slotParentTrm)
    {
    }

    public override void AddItem(ItemDataSO item, int count = 1)
    {
        //�ߺ��� �������� CanAddItem���� �ɷ��ٲ��ϱ� ���⼭ �ɸ��� �ʴ´�.
        InventoryItem newItem = new InventoryItem(item);
        stash.Add(newItem);
        stashDictionary.Add(item, newItem);
    }

    public override void RemoveItem(ItemDataSO item, int count = 1)
    {
        if (stashDictionary.TryGetValue(item, out InventoryItem invenItem))
        {
            stash.Remove(invenItem);
            stashDictionary.Remove(item);
        }
    }

    public override bool CanAddItem(ItemDataSO itemData)
    {
        if (stashDictionary.ContainsKey(itemData))
        {
            Debug.Log("No more space or already exists!");
            return false;
        }
        return true;
    }

}