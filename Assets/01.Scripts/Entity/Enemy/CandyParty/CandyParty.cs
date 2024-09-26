using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyParty : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        //VFXPlayer.OnEndEffect += () => turnStatus = TurnStatus.End;
    }

	public override void Init()
	{
	}

}
