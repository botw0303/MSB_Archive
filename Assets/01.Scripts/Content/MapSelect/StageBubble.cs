using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StageBubble : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _stageNameText;
    [SerializeField] private TextMeshProUGUI _chapterNameText;

    public void SetInfo(string stageName, string chapterName)
    {
        _stageNameText.text = stageName;
        _chapterNameText.text = chapterName;
    }

    public void EnterStage()
    {
        GameManager.Instance.ChangeScene(SceneType.battle);
    }

    public void ClosePanel()
    {
        Destroy(gameObject);
    }
}
