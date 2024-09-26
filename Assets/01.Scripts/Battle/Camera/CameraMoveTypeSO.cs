using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum CameraTargetType
{
    Enemy = -1,
    Player = 1,
}

[Serializable]
public struct ShakeDefination
{
    public bool isShaking;
    public float seconds;
}

[Serializable]
public struct CameraMoveSequence
{
    public CameraTargetType cameraTarget;
    public Ease easingType;
    public Vector2 movingValue;
    public float rotationValue;
    public float zoonInValue;
    public float duration;
    public float cameraTransitionTime;
    public float cameraShakeValue;
    public ShakeDefination shakeDefination;
    public float delayTime;

}

[CreateAssetMenu(menuName = "SO/Camera/Sequence")]
public class CameraMoveTypeSO : ScriptableObject
{
    public List<CameraMoveSequence> camMoveSequenceList = new(); 
}
