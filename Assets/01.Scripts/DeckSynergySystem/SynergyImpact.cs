using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SynergyClass
{
    public abstract class SynergyImpact
    {
        protected Player _player = BattleController.Instance.Player;
        protected Synergy _ownerSynergy;

        public void SetOwnerSynergy(Synergy ownerSynergy) { _ownerSynergy = ownerSynergy; }
        public abstract void ImpactExcution();
    }
}