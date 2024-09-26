using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MazeInUpgradeStat
{
    Hp,
    Atk,
    Cost
}

[CreateAssetMenu(menuName = "SO/Myosu/Stat")]
public class MazeStatSO : ScriptableObject
{
    public MazeInUpgradeStat mazeInUpgradeStat;
    public Sprite icon;
    public int addValue;
}
