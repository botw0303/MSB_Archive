using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPoolObject : PoolableMono
{
    [SerializeField] private AudioSource source;
    public bool IsSFX { get; set; }

    private float _saveValue;

    public void SetVolume(float vlaue)
    {
        _saveValue = vlaue;
        source.volume = vlaue;
    }

    public void SetMasterVolume(float vlaue)
    {
        source.volume = _saveValue * vlaue;
    }

    public override void Init()
    {
        //Do nothing
    }

    public void Push()
    {
        source.Stop();
        source.loop = false;
        SoundManager.Instance.RemoveAuidoObject(this);
    }
    /// <summary>
    /// 사운드 플레이 함수
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    public void Play(AudioClip clip, float pitch = 1f, float volume = 1f, bool isLoop = false)
    {
        if (isLoop)
        {
            if (SoundManager.Instance.CurrentBgmObject != null)
            {
                SoundManager.Instance.CurrentBgmObject.Push();
                PoolManager.Instance.Push(SoundManager.Instance.CurrentBgmObject);
            }

            SoundManager.Instance.CurrentBgmObject = this;
            source.loop = true;
        }

        source.Stop();
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        //stdPitch = pitch;
        source.Play();

        if(!isLoop && source.clip)
        {
            StartCoroutine(WaitForPush(source.clip.length * 1.05f));
        }
    }

    public void StopMusic()
    {
        source.Stop();
    }

    /*public void Update()
    {
        //source.pitch = stdPitch * (1 + (Time.timeScale - 1) * 0.5f);
    }*/
    IEnumerator WaitForPush(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Push();
        PoolManager.Instance.Push(this);
    }
}