using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFlags : MonoBehaviour
{
    [SerializeField]
    CameraClearFlags camFlags;

    private void OnEnable()
    {
        Camera.main.clearFlags = camFlags;
    }
}
