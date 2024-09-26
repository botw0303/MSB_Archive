using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingRodBuff : SpecialBuff, IOnEndSkill
{
    private int duration;
    public List<int> dmgValues;
    public List<int> durationValues;

    public override void Refresh(int level)
    {
        //���� ���� ��ŭ ���� ���� �Ȱ� ����
        for (int i = 0; i < entity.BuffStatCompo.GetStack(StackEnum.Lightning); i++)
            entity.CharStat.DecreaseStatBy(dmgValues[combineLevel], entity.CharStat.GetStatByType(StatType.damage));
        base.Refresh(level);
        //���� ��ŭ �ٲ� ���� �������� �ֱ�
        for (int i = 0; i < entity.BuffStatCompo.GetStack(StackEnum.Lightning); i++)
            entity.CharStat.IncreaseStatBy(dmgValues[combineLevel], entity.CharStat.GetStatByType(StatType.damage));
    }
    public override void UpdateBuff(int level)
    {
        base.UpdateBuff(level);
        duration--;
        if (duration <= 0)
        {
            SetIsComplete(true);
        }
    }
    public void EndSkill()
    {
        foreach (var e in BattleController.Instance.OnFieldMonsterArr)
        {
            if(e != null)
            {
                if (e.HealthCompo.AilmentStat.HasAilment(AilmentEnum.Shocked))
                {
                    entity.BuffStatCompo.AddStack(StackEnum.Lightning, 1);
                    e.HealthCompo.AilmentStat.CuredAilment(AilmentEnum.Shocked);
                    entity.CharStat.IncreaseStatBy(dmgValues[combineLevel], entity.CharStat.GetStatByType(StatType.damage));
                }
            }
        }
    }

    public override void EndBuff()
    {
        base.EndBuff();
        for (int i = 0; i < entity.BuffStatCompo.GetStack(StackEnum.Lightning); i++)
            entity.CharStat.DecreaseStatBy(dmgValues[combineLevel], entity.CharStat.GetStatByType(StatType.damage));
    }

    public override void SetIsComplete(bool value)
    {
        entity.CharStat.DecreaseStatBy(dmgValues[combineLevel], entity.CharStat.GetStatByType(StatType.damage));
        //��������
        base.SetIsComplete(value);
    }
    public override void Init()
    {
        base.Init();
        duration = durationValues[combineLevel];
        EndSkill();
    }
}
