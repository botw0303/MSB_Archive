using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chameleon : Enemy
{
    private ThrowController _throwMelon;
    [SerializeField] private Transform melonSpawnTrm;

	public override void Init()
	{
	}


	private void ThrowMelon()
    {
        _throwMelon = PoolManager.Instance.Pop(PoolingType.ThrowMelon) as ThrowController;
        _throwMelon.transform.position = melonSpawnTrm.position;
        //_throwMelon.Throw(this, target, AttackEnd);
    }
}
