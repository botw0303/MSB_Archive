using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverSkill : ForgingCardBase, ISkillEffectAnim
{
    public override void Abillity()
    {
        IsActivingAbillity = true;

        Player.UseAbility(this);
        Player.OnAnimationCall += HandleAnimationCall;
        Player.VFXManager.OnEndEffectEvent += HandleEffectEnd;

        Player.BuffSetter.RemoveSpecificBuffingType(BuffingType.Smelting);
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
        yield return new WaitForSeconds(7.3f);

        foreach (var e in Player.GetSkillTargetEnemyList[this])
        {
            e?.HealthCompo.ApplyDamage(GetDamage(CombineLevel), Player);
            if (e != null)
            {
                GameObject obj = Instantiate(CardInfo.hitEffect.gameObject, e.transform.position, Quaternion.identity);
                Destroy(obj, 1.0f);
            }
        }

        yield return new WaitForSeconds(3.6f);

        foreach(var e in Player.GetSkillTargetEnemyList[this])
        {
            e?.HealthCompo.ApplyDamage(Mathf.RoundToInt((Player.CharStat.GetDamage() * 0.03f)) * Player.BuffStatCompo.GetStack(StackEnum.Forging), Player);
            if(e != null)
            {
                GameObject obj = Instantiate(CardInfo.hitEffect.gameObject, e.transform.position, Quaternion.identity);
                Destroy(obj, 1.0f);
            }
        }
    }
}
