using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PreviewIngredientElement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int idx;

    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.Instance.GetSceneUI<BakeryUI>().RemoveIngredient(idx);
    }
}
