using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SynergyClass
{
    public class DefDownDebuffImpact : BuffAndDebuffImpactBase
    {
        public override void ImpactExcution()
        {
            ApplyBuffOrDebuffToAllEnemy();
        }
    }
}
