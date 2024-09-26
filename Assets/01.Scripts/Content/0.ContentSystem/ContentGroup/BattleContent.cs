using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleContent : Content
{
    public CutScene cutScene;
    [SerializeField] private Collider2D _contentConfiner;
    public Collider2D ContentConfiner => _contentConfiner;

    public override void ContentStart()
    {
        if (StageManager.Instanace.SelectStageData.stageCutScene is not null)
            cutScene = Instantiate(StageManager.Instanace.SelectStageData.stageCutScene, transform);

        MaestrOffice.Camera.orthographic = false;
        MaestrOffice.EffectCamera.orthographic = false;

    }
}
