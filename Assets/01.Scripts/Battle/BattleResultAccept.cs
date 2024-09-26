using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleResultAccept : MonoBehaviour
{
    [SerializeField] private Button _myButton;
    [SerializeField] private GameObject _battleResultPanel;

    private void OnEnable()
    {
        _myButton.onClick.AddListener(StageAccept);
    }

    private void OnDisable()
    {
        _myButton.onClick.RemoveAllListeners();
    }

    public void StageAccept()
    {
        GameManager.Instance.ChangeScene(SceneObserver.BeforeSceneType);
    }

}
