using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamBurstSkill : SteamEngineCardBase, ISkillEffectAnim
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
        //SoundManager.PlayAudio(_soundEffect, false);
        Player.VFXManager.PlayParticle(CardInfo, (int)CombineLevel, _skillDurations[(int)CombineLevel]);

        // 증기기관 상태 아니면 데미지 안 들어감
        //if(((ConstrastGauge)SkillGaugeController.Instance.GetSkillGauge(GaugeSO)).GetFirstGauge() >= 40
        //    && ((ConstrastGauge)SkillGaugeController.Instance.GetSkillGauge(GaugeSO)).GetFirstGauge() <= 60)
        //{
        //    StartCoroutine(AttackCor());
        //}

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
        yield return new WaitForSeconds(7f);

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
    }
}
