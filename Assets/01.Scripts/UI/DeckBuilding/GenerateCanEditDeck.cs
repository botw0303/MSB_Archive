using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCanEditDeck : DeckGenerator
{
    [SerializeField] private EditDeckPanel _editDeckPanel;

    protected override void Start()
    {
        base.Start();
    }

    private void OnEnable()
    {
        UIManager.Instance.GetSceneUI<DeckBuildingUI>().IsEditing = false;
        ResetDeckList();
    }

    protected override void SetSelectDeck(DeckElement deckElement)
    {
        _editDeckPanel.gameObject.SetActive(true);
        _editDeckPanel.SetPanelInfo(deckElement);
    }
}
