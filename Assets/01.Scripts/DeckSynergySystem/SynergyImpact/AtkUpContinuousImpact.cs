using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SynergyClass
{
    public class AtkUpContinuousImpact : SynergyImpact
    {
        public override void ImpactExcution()
        {
            _player.CharStat.damage.AddModifier(_ownerSynergy.ImpactCoefficient);
        }
    }
}