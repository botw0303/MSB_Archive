using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FermataSkill : MusicCardBase, ISkillEffectAnim
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
        yield return new WaitForSeconds(2.4f);

        foreach (var e in Player.GetSkillTargetEnemyList[this])
        {
            e?.HealthCompo.ApplyDamage(GetDamage(CombineLevel), Player);
            if(e != null)
            {
                GameObject obj = Instantiate(CardInfo.hitEffect.gameObject, Player.GetSkillTargetEnemyList[this][0].transform.position, Quaternion.identity);
                Destroy(obj, 1.0f);
            }
        }

        AddFAINTMusicalNoteStack();
        Debug.Log($"Stacks: DEF({Player.BuffStatCompo.GetStack(StackEnum.DEFMusicalNote)}) / DMG({Player.BuffStatCompo.GetStack(StackEnum.DMGMusicaldNote)}) / FAINT({Player.BuffStatCompo.GetStack(StackEnum.FAINTMusicalNote)})");

        CombatMarkingData data = new CombatMarkingData(BuffingType.MusicFaint, buffSO.buffInfo, (int)CombineLevel + 1);

        BattleReader.CombatMarkManagement.AddBuffingData(Player, CardID, data, buffSO.stackBuffs[0].values[(int)CombineLevel]);
    }
}
