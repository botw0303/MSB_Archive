using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSubject : ITurnSubject
{
	private List<ITurnObserver> _turnObservers = new();

	public void RegisterObserver(ITurnObserver observer)
	{
		_turnObservers.Add(observer);
	}

	public void RemoveObserver(ITurnObserver observer)
	{
		_turnObservers.Remove(observer);
	}

	public void TurnEnd()
	{
		foreach (var observer in _turnObservers)
		{
			observer.OnTurnEnd();
		} 
	}

	public void TurnStart()
	{
		foreach (var observer in _turnObservers)
		{
			observer.OnTurnStart();
		}
	}
}
