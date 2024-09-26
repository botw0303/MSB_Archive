using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SpriteRendererBehaviour : PlayableBehaviour
{
    public bool flipX = false;
    public bool flipY = false;
    public Color color = Color.white;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        GameObject obj = playerData as GameObject;
        SpriteRenderer sprite = obj.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = color;
            sprite.flipX = flipX;
            sprite.flipY = flipY;
        }
    }
}
