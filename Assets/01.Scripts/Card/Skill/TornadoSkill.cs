using Particle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TornadoSkill : CardBase
{
    public override void Abillity()
    {
        IsActivingAbillity = true;
        Player.OnAnimationCall += HandleAnimationCall;
        Player.VFXManager.OnEndEffectEvent += HandleEffectEnd;
        Player.UseAbility(this, false, true);
    }
    private void HandleAnimationCall()
    {
        //Player.VFXManager.PlayParticle(CardInfo, battleController.enemyGroupCenter.position, (int)CombineLevel);
        ParticlePoolObject tornado = null;
        Player.VFXManager.PlayParticle(this, battleController.FormationCenterPos,out tornado);
        Vector3 pos = tornado.transform.position;
        pos.x -= 5;
        tornado.transform.DOMove(pos, 5f);

        StartCoroutine(AttackCor());
        Player.OnAnimationCall -= HandleAnimationCall;
    }

    private void HandleEffectEnd()
    {
        Player.EndAbility();
        Player.VFXManager.EndParticle(CardInfo, (int)CombineLevel);
        IsActivingAbillity = false;
        Player.VFXManager.OnEndEffectEvent -= HandleEffectEnd;
    }

    private IEnumerator AttackCor()
    {
        FeedbackManager.Instance.ShakeScreen(2.0f);
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.26f);
            foreach (var e in Player.GetSkillTargetEnemyList[this])
            {
                e?.HealthCompo.ApplyDamage(GetDamage(CombineLevel), Player);

                if (e != null)
                {
                    GameObject obj = Instantiate(CardInfo.hitEffect.gameObject, e.transform.position, Quaternion.identity);
                    Destroy(obj, 1.0f);
                }

                float randNumX = UnityEngine.Random.Range(-.5f, .5f);
                float randNumY = UnityEngine.Random.Range(-.5f, .5f);
                FeedbackManager.Instance.ShakeScreen(new Vector3(randNumX, randNumY, 0.0f));
            }
        }
    }
}