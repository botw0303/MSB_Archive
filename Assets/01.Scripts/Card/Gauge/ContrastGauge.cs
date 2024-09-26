using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContrastGauge : SkillGauge
{
    public override void Initialize(SkillGaugeSO dataSO)
    {
        base.Initialize(dataSO);

        gaugeValue = data.gaugeMax / 2;
    }

    public int GetFirstGauge()
    {
        return gaugeValue;
    }

    public int GetSecondsGauge()
    {
        return data.gaugeMax - gaugeValue;
    }
}
