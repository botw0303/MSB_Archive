using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiBird : Enemy
{
    private ThrowController _throwKiwi;
    private int _animCatchHash = Animator.StringToHash("catchKiwi");
    [SerializeField]private Transform kiwiSpawnTrm;

	public override void Init()
	{
	}

}
