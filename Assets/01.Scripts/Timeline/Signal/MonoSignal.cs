using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public abstract class MonoSignal : MonoBehaviour
{
    protected CutScene cutScene;
    private void Start()
    {
        cutScene = GameManager.Instance.GetContent<BattleContent>().cutScene;
        cutScene.startCutScene += Init;
    }
    protected abstract void Init(PlayableDirector d);
    private void OnDestroy()
    {
        cutScene.startCutScene -= Init;
    }

}
