using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableMono : MonoBehaviour, IPoolable
{
    [SerializeField]private PoolingType _poolingType;
    public PoolingType poolingType { get => _poolingType; set => _poolingType = value; }

    public abstract void Init();
}
