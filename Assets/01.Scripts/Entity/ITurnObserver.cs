using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnObserver
{
    public void OnTurnStart();
    public void OnTurnEnd();
}
