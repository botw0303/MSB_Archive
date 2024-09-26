using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SteamSkillType
{
    Fire,
    Water,
    Steam,
}

public abstract class SteamEngineCardBase : CardBase, IGaugeSkill
{
    public SteamSkillType skillType;
    [SerializeField] private SkillGaugeSO _gaugeSO;
    public SkillGaugeSO GaugeSO => _gaugeSO;

    protected void IncreaseGauge(SkillGaugeSO gaugeSO, int increaseValue)
    {
        Player.OnIncreaseSkillGauge?.Invoke(gaugeSO, increaseValue);
    }

    protected void DecreaseGauge(SkillGaugeSO gaugeSO, int decreaseValue)
    {
        Player.OnDecreaseSkillGauge?.Invoke(gaugeSO, decreaseValue);
    }
}
