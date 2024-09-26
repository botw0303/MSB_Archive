using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skill/SkillGaugeData")]
public class SkillGaugeSO : ScriptableObject
{
    [Header("Gauge Info")]
    public string gaugeName;        // 게이지 이름
    public string gaugeDesc;        // 게이지 설명
    [Space]
    public GaugeType gaugeType;     // 게이지 형식
    public int gaugeMax;            // 게이지 최대치
    public int gaugeMin;            // 게이지 최소치
}
