using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SynergyClass
{
    public class FAINTMusicalNoteGainUpImpact : SynergyImpact
    {
        public override void ImpactExcution()
        {
            foreach (CardBase card in StageManager.Instanace.SelectDeck)
            {
                MusicCardBase mc = card as MusicCardBase;

                if (mc != null && mc.stackType == MusicCardStackType.FAINTMusicalNote)
                    mc.additionStack += _ownerSynergy.ImpactCoefficient;
            }
        }
    }
}
