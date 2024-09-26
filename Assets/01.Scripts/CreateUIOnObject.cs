using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUIOnObject : MonoBehaviour
{
    [SerializeField] private Entity e;
    [SerializeField] private HpBarMaker _hpm;

    private void Start()
    {
        _hpm.SpawnHPBar(e);
    }
}
