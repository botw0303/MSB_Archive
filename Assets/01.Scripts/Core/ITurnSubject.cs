using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnSubject
{
	public void RegisterObserver(ITurnObserver observer);
	public void RemoveObserver(ITurnObserver observer);
	public void TurnStart();
	public void TurnEnd();
}
