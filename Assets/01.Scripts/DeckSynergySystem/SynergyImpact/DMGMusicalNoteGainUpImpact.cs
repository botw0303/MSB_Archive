using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SynergyClass
{
    public class DMGMusicalNoteGainUpImpact : SynergyImpact
    {
        public override void ImpactExcution()
        {
            foreach (CardBase card in StageManager.Instanace.SelectDeck)
            {
                MusicCardBase mc = card as MusicCardBase;

                if (mc != null && mc.stackType == MusicCardStackType.DMGMusicalNote)
                    mc.additionStack += _ownerSynergy.ImpactCoefficient;
            }
        }
    }
}
