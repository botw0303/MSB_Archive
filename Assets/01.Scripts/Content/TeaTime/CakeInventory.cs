using ExtensionFunction;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CakeInventory : MonoBehaviour
{
    [SerializeField] private RectTransform _content;
    [SerializeField] private float _contentStretchValue = 260;
    [SerializeField] private CakeInventoryElement _cakeElementPrefab;
    [SerializeField] private CakeCollocation _cakeCollocation;
    [SerializeField] private CakeInventoryPanel _cakeInvenPanel;

    public void CreateCakeElement()
    {
        // _content.Clear();

        if (DataManager.Instance.IsHaveData(DataKeyList.bakeryRecipeDataKey))
        {
            List<CakeData> cakeDataList =
            DataManager.Instance.LoadData<BakeryData>(DataKeyList.bakeryRecipeDataKey).
            CakeDataList;

            List<(ItemDataSO, int)> cakeList = new();

            foreach (var cake in cakeDataList)
            {
                Debug.Log($"Name : {cake.CakeName}");
                cakeList.Add((BakingManager.Instance.GetCakeDataByName(cake.CakeName), cake.Count));
            }

            for (int i = 0; i < cakeList.Count; i++)
            {
                if (i % 5 == 0)
                {
                    _content.sizeDelta =
                    new Vector2(_content.sizeDelta.x, _content.sizeDelta.y + _contentStretchValue);
                }

                CakeInventoryElement cie = Instantiate(_cakeElementPrefab, _content);

                cie.SetInfo(cakeList[i].Item1, cakeList[i].Item2, _cakeCollocation, _cakeInvenPanel, cakeDataList[i]);
            }
        }
    }
}
