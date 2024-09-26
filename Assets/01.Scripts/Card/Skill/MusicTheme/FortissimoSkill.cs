using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortissimoSkill : MusicCardBase, ISkillEffectAnim
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
        SoundManager.PlayAudio(_soundEffect, true);
        //Player.VFXManager.PlayParticle(CardInfo, (int)CombineLevel, _skillDurations[(int)CombineLevel]);
        Player.VFXManager.PlayParticle(this, Player.GetSkillTargetEnemyList[this][0].transform.position, true);
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
        yield return new WaitForSeconds(3.02f);

        Player.GetSkillTargetEnemyList[this][0]?.HealthCompo.ApplyDamage(GetDamage(CombineLevel), Player);
        if(Player.GetSkillTargetEnemyList[this][0] != null)
        {
            GameObject obj = Instantiate(CardInfo.hitEffect.gameObject, Player.GetSkillTargetEnemyList[this][0].transform.position, Quaternion.identity);
            Destroy(obj, 1.0f);
        }

        AddDMGMusicalNoteStack();
        Debug.Log($"Stacks: DEF({Player.BuffStatCompo.GetStack(StackEnum.DEFMusicalNote)}) / DMG({Player.BuffStatCompo.GetStack(StackEnum.DMGMusicaldNote)}) / FAINT({Player.BuffStatCompo.GetStack(StackEnum.FAINTMusicalNote)})");

        CombatMarkingData data = new CombatMarkingData(BuffingType.MusicAtk,
                                 buffSO.buffInfo, (int)CombineLevel + 1);

        BattleReader.CombatMarkManagement.AddBuffingData(Player, CardID, data, buffSO.stackBuffs[0].values[(int)CombineLevel]);
    }
}
