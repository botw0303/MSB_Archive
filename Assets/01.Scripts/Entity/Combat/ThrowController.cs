using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThrowController : PoolableMono
{
    [SerializeField]protected float flyTime = 0.3f;
    public abstract void Throw(Enemy enemy, Entity target,Action callbakc = null);
}
