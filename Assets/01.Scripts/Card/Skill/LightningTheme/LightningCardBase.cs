using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LightningCardBase : CardBase
{
    [SerializeField] private ParticleSystem _shockedEffect;
    [SerializeField] private ParticleSystem _staticEffect;
    private ParticleSystem.MainModule _mainModule;

    protected void ExtraAttack(Entity me)
    {
        foreach (var e in battleController.OnFieldMonsterArr)
        {
            try
            {
                if (e != null && e.HealthCompo.AilmentStat.HasAilment(AilmentEnum.Shocked) && e != me)
                {
                    // Apply Damage
                    e?.HealthCompo.AilmentStat.UsedToAilment(AilmentEnum.Shocked);

                    // Static chain effect spawn
                    ParticleSystem shockedFX = Instantiate(_staticEffect, Vector3.Lerp(me.transform.position, e.transform.position, 0.5f), Quaternion.identity);

                    // Set rotate
                    Vector3 dir = (e.transform.position - me.transform.position).normalized;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    //shockedFX.transform.rotation = Quaternion.Euler(shockedFX.transform.rotation.x, angle + 45, shockedFX.transform.rotation.z);
                    shockedFX.transform.rotation = Quaternion.Euler(0f, 0f, angle);

                    // Set size
                    float distance = Vector2.Distance(e.transform.position, me.transform.position);
                    _mainModule = shockedFX.main;
                    _mainModule.startSizeX = distance;

                    Destroy(shockedFX, 2f);

                    e.BuffSetter.RemoveSpecificBuffingType(BuffingType.Lightning);
                }
            }
            catch (Exception ex)
            {
                Debug.Log(e);
            }
        }
    }

    protected void ApplyShockedAilment(Entity enemy)
    {
        enemy.HealthCompo.AilmentStat.ApplyAilments(AilmentEnum.Shocked);
        CreateLightningToken(enemy);
    }

    protected void RandomApplyShockedAilment(Entity enemy, float percentage)
    {
        if (UnityEngine.Random.value * 100 >= percentage)
        {
            enemy.HealthCompo.AilmentStat.ApplyAilments(AilmentEnum.Shocked);
            CreateLightningToken(enemy);
        }
    }

    private void CreateLightningToken(Entity entity)
    {
        CombatMarkingData lightningData =
        new CombatMarkingData(BuffingType.Lightning,
        "감전 상태입니다.\n감전 공격을 받으면 다른 모든 감전 상태의 적에게 데미지가 전이됩니다.",
        2);

        BattleReader.CombatMarkManagement.AddBuffingData(entity, CardID, lightningData);
    }
}
