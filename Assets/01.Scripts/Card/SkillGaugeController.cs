using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GaugeType
{
    //Single,         // 기본적인 게이지    
    ContrastGauge,  // 한 게이지 바를 두 종류의 게이지가 나눠 사용함. A가 상승하면 B가 줄어드는 형식

}

public class SkillGaugeController : MonoSingleton<SkillGaugeController>
{
    [SerializeField] private List<SkillGaugeSO> gaugeSOList;    // 있는 SO 다 넣어둬야함
    private Dictionary<GaugeType, SkillGauge> skillGaugeDic = new Dictionary<GaugeType, SkillGauge>(); // 이건 알아서 채워줌

    private Player _player;

    public void Initialize(Player player)
    {
        _player = player;
        _player.OnIncreaseSkillGauge += HandleExcutionIncreaseGauge;
        _player.OnDecreaseSkillGauge += HandleExcutionDecreaseGauge;

        foreach (GaugeType gaugeType in Enum.GetValues(typeof(GaugeType)))
        {
            string typeName = gaugeType.ToString();
            Type t = Type.GetType($"{typeName}");

            if (t != null)
            {
                SkillGauge skillGauge = Activator.CreateInstance(t) as SkillGauge;
                SkillGaugeSO dataSO = gaugeSOList.Find(gauge => gauge.gaugeType == gaugeType);
                if (dataSO != null)
                {
                    skillGauge.Initialize(dataSO);
                }
                skillGaugeDic.Add(gaugeType, skillGauge);
                Debug.Log(skillGaugeDic[gaugeType] == null);
            }
        }

        //foreach(SkillGaugeSO so in gaugeSOList)
        //{
        //    SkillGauge gauge = new SkillGauge();
        //    gauge.Initialize(so);
        //    skillGaugeDic.Add(so, gauge);
        //}

        SetGauges();
    }

    public void EventsFinalize()
    {
        _player.OnIncreaseSkillGauge -= HandleExcutionIncreaseGauge;
        _player.OnDecreaseSkillGauge -= HandleExcutionDecreaseGauge;
    }

    public void SetGauges()
    {
        foreach (CardBase card in StageManager.Instanace.SelectDeck)
        {
            if (card.gameObject.TryGetComponent<IGaugeSkill>(out IGaugeSkill gaugeSkill))
            {
                SkillGauge result = _player.skillGaugeList.Find(gauge => gauge.Data.gaugeName == gaugeSkill.GaugeSO.gaugeName);
                if (result == null)
                {
                    Debug.Log(gaugeSkill.GaugeSO.gaugeType);
                    Debug.Log(skillGaugeDic[gaugeSkill.GaugeSO.gaugeType] == null);
                    _player.skillGaugeList.Add(skillGaugeDic[gaugeSkill.GaugeSO.gaugeType]);
                }
            }
        }
    }

    public SkillGauge GetSkillGauge(SkillGaugeSO gaugeSO)
    {
        if (skillGaugeDic.TryGetValue(gaugeSO.gaugeType, out SkillGauge gauge))
            return gauge;
        else
        {
            Debug.Log($"SkillGaugeController is not has {gaugeSO.gaugeName} gauge. Check the 'skillGaugDic'");
            return null;
        }
    }

    public void HandleExcutionIncreaseGauge(SkillGaugeSO gaugeSO, int value)
    {
        GetSkillGauge(gaugeSO).HandleIncreaseGauge(value);
    }

    public void HandleExcutionDecreaseGauge(SkillGaugeSO gaugeSO, int value)
    {
        GetSkillGauge(gaugeSO).HandleDecreaseGauge(value);
    }
}
