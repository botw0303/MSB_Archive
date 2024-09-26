using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaggieLanternSkill : CardBase, ISkillEffectAnim
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
        Player.VFXManager.PlayParticle(this, Player.transform.position,true);
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

        yield return new WaitForSeconds(1.7f);

        FeedbackManager.Instance.ShakeScreen(3);
        foreach (var e in Player.GetSkillTargetEnemyList[this])
        {
            e?.HealthCompo.ApplyDamage(GetDamage(CombineLevel), Player,KnockBackType.KnockBack);
            //�켱 �ӽ÷� ¥�Ӵϴ�. ���߿� ��ĥ �� ������ ��ĥ�Կ�
            if(e != null)
            {
                GameObject obj = Instantiate(CardInfo.hitEffect.gameObject, e.transform.position, Quaternion.identity);
                Destroy(obj, 1.0f);
            }
        }
    }
}
