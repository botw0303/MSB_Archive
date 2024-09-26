using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SynergyClass
{
    public class AtkUpBuffImpact : BuffAndDebuffImpactBase
    {
        public override void ImpactExcution()
        {
            ApplyBuffOrDebuffToOneEntity(_player);
        }
    }
}
