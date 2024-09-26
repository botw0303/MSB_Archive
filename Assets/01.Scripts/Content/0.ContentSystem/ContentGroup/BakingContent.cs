using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakingContent : Content
{
    [SerializeField] private GameObject _stoveGroup;
    [SerializeField] private DoughHandler _doughHandler;
    [SerializeField] private GetBreadController _breadController;

    public override void ContentStart()
    {
        _doughHandler.doughToInnerEndEvent += _breadController.DoughInStove;
        SceneObserver.BeforeSceneType = SceneType.Lobby;

        DisableStoveGroup();
    }

    public void DisableStoveGroup()
    {
        _stoveGroup.SetActive(false);
    }

    public void EnableStoveGroup()
    {
        _stoveGroup.SetActive(true);
    }

    public void OnEnable()
    {
        
    }
}
