using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private DropTableSO _dropTable;
    [SerializeField] private int _dropDiceCount = 2; //��� �ֻ��� ���� Ƚ��
    [SerializeField] private ItemObject _dropPrefab;
    public void DropItem(Vector2 dropVelocity)
    {
        for (int i = 0; i < _dropDiceCount; ++i)
        {
            Vector2 velocity = dropVelocity;
            velocity += Random.insideUnitCircle;
            //������� �ȿ��� �ɸ���
            if (_dropTable.GetDropItem(out ItemDataSO item))
            {
                ItemObject newDrop = Instantiate(_dropPrefab, transform.position, Quaternion.identity);
                newDrop.SetUpItem(item, velocity);
            }
        }
    }
}