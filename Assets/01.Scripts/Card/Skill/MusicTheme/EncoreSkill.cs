using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncoreSkill : MusicCardBase, ISkillEffectAnim
{
    public override void Abillity()
    {
        IsActivingAbillity = true;

        Player.UseAbility(this);
        Player.OnAnimationCall += HandleAnimationCall;
        Player.VFXManager.OnEndEffectEvent += HandleEffectEnd;
    }

    public void HandleAnimationCall()
    {
        Player.VFXManager.PlayParticle(CardInfo, (int)CombineLevel, _skillDurations[(int)CombineLevel]);
        SoundManager.PlayAudio(_soundEffect, true);
        StartCoroutine(AttackCor());
        Player.OnAnimationCall -= HandleAnimationCall;
    }

    public void HandleEffectEnd()
    {
        Player.EndAbility();
        Player.VFXManager.EndParticle(CardInfo, (int)CombineLevel);
        IsActivingAbillity = false;
        Player.VFXManager.OnEndEffectEvent -= HandleEffectEnd;
    }

    private IEnumerator AttackCor()
    {
        yield return new WaitForSeconds(2.45f);

        Player.BuffStatCompo.AddStack(StackEnum.DEFMusicalNote, buffSO.stackBuffs[0].values[(int)CombineLevel]);
        Player.BuffStatCompo.AddStack(StackEnum.DMGMusicaldNote, buffSO.stackBuffs[0].values[(int)CombineLevel]);
        Player.BuffStatCompo.AddStack(StackEnum.FAINTMusicalNote, buffSO.stackBuffs[0].values[(int)CombineLevel]);

        CombatMarkingData d_data = new CombatMarkingData(BuffingType.MusicDef,
                                 buffSO.buffInfo, (int)CombineLevel + 1);
        BattleReader.CombatMarkManagement.AddBuffingData(Player, CardID, d_data, buffSO.stackBuffs[0].values[(int)CombineLevel]);

        CombatMarkingData a_data = new CombatMarkingData(BuffingType.MusicAtk,
                                 buffSO.buffInfo, (int)CombineLevel + 1);
        BattleReader.CombatMarkManagement.AddBuffingData(Player, CardID, a_data, buffSO.stackBuffs[0].values[(int)CombineLevel]);

        CombatMarkingData f_data = new CombatMarkingData(BuffingType.MusicFaint,
                                 buffSO.buffInfo, (int)CombineLevel + 1);
        BattleReader.CombatMarkManagement.AddBuffingData(Player, CardID, f_data, buffSO.stackBuffs[0].values[(int)CombineLevel]);
    }
}
