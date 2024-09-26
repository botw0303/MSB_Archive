using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingItemData : CanSaveData
{
    public List<string> itemDataList = new ();

    public override void SetInitialValue()
    {
    }
}
