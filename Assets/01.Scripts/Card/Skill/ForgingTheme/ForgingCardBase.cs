using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ForgingCardBase : CardBase, IEndowStackSkill
{
    public int additionStack;

    protected void AddStack()
    {
        Debug.Log($"{buffSO.stackBuffs[0].values[(int)CombineLevel]} + {additionStack} = {buffSO.stackBuffs[0].values[(int)CombineLevel] + additionStack}");
        Player.BuffStatCompo.AddStack(StackEnum.Forging, buffSO.stackBuffs[0].values[(int)CombineLevel] + additionStack);
    }

    public void ResetAdditionStack()
    {
        additionStack = 0;
    }
}
