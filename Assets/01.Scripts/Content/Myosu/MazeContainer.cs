using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeContainer : MonoBehaviour
{
    [SerializeField] private MazeData[] _mazeDataArr;
    private Dictionary<int, MazeData> _getMazeDataDic = new ();

    [SerializeField] private MazeStatSO[] _mazeStatDataArr;
    public MazeStatSO[] MazeStatDataArr => _mazeStatDataArr;

    public StageDataSO[] GetMazeDataByLoad(int load)
    {
        if(_getMazeDataDic.Count != _mazeDataArr.Length)
        {
            _getMazeDataDic.Clear();

            foreach (MazeData data in _mazeDataArr)
            {
                _getMazeDataDic.Add(data.mazeLoad, data);
            }
        }

        return _getMazeDataDic[load].stageDataGroup;
    }
}
