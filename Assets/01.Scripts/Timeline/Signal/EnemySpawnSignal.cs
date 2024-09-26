using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemySpawnSignal : MonoSignal
{
    public void InitField()
    {
        BattleController.Instance.InitField();
    }

    protected override void Init(PlayableDirector d)
    {
    }
}
