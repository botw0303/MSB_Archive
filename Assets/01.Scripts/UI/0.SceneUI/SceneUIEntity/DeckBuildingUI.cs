using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuildingUI : SceneUI
{
    private DeckBuilder _deckBuilder;
    public bool IsEditing { get; set; } = false;

    public override void SceneUIStart()
    {
        base.SceneUIStart();
        _deckBuilder = GetComponent<DeckBuilder>();
    }
}
