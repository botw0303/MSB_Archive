using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SpriteRendererAsset : PlayableAsset
{
    public bool flipX = false;
    public bool flipY = false;
    public Color color = Color.white;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<SpriteRendererBehaviour>.Create(graph);

        var spriteRendererBehaviour = playable.GetBehaviour();
        //lightControlBehaviour.light = light.Resolve(graph.GetResolver());
        spriteRendererBehaviour.flipX = flipX;
        spriteRendererBehaviour.flipY = flipY;
        spriteRendererBehaviour.color = color;

        return playable;
    }
}
