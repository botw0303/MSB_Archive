using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeringueSora : Enemy
{
    private int shellAnimHash = Animator.StringToHash("brokenshell");
    private int shellTriggerAnimHash = Animator.StringToHash("brokenshellTrigger");

    public BuffSO specialBuff;
    public bool haveShell
    {
        set
        {
            if (!value)
            {
                AnimatorCompo.SetBool(shellAnimHash, !value);
                AnimatorCompo.SetTrigger(shellTriggerAnimHash);
            }
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        BuffStatCompo.AddBuff(specialBuff, 0);
    }


	public override void Init()
	{
	}
}
