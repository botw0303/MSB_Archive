using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SynergyClass
{
    public abstract class BuffAndDebuffImpactBase : SynergyImpact
    {
        // Due to a buff structure issue, the buff turn is not applied.

        protected void ApplyBuffOrDebuffToOneEntity(Entity e)
        {
            e.BuffStatCompo.AddBuff(_ownerSynergy.BuffSO, _ownerSynergy.ImpactCoefficient, 0);
        }

        protected void ApplyBuffOrDebuffToAllEnemy()
        {
            foreach (Entity e in BattleController.Instance.OnFieldMonsterArr)
            {
                e?.BuffStatCompo.AddBuff(_ownerSynergy.BuffSO, _ownerSynergy.ImpactCoefficient, 0);
            }
        }
    }
}
