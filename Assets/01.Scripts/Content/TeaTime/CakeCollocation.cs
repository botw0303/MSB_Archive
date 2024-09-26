using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeCollocation : MonoBehaviour
{
    [SerializeField] private TeaTimeCakeObject[] _cakeObjectArr = new TeaTimeCakeObject[3];
    private CakeInventoryElement inventoryElement;

    public void UnCollocateCake(CakeData cakeInfo)
    {
        for (int i = 0; i < _cakeObjectArr.Length; i++)
        {
            if (_cakeObjectArr[i].CakeInfo == cakeInfo)
            {
                _cakeObjectArr[i].CanCollocate = true;
                return;
            }
        }

        Debug.LogWarning("����ũ ����");
    }

    public void CollocateCake(CakeInventoryElement element, ItemDataBreadSO cakeInfo,CakeData data)
    {
        for (int i = 0; i < _cakeObjectArr.Length; i++)
        {
            if (_cakeObjectArr[i].CanCollocate)
            {
                _cakeObjectArr[i].SetCakeImage(element,cakeInfo,data);
                _cakeObjectArr[i].CanCollocate = false;
                return;
            }
        }

        Debug.LogWarning("�ڸ� ����");
    }
}
