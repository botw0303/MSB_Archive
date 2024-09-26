using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningRainSkill : LightningCardBase, ISkillEffectAnim
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
        SoundManager.PlayAudio(_soundEffect, false);
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
        yield return new WaitForSeconds(0.2f);

        var targetList = Player.GetSkillTargetEnemyList[this];

        for (int i = 0; i < 5; ++i)
        {
            foreach (var e in targetList)
            {
                e?.HealthCompo.ApplyDamage(GetDamage(CombineLevel), Player);
                if (e != null)
                {
                    GameObject obj = Instantiate(CardInfo.hitEffect.gameObject, targetList[0].transform.position, Quaternion.identity);
                    Destroy(obj, 1.0f);
                }
            }
            yield return new WaitForSeconds(0.13f);
        }

        if(targetList.Count > 0)
        {
            ExtraAttack(targetList[targetList.Count - 1]);
        }
        
        foreach(var e in targetList)
        {
            RandomApplyShockedAilment(e, 20f);
        }
    }
}
