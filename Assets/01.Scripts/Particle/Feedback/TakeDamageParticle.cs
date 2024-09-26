using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Particle.Trigger;
public class TakeDamageParticle : ParticleTriggerEventBase
{
    public override void Action(ref ParticleSystem.Particle p, Collider2D col)
    {
        foreach(var t in info.Targets)
        {
            Debug.Log($"Target name {t.name}");
        }

        foreach (var t in info.Targets)
        {
            Debug.Log($"{gameObject.name} apply damage {info.Damage}");
            t.HealthCompo.ApplyDamage(info.Damage, info.Owner);
        }

        //foreach (var d in info.Damage)
        //{
        //    foreach (var t in info.Targets)
        //    {
        //        Debug.Log($"Damage {d}");
        //        t.HealthCompo.ApplyDamage(d, info.Owner);
        //    }
        //}
    }
}