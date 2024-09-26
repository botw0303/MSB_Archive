using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineConfinerBinding : MonoBehaviour
{
    private CinemachineConfiner2D confiner;

    private void Awake()
    {
        confiner = GetComponent<CinemachineConfiner2D>();
        confiner.m_BoundingShape2D = GameManager.Instance.GetContent<BattleContent>().ContentConfiner;
        confiner.InvalidateCache();
    }

}
