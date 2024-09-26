using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIDefine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.Events;

public class BakeryUI : SceneUI
{
    private BakeryContentPanel _recipePanel;
    public BakeryContentPanel RecipePanel
    {
        get
        {
            if(_recipePanel == null)
            {
                _recipePanel = FindObjectOfType<BakeryContentPanel>();
            }
            return _recipePanel;
        }
    }

    [SerializeField] private GetCakePanel _getCakePanel;
    public GetCakePanel GetCakePanel => _getCakePanel;

    [SerializeField] private GameObject _previewPanelObj;
    private PreviewPanel[] _previewPanels;

    public ItemDataBreadSO ToGetCakeType { get; set; }
    public int ToGetCakeCount { get; set; }

    [SerializeField] private GameObject _bakingGroup;

    public override void SceneUIStart()
    {
        base.SceneUIStart();
        _previewPanels = _previewPanelObj.GetComponentsInChildren<PreviewPanel>();
    }
    
    public void FilteringPreviewContent(RecipeSortType type)
    {
        foreach (var panel in _previewPanels)
        {
            panel.SetUpPanel(type);
        }
    }
    public void SelectRecipe(RecipeElement element)
    {
        var bp = _previewPanels.FirstOrDefault(x => x.MySortType == RecipeSortType.Fast) as LookRecipePreviewPanel;
        bp.HandleAppearRecipe(element);
    }
    public void SelectFavoriteRecipe(RecipeElement element)
    {
        var bp = _previewPanels.FirstOrDefault(x => x.MySortType == RecipeSortType.Favorites) as LookRecipePreviewPanel;
        bp.HandleAppearRecipe(element);
    }
    public void SelectIngredient(IngredientElement element)
    {
        var bp = _previewPanels.FirstOrDefault(x => x.MySortType == RecipeSortType.Baking) as LookBakingPreviewPanel;
        bp.SetIngredientElement(element);
    }
    public void RemoveIngredient(int idx)
    {
        var bp = _previewPanels.FirstOrDefault(x => x.MySortType == RecipeSortType.Baking) as LookBakingPreviewPanel;
        bp.RemoveIngredientElement(idx);
    }

    public void ProductionStart()
    {
        _bakingGroup.SetActive(false);
        GameManager.Instance.GetContent<BakingContent>().EnableStoveGroup();
    }

    public void ProductionEnd()
    {
        _bakingGroup.SetActive(true);
        GameManager.Instance.GetContent<BakingContent>().DisableStoveGroup();
    }

    public void SetUpResultPanel()
    {
        StartCoroutine(TurmCo());
    }

    IEnumerator TurmCo()
    {
        yield return new WaitForSeconds(1);
        GetCakePanel.SetUp(ToGetCakeType, ToGetCakeCount);
    }

    public void Reload()
    {
        GameManager.Instance.ChangeScene(SceneType.bakery);
    }
}
