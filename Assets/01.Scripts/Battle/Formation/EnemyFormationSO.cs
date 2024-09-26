using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemy/Formation")]
public class EnemyFormationSO : ScriptableObject
{
    public List<Vector3> positionList = new List<Vector3>();
}
