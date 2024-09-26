using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineInfoContainer : MonoBehaviour
{
    [SerializeField] private List<MineInfo> _infoContainer = new List<MineInfo>();

    public MineInfo GetInfoByFloor(int floor)
    {
        int getIdx = floor - 1;
        if(getIdx < 0 || getIdx >= _infoContainer.Count)
        {
            Debug.LogError($"{floor}���� ���µ��?");
            return null;
        }
        return _infoContainer[getIdx];
    }
}
