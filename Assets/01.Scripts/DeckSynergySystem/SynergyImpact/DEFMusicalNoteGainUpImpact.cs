using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SynergyClass
{
    public class DEFMusicalNoteGainUpImpact : SynergyImpact
    {
        public override void ImpactExcution()
        {
            foreach (CardBase card in BattleReader.InDeckCardList)
            {
                MusicCardBase mc = card as MusicCardBase;

                if (mc != null && mc.stackType == MusicCardStackType.DEFMusicalNote)
                {
                    mc.additionStack += _ownerSynergy.ImpactCoefficient;
                }
            }
        }
    }
}
