using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SynergyClass
{
    public class ForgingStackGainUpImpact : SynergyImpact
    {
        public override void ImpactExcution()
        {
            foreach(CardBase card in BattleReader.InDeckCardList)
            {
                ForgingCardBase fc = card as ForgingCardBase;

                if(fc != null)
                {
                    fc.additionStack += _ownerSynergy.ImpactCoefficient;
                }
            }
        }
    }
}
