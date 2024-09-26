using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeScreenSignal : MonoBehaviour
{
    public CinemachineBrain timelineCam;
    public CinemachineImpulseSource source;
    public void ShakeScreen(Vector3 value)
    {
        CinemachineImpulseListener perlin = timelineCam.ActiveVirtualCamera.VirtualCameraGameObject
            .GetComponent<CinemachineImpulseListener>();
        FeedbackManager.Instance.ShakeScreen(value, 0.3f);
        source.GenerateImpulse(value);
    }
}