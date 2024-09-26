using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReforgingSkill : ForgingCardBase, ISkillEffectAnim
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
        StartCoroutine(AddStackCor());
        Player.OnAnimationCall -= HandleAnimationCall;
    }

    public void HandleEffectEnd()
    {
        Player.EndAbility();
        Player.VFXManager.EndParticle(CardInfo, (int)CombineLevel);
        IsActivingAbillity = false;
        Player.VFXManager.OnEndEffectEvent -= HandleEffectEnd;
    }

    private IEnumerator AddStackCor()
    {
        yield return new WaitForSeconds(0.3f);

        Debug.Log($"Before Forging Stack: {Player.BuffStatCompo.GetStack(StackEnum.Forging)}");
        AddStack();
        Debug.Log($"After Forging Stack: {Player.BuffStatCompo.GetStack(StackEnum.Forging)}");

        CombatMarkingData forgeData =
        new CombatMarkingData(BuffingType.Smelting,
        "진정한 강철은 수만번의 담금질 속에서 만들어진다.",
        int.MaxValue);

        BattleReader.CombatMarkManagement.AddBuffingData(Player, CardID, forgeData, buffSO.stackBuffs[0].values[(int)CombineLevel] + additionStack);
    }
}
