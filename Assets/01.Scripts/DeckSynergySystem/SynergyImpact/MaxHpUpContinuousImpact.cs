using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SynergyClass
{
    public class MaxHpUpContinuousImpact : SynergyImpact
    {
        public override void ImpactExcution()
        {
            _player.HealthCompo.SetHp(_player.CharStat.maxHealth.GetValue() + _ownerSynergy.ImpactCoefficient);
        }
    }
}
