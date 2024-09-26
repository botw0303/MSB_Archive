using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillGauge
{
    protected SkillGaugeSO data;
    public SkillGaugeSO Data => data;

    public int gaugeValue;

    public virtual void Initialize(SkillGaugeSO dataSO)
    {
        data = dataSO;

        gaugeValue = 0;
    }

    public virtual void HandleIncreaseGauge(int increaseValue)
    {
        gaugeValue = Mathf.Clamp(gaugeValue + increaseValue, data.gaugeMin, data.gaugeMax);
    }

    public virtual void HandleDecreaseGauge(int decreaseValue)
    {
        gaugeValue = Mathf.Clamp(gaugeValue - decreaseValue, data.gaugeMin, data.gaugeMax);
    }
}
