using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinaleSkill : MusicCardBase, ISkillEffectAnim
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
        Player.VFXManager.PlayParticle(CardInfo, (int)CombineLevel, _skillDurations[(int)CombineLevel]);
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
        yield return new WaitForSeconds(9.5f);

        foreach(var e in Player.GetSkillTargetEnemyList[this])
        {
            e?.HealthCompo.ApplyDamage(GetDamage(CombineLevel) + (Player.BuffStatCompo.GetStack(StackEnum.DEFMusicalNote) * 2) + (Player.BuffStatCompo.GetStack(StackEnum.DMGMusicaldNote) * 2), Player);
            if(e != null)
            {
                GameObject obj = Instantiate(CardInfo.hitEffect.gameObject, Player.GetSkillTargetEnemyList[this][0].transform.position, Quaternion.identity);
                Destroy(obj, 1.0f);
            }
        }

        if (GetNoteCount() > 0)
        {
            ApplyDebuffToAllEnemy();

            Player.BuffStatCompo.ClearStack(StackEnum.DEFMusicalNote);
            Player.BuffStatCompo.ClearStack(StackEnum.DMGMusicaldNote);
            Player.BuffStatCompo.ClearStack(StackEnum.FAINTMusicalNote);

            Player.BuffSetter.RemoveSpecificBuffingType(BuffingType.MusicDef);
            Player.BuffSetter.RemoveSpecificBuffingType(BuffingType.MusicAtk);
            Player.BuffSetter.RemoveSpecificBuffingType(BuffingType.MusicFaint);
        }
    }
}
