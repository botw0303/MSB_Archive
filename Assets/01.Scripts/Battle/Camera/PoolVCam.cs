using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolVCam : PoolableMono
{
    private CinemachineVirtualCamera _vCam;
    private CinemachineConfiner2D _confiner2D;
    public CinemachineVirtualCamera VCam { get => _vCam; }
    public CinemachineConfiner2D Confiner
    {
        get { return _confiner2D; }
        set 
        { 
            _confiner2D = value; 
        }
    }

    private void Awake()
    {
        _vCam = GetComponent<CinemachineVirtualCamera>();
        _confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    public override void Init()
    {
        _vCam.m_Follow = null;
        transform.rotation = Quaternion.identity;
    }
}
