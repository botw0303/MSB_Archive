using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuenchingSkill : ForgingCardBase, ISkillEffectAnim
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
        StartCoroutine(SkillCor());
        Player.OnAnimationCall -= HandleAnimationCall;
    }

    public void HandleEffectEnd()
    {
        Player.EndAbility();
        Player.VFXManager.EndParticle(CardInfo, (int)CombineLevel);
        IsActivingAbillity = false;
        Player.VFXManager.OnEndEffectEvent -= HandleEffectEnd;
    }

    private IEnumerator SkillCor()
    {
        yield return new WaitForSeconds(0.3f);

        Debug.Log($"Before Forging Stack: {Player.BuffStatCompo.GetStack(StackEnum.Forging)}");
        AddStack();
        Debug.Log($"After Forging Stack: {Player.BuffStatCompo.GetStack(StackEnum.Forging)}");
        Player.BuffStatCompo.AddBuff(buffSO, 2, (int)CombineLevel);

        CombatMarkingData forgeData =
        new CombatMarkingData(BuffingType.Smelting,
        "������ ��ö�� �������� ����� �ӿ��� ���������.",
        int.MaxValue);

        BattleReader.CombatMarkManagement.AddBuffingData(Player, CardID, forgeData, buffSO.stackBuffs[0].values[(int)CombineLevel] + additionStack);

        Debug.Log($"Current Forging Stat: {Player.BuffStatCompo.GetStack(StackEnum.Forging)}");
    }
}
