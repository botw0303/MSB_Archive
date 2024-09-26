using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public enum CutSceneBindingEnum
{
    TartSprite,
    TartShadow,
    CreamSprite,
    CreamShadow,
}
[System.Serializable]
public struct CutSceneBindingData
{
    public string trackName;
    public CutSceneBindingEnum type;
}

public class CutScene : MonoBehaviour
{
    private PlayableDirector _director;
    private Camera _cam;

    [SerializeField] private List<CutSceneBindingData> _bindingDatas;
    private Dictionary<string, CutSceneBindingEnum> _dataDic = new();

    public event Action<PlayableDirector> startCutScene;
    public event Action<PlayableDirector> endCutScene;

    private void Awake()
    {
        _director = GetComponent<PlayableDirector>();
        _cam = transform.Find("CutSceneCam").GetComponent<Camera>();

        foreach (var d in _bindingDatas)
            _dataDic.Add(d.trackName, d.type);

        _director.played += OnStartTimeline;
        _director.stopped += OnStopTimeline;
    }
    private void Start()
    {
        TimelineAsset asset = (TimelineAsset)_director.playableAsset;
        foreach (var t in asset.GetOutputTracks())
        {
            if (_dataDic.TryGetValue(t.name, out CutSceneBindingEnum v))
            {
                _director.SetGenericBinding(t, CutSceneBindingHelper.GetBindingObject(v));
            }
        }
        _director.Play();
    }
    private void OnDestroy()
    {
        _director.played -= OnStartTimeline;
        _director.stopped -= OnStopTimeline;
    }

    private void OnStartTimeline(PlayableDirector d)
    {
        _cam.gameObject.SetActive(true);
        startCutScene?.Invoke(d);
    }
    private void OnStopTimeline(PlayableDirector d)
    {
        endCutScene?.Invoke(d);
        _cam.gameObject.SetActive(false);
    }
}