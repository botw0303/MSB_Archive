using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SynergyClass
{
    [CreateAssetMenu(menuName = "SO/Synergy/SynergyImpact")]
    public class Synergy : ScriptableObject
    {
        public string SynergyName;              // Name of synergy
        [TextArea]
        public string SynergyDesc;              // Description of synergy
        public int ImpactCoefficient;           // Buff's turn, Buff's increase value, stat increase value etc...
        public BuffSO BuffSO;                   // Buff to apply for if this synergy's impact has buff or debuff
        public SynergyImpactType ImpactType;    // Type for check to what effect of this impact
        public SynergyImpact SynergyImpact;     // Synergy's effect
        public int ConditionCheckValue;         // Value that determines how much a condition must be satisfied
        public List<CardBase> ConditionCards;   // Condition cards that must be satisfied
        public bool Enable;                     // Whether the conditions for synergy are met
    }
}