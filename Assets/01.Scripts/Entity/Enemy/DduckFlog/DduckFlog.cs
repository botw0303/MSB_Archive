using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DduckFlog : Enemy
{
    private int rollAnimHash = Animator.StringToHash("roll");

	public override void Init()
	{
	}


	protected override void Awake()
    {
        base.Awake();
    }
}
