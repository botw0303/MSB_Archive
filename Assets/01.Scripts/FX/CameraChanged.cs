using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CameraChanged : MonoBehaviour
{
    private Camera _parentCam;
    private Camera _cam;

    private void Awake()
    {
        _parentCam = GetComponentInParent<Camera>();
        _cam = GetComponent<Camera>();
        
        _cam.orthographic = _parentCam.orthographic;
    }

    void Update()
    {
        _cam.orthographic = _parentCam.orthographic;
    }
}
