using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningJangSkill : LightningCardBase, ISkillEffectAnim
{
    public override void Abillity()
    {
        foreach (var e in Player.GetSkillTargetEnemyList[this])
        {
            if (e == null)
            {
                foreach (var fe in battleController.OnFieldMonsterArr)
                {
                    if (fe != null)
                    {
                        Player.GetSkillTargetEnemyList[this][0] = fe;
                        Player.target = fe;
                    }
                }
            }
        }

        IsActivingAbillity = true;
        targets = Player.GetSkillTargetEnemyList[this];
        Player.OnAnimationCall += HandleAnimationCall;
        Player.VFXManager.OnEndEffectEvent += HandleEffectEnd;
        Player.UseAbility(this, false, true);

        Player.VFXManager.SetBackgroundFadeOut(0.5f);

        if (targets.Count > 0)
        {
            //GameObject obj = Instantiate(CardInfo.hitEffect.gameObject, Player.target.transform.position, Quaternion.identity);
            //Destroy(obj, 1.0f);

            foreach (var e in battleController.OnFieldMonsterArr)
            {
                if (e != null)
                {
                    if (Player.GetSkillTargetEnemyList[this].Contains(e)) continue;
                    e.SpriteRendererCompo.DOColor(minimumColor, 0.5f);
                }
            }
        }
    }

    public void HandleAnimationCall()
    {
        foreach (var e in targets)
        {
            Vector3 pos = e.transform.position;
            Player.VFXManager.PlayParticle(CardInfo, pos, (int)CombineLevel, _skillDurations[(int)CombineLevel]);
        }

        if (targets.Count > 0)
        {
            StartCoroutine(AttackCor());
        }
        else
        {
            Player.VFXManager.PlayParticle(CardInfo, battleController.EnemyGroupPosList[0], (int)CombineLevel, _skillDurations[(int)CombineLevel]);
        }
            
        Player.OnAnimationCall -= HandleAnimationCall;
    }

    public void HandleEffectEnd()
    {
        Player.EndAbility();
        Player.VFXManager.EndParticle(CardInfo, (int)CombineLevel);
        IsActivingAbillity = false;
        Player.VFXManager.OnEndEffectEvent -= HandleEffectEnd;

        foreach (var m in battleController.OnFieldMonsterArr)
        {
            if (targets.Contains(m)) continue;
            m?.SpriteRendererCompo.DOColor(maxtimumColor, .5f);
        }
    }

    private IEnumerator AttackCor()
    {
        yield return new WaitForSeconds(0.1f);

        foreach (var e in targets)
        {
            e.HealthCompo.ApplyDamage(GetDamage(CombineLevel), Player);
            if (Random.value * 100 >= 30f)
            {
                e.HealthCompo.AilmentStat.ApplyAilments(AilmentEnum.Shocked);
            }
            GameObject obj = Instantiate(CardInfo.hitEffect.gameObject, e.transform.position, Quaternion.identity);
            Destroy(obj, 1.0f);
            ExtraAttack(e);
        }

        FeedbackManager.Instance.EndSpeed = 3.0f;
        FeedbackManager.Instance.ShakeScreen(2.0f);

    }
}
