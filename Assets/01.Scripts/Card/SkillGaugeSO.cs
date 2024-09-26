using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skill/SkillGaugeData")]
public class SkillGaugeSO : ScriptableObject
{
    [Header("Gauge Info")]
    public string gaugeName;        // ������ �̸�
    public string gaugeDesc;        // ������ ����
    [Space]
    public GaugeType gaugeType;     // ������ ����
    public int gaugeMax;            // ������ �ִ�ġ
    public int gaugeMin;            // ������ �ּ�ġ
}
