using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAddCostBuff : SpecialBuff
{
    public int addCostValue = 3;

    public override void UpdateBuff(int level)
    {
        base.UpdateBuff(level);
        CostCalculator.GetCost(addCostValue);
        SetIsComplete(true);
    }
}
