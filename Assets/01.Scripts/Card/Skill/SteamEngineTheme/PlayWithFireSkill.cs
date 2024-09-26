﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWithFireSkill : SteamEngineCardBase, ISkillEffectAnim
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
        Player.VFXManager.PlayWaterAndFireHarpoonParticle(this, Player.transform.position, true);
        StartCoroutine(AttackCor());
        //Player.BuffStatCompo.AddStack(StackEnum.DEFMusicalNote, buffSO.stackBuffs[0].values[(int)CombineLevel]);
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
        // ���� �׽�Ʈ �غ�����

        yield return new WaitForSeconds(1f);

        //Player.VFXManager.PlayPianissimoParticle(this, Player.transform.position, true);

        //List<Entity> TEList = Player.GetSkillTargetEnemyList[this];

        //for(int i = 0; i < 2; ++i)
        //{
        //    Entity e = TEList[i % TEList.Count];

        //    e?.HealthCompo.ApplyDamage(GetDamage(CombineLevel), Player);
        //    if(e != null)
        //    {
        //        GameObject obj = Instantiate(CardInfo.hitEffect.gameObject, e.transform.position, Quaternion.identity);
        //        Destroy(obj, 1.0f);
        //    }
        //    yield return new WaitForSeconds(0.4f);
        //}

        IncreaseGauge(GaugeSO, 10);
    }
}
