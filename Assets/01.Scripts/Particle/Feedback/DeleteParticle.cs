using Particle.Trigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteParticle : ParticleTriggerEventBase
{
    public override void Action(ref ParticleSystem.Particle p, Collider2D col)
    {
        p.remainingLifetime = -.1f;
        if(TryGetComponent<Pianissimo>(out Pianissimo pianissimo))
        {
            pianissimo.isTriggered = true;
        }
    }
}

