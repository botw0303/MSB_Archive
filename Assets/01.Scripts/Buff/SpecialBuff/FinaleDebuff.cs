using CardDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinaleDebuff : SpecialBuff, IOnEndSkill
{
    private int duration;
    public List<int> defDebuffValues;
    public List<int> dmgDebuffValues;

    public override void Refresh(int level)
    {
        for (int i = 0; i < entity.target.BuffStatCompo.GetStack(StackEnum.DEFMusicalNote); ++i)
        {
            entity.CharStat.DecreaseStatBy(defDebuffValues[combineLevel], entity.CharStat.GetStatByType(StatType.armor));
        }
        for (int i = 0; i < entity.target.BuffStatCompo.GetStack(StackEnum.DMGMusicaldNote); ++i)
        {
            entity.CharStat.DecreaseStatBy(dmgDebuffValues[combineLevel], entity.CharStat.GetStatByType(StatType.receivedDmgIncreaseValue));
        }
        base.Refresh(level);
        for (int i = 0; i < entity.target.BuffStatCompo.GetStack(StackEnum.DEFMusicalNote); ++i)
        {
            entity.CharStat.IncreaseStatBy(defDebuffValues[combineLevel], entity.CharStat.GetStatByType(StatType.armor));
        }
        for (int i = 0; i < entity.target.BuffStatCompo.GetStack(StackEnum.DMGMusicaldNote); ++i)
        {
            entity.CharStat.IncreaseStatBy(dmgDebuffValues[combineLevel], entity.CharStat.GetStatByType(StatType.receivedDmgIncreaseValue));
        }

        int faintStackCnt = entity.target.BuffStatCompo.GetStack(StackEnum.FAINTMusicalNote) / 5;
        if (entity.HealthCompo.AilmentStat.HasAilment(AilmentEnum.Faint))
        {
            entity.HealthCompo.AilmentStat.SetAilment(AilmentEnum.Faint, faintStackCnt);
        }
        else
        {
            entity.HealthCompo.AilmentStat.ApplyAilments(AilmentEnum.Faint, faintStackCnt);
        }
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

    public override void EndBuff()
    {
        base.EndBuff();
        for (int i = 0; i < entity.target.BuffStatCompo.GetStack(StackEnum.DEFMusicalNote); ++i)
        {
            entity.CharStat.DecreaseStatBy(defDebuffValues[combineLevel], entity.CharStat.GetStatByType(StatType.armor));
        }
        for (int i = 0; i < entity.target.BuffStatCompo.GetStack(StackEnum.DMGMusicaldNote); ++i)
        {
            entity.CharStat.DecreaseStatBy(dmgDebuffValues[combineLevel], entity.CharStat.GetStatByType(StatType.receivedDmgIncreaseValue));
        }
    }

    public override void SetIsComplete(bool value)
    {
        base.SetIsComplete(value);
    }

    public override void Init()
    {
        base.Init();
        duration = combineLevel + 2;
        EndSkill();
    }

    public void EndSkill()
    {
        Refresh(combineLevel);
    }
}