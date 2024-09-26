using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LookBakingPreviewPanel : PreviewPanel
{
    [SerializeField]
    private Image[] _ingredientElementVisualArr = new Image[3];
    private IngredientElement[] _ingredientElementArr = new IngredientElement[3];

    [SerializeField] private GameObject _cakeImgObj;
    [SerializeField] private GameObject _questionMarkObj;

    protected override void LookUpContent()
    {
        for (int i = 0; i < _ingredientElementArr.Length; i++)
        {
            _ingredientElementVisualArr[i].enabled = false;
            _ingredientElementArr[i] = null;
        }
    }

    public void SetIngredientElement(IngredientElement ingElement)
    {
        int idx = (int)ingElement.IngredientData.ingredientType;

        if (_ingredientElementArr[idx] != null)
        {
            _ingredientElementArr[idx].IsSelected = false;
        }

        if (_ingredientElementArr[idx] != ingElement)
        {
            _ingredientElementArr[idx] = ingElement;
            var element = _ingredientElementVisualArr[idx];

            element.enabled = true;
            element.sprite = ingElement.IngredientData.itemIcon;
        }
        else
        {
            _ingredientElementArr[idx].IsSelected = false;
            _ingredientElementArr[idx] = null;

            var element = _ingredientElementVisualArr[idx];

            element.enabled = false;
        }
    }

    public void RemoveIngredientElement(int idx)
    {
        if (_ingredientElementArr[idx] == null) return;

        _ingredientElementArr[idx].IsSelected = false;
        _ingredientElementArr[idx] = null;
        var element = _ingredientElementVisualArr[idx];

        element.enabled = false;
        element.sprite = null;
    }

    public void BakeCake()
    {
        ItemDataIngredientSO[] ingDatas =
        {
                _ingredientElementArr[0].IngredientData,
                _ingredientElementArr[1].IngredientData,
                _ingredientElementArr[2].IngredientData,
            };

        if (BakingManager.Instance.CanBake(ingDatas))
        {
            CakeData cake = BakingManager.Instance.BakeBread(ingDatas);
            if (cake == null) return;
            ItemDataBreadSO cakeSO = BakingManager.Instance.GetCakeDataByName(cake.CakeName);
            Inventory.Instance.AddItem(cakeSO);

            foreach (var ingData in ingDatas)
            {
                Inventory.Instance.RemoveItem(ingData);
            }

            BakeryUI bui = UIManager.Instance.GetSceneUI<BakeryUI>();
            BakeryData bd = new BakeryData();

            if (DataManager.Instance.IsHaveData(DataKeyList.bakeryRecipeDataKey))
            {
                bd = DataManager.Instance.LoadData<BakeryData>(DataKeyList.bakeryRecipeDataKey);
            }

            CakeData cakeData = bd.CakeDataList.FirstOrDefault(x => x.CakeName == cake.CakeName && x.Rank == cake.Rank);

            if (cakeData == null)
            {
                bd.CakeDataList.Add(BakingManager.Instance.cacheBread);
            }
            else
            {
                cakeData.Count++;
            }

            DataManager.Instance.SaveData(bd, DataKeyList.bakeryRecipeDataKey);

            bui.ToGetCakeType = cakeSO;
            bui.FilteringPreviewContent(MySortType);
            bui.RecipePanel.InvokeRecipeAction(MySortType);

            bui.ProductionStart();

            Inventory.Instance.SaveCurrentData();
        }
    }
}
