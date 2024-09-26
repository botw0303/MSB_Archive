using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FirstSpawnData
{
    public int mapIdx;
    public Enemy enemy;
}
[CreateAssetMenu(menuName = "SO/EnemyGroup")]
public class EnemyGroupSO : ScriptableObject
{
    public List<FirstSpawnData> firstSpawns = new(); 
    public List<Enemy> enemies = new List<Enemy>();

}
