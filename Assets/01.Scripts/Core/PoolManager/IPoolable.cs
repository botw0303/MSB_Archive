using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public interface IPoolable
{
    public PoolingType poolingType{ get; set;}
    public void Init();
}
