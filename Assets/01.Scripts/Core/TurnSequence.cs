using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSequence : LinkedList<ITurnAction>
{
	public event Action OnSequenceStart;
	public event Action OnSequenceEnd;
	public event Action OnStartAction;
	public event Action OnEndAction;

	public IEnumerator StartSequence(float startDelay, float delayTime)
	{
		yield return new WaitForSeconds(startDelay);
		WaitForSeconds waitCor = new(delayTime);
		OnSequenceStart?.Invoke();
		while (Count > 0)
		{
			ITurnAction work = First.Value;
			if (work.CanUse())
			{
				OnStartAction?.Invoke();
				yield return work.Execute();
				OnEndAction?.Invoke();
			}
			Remove(work);
			if (work.CanUse())
				yield return waitCor;
		}
		Clear();
		OnSequenceEnd?.Invoke();
	}
}
