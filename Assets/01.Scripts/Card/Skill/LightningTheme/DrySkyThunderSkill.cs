using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrySkyThunderSkill : LightningCardBase, ISkillEffectAnim
{
    public override void Abillity()
    {
        IsActivingAbillity = true;

        StartCoroutine(AttackCor());

        // 0.2 sec wait


        Player.OnAnimationCall += HandleAnimationCall;
        Player.VFXManager.OnEndEffectEvent += HandleEffectEnd;
    }

    public void HandleAnimationCall()
    {
        Player.OnAnimationCall -= HandleAnimationCall;
    }

    public void HandleEffectEnd()
    {
		Player.MoveToOriginPos();
		Player.EndAbility();
        Player.VFXManager.EndParticle(CardInfo, (int)CombineLevel);
        IsActivingAbillity = false;
        Player.VFXManager.OnEndEffectEvent -= HandleEffectEnd;
    }

    private IEnumerator AttackCor()
    {
        Player.VFXManager.PlayParticle(CardInfo, Player.transform.position, (int)CombineLevel, _skillDurations[(int)CombineLevel]);
        SoundManager.PlayAudio(_soundEffect, false);

        yield return new WaitForSeconds(1.25f);

		Player.UseAbility(this, true, false, true, 0.1f);

		var targetList = Player.GetSkillTargetEnemyList[this];

        yield return new WaitForSeconds(0.65f);

        foreach (var e in targetList)
        {
            e?.HealthCompo.ApplyDamage(GetDamage(CombineLevel), Player);
            if (e != null)
            {
                GameObject obj = Instantiate(CardInfo.hitEffect.gameObject, targetList[0].transform.position, Quaternion.identity);
                Destroy(obj, 1.0f);
                RandomApplyShockedAilment(e, 20f);
            }
        }

        if (targetList.Count > 0)
        {
            ExtraAttack(targetList[targetList.Count - 1]);
        }
    }
}
