using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SynergyClass
{
    public class DefUpBuffImpact : BuffAndDebuffImpactBase
    {
        public override void ImpactExcution()
        {
            ApplyBuffOrDebuffToOneEntity(_player);
        }
    }
}
