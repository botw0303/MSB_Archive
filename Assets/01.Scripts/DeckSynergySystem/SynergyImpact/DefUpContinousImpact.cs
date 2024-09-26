using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SynergyClass
{
    public class DefUpContinousImpact : SynergyImpact
    {
        public override void ImpactExcution()
        {
            _player.CharStat.armor.AddModifier(_ownerSynergy.ImpactCoefficient);
        }
    }
}
