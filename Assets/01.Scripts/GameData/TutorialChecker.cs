using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChecker : CanSaveData
{
    public bool isFirstStartGame = true;

    public override void SetInitialValue()
    {
        TutorialChecker tc = DataManager.Instance.LoadData<TutorialChecker>(DataKeyList.tutorialDataKey);
    }
}
