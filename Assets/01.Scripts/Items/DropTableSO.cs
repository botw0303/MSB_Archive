using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Items/DropTable")]
public class DropTableSO : ScriptableObject
{
    public List<ItemDataSO> dropList;
    [Range(0, 100f)]
    public float dropChance;

    private float[] _itemWeights;

    private void OnEnable()
    {
        //��������� ���� �̾Ƽ� �迭�� ����.
        _itemWeights = dropList.Select(item => item.dropChance).ToArray();
    }

    //��������� ������ �ش� �����ȿ� ���� ���ɼ��� �ִ� �ַ� ��´�. 
    public bool GetDropItem(out ItemDataSO data)
    {
        float value = Random.Range(0, 100f);
        data = null;
        if (value <= dropChance)
        {
            data = dropList[GetRandomWeightedIndex()];
            return true;
        }

        return false;
    }


    private int GetRandomWeightedIndex()
    {
        float sum = 0f;
        for (int i = 0; i < _itemWeights.Length; i++)
        {
            sum += _itemWeights[i];
        }

        float randomValue = Random.Range(0f, sum);
        float tempSum = 0f;

        for (int i = 0; i < _itemWeights.Length; i++)
        {
            if (randomValue >= tempSum && randomValue < tempSum + _itemWeights[i])
            {
                return i;
            }
            else
            {
                tempSum += _itemWeights[i];
            }
        }

        return 0;
    }
}
