using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public PlayerStat stat;

    [Header("Contents")]
    [SerializeField] private List<Content> _contentList = new List<Content>();
    private Dictionary<SceneType, Content> _contentDic = new Dictionary<SceneType, Content>();
    private SceneType CurrentSceneType => SceneObserver.CurrentSceneType;
    private Content _currentContent;
    public Content CurrentContent => _currentContent;

    [Header("Pooling")]
    [SerializeField] private PoolListSO _poolingList;
    [SerializeField] private Transform _poolingTrm;

    [Header("Fade")]
    [SerializeField] private FadePanel _fadePanel;
    private bool _isLoading = false;

    private void Start()
    {
        foreach(Content content in _contentList)
        {
            if(_contentDic.ContainsKey(content.SceneType))
            {
                Debug.LogError($"Error : {content.SceneType} has overlap!!");
                continue;
            }

            _contentDic.Add(content.SceneType, content);
        }

        SceneManager.sceneLoaded += ChangeSceneContentOnChangeScene;
        ChangeSceneContentOnChangeScene(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        PoolSetUp();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= ChangeSceneContentOnChangeScene;
    }

    public T GetContent<T>() where T : Content
    {
        return (T)FindFirstObjectByType(typeof(T));
    }

    private void ChangeSceneContentOnChangeScene(Scene updateScene, LoadSceneMode mode)
    {
        if (_currentContent != null)
        {
            _currentContent.ContentEnd();
            Destroy(_currentContent.gameObject);
        }

        if (_contentDic.ContainsKey(CurrentSceneType))
        {
            Content contentObj = Instantiate(_contentDic[CurrentSceneType]);
            contentObj.gameObject.name = _contentDic[CurrentSceneType].gameObject.name + "_MAESTRO_[Content]_";
            contentObj.ContentStart();

            _currentContent = contentObj;
        }
    }
    private void PoolSetUp()
    {
        PoolManager.Instance = new PoolManager(_poolingTrm);
        foreach (PoolingItem item in _poolingList.poolList)
        {
            PoolManager.Instance.CreatePool(item.prefab, item.type, item.count);
        }
    }
    public void ChangeScene(SceneType toChangingScene)
    {
        if (_isLoading) return;

        SceneObserver.BeforeSceneType = CurrentSceneType;

        StartCoroutine(Fade(toChangingScene));
    }
    
    public Scene GetCurrentSceneInfo()
    {
        return SceneManager.GetActiveScene();
    }

    private IEnumerator Fade(SceneType toChangingScene)
    {
        _isLoading = true;
        yield return _fadePanel.StartFade(MaestrOffice.GetWorldPosToScreenPos(Input.mousePosition));
        _isLoading = false;

        SceneObserver.CurrentSceneType = SceneType.loading;
        SceneObserver.CurrentSceneType = toChangingScene;
        SceneManager.LoadScene("ActiveScene");
    }
}