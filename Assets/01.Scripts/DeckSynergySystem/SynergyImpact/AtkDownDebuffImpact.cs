using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SynergyClass
{
    public class AtkDownDebuffImpact : BuffAndDebuffImpactBase
    {
        public override void ImpactExcution()
        {
            ApplyBuffOrDebuffToAllEnemy();
        }
    }
}
