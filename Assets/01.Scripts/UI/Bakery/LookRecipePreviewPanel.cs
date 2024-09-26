using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LookRecipePreviewPanel : PreviewPanel
{
    private RecipeElement _recipeElement;
    [SerializeField] private GameObject _plzSelectText;
    [SerializeField] private GameObject _recipeElementObj;

    [Header("����")]
    [SerializeField] private Vector2 _crookedAngleRange;
    [SerializeField] private Image _cakeImage;
    [SerializeField] private TextMeshProUGUI _cakeNameText;
    [SerializeField] private Transform _decisionBtn;

    protected override void LookUpContent()
    {
        _plzSelectText.SetActive(true);
        _recipeElementObj.SetActive(false);
    }

    public void HandleAppearRecipe(RecipeElement element)
    {
        _recipeElement = element;

        _plzSelectText.SetActive(false);
        _recipeElementObj.SetActive(true);

        ItemDataBreadSO cakeInfo = element.CakeItemData;
        ResetRecipeElement();

        _cakeImage.sprite = cakeInfo.itemIcon;
        _cakeNameText.text = cakeInfo.itemName;

        _cakeImage.transform.parent.DOScale(1, 0.2f).SetEase(Ease.InQuart);
        _cakeImage.transform.parent.DOLocalRotateQuaternion(Quaternion.identity, 0.2f);

        _cakeNameText.transform.DOScale(1, 0.2f).SetEase(Ease.OutBack);

        _decisionBtn.transform.DOScale(1, 0.2f).SetEase(Ease.OutBounce);
    }

    private void ResetRecipeElement()
    {
        _cakeImage.transform.parent.localScale = Vector3.one * 1.2f;
        _cakeImage.transform.parent.rotation =
        Quaternion.Euler(0, 0, Random.Range(_crookedAngleRange.x, _crookedAngleRange.y));
        _decisionBtn.transform.localScale = Vector3.zero;
    }

    public void BakeCake()
    {
        ItemDataIngredientSO[] data =
        BakingManager.Instance.GetIngredientDatasByCakeName(_recipeElement.CakeItemData.itemName);

        if (!BakingManager.Instance.CanBake(data))
        {
            return;
        }

        CakeData cake = BakingManager.Instance.BakeBread(data);
        ItemDataBreadSO cakeSO = BakingManager.Instance.GetCakeDataByName(cake.CakeName);

        foreach (var item in data)
        {
            Inventory.Instance.RemoveItem(item);
        }

        Inventory.Instance.AddItem(cakeSO);

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
