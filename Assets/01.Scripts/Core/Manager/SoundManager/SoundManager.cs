using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Collections.Concurrent;

public class SoundManager : MonoSingleton<SoundManager>
{
	public AudioPoolObject CurrentBgmObject { get; set; }
	public static List<AudioPoolObject> AudioObjList { get; private set; } = new List<AudioPoolObject>();

	private static float _bgmVolumeValue = 1f;
	private static float _sfxVolumeValue = 1f;
	private static float _masterVolumeValue = 1f;

	public void RemoveAuidoObject(AudioPoolObject obj)
	{
		AudioObjList.Remove(obj);
	}

	private static float GetVolume(bool isSFX)
	{
		float volume = isSFX ? _sfxVolumeValue : _bgmVolumeValue;
		return volume *= _masterVolumeValue;
	}

	public static void PlayAudio(AudioClip clip, bool isSFX, bool isLoop = false, float pitch = 1f)
	{
		if (clip == null) return;
		AudioPoolObject ao = PoolManager.Instance.Pop(PoolingType.Sound).GetComponent<AudioPoolObject>();
		ao.IsSFX = isSFX;
		ao.Play(clip, pitch, GetVolume(isSFX), isLoop);
		AudioObjList.Add(ao);
	}

	public static void PlayAudioRandPitch(AudioClip clip, bool isSFX, bool isLoop = false, float pitch = 1f, float randValue = 0.1f)
	{
		AudioPoolObject ao = PoolManager.Instance.Pop(PoolingType.Sound).GetComponent<AudioPoolObject>();
		ao.IsSFX = isSFX;
		ao.Play(clip, pitch + Random.Range(-randValue, randValue), GetVolume(isSFX), isLoop);
		AudioObjList.Add(ao);
	}

	public void SetBGMVolume(float value)
	{
		_bgmVolumeValue = value;

		CurrentBgmObject?.SetVolume(value);
	}

	public void SetSFXVolume(float value)
	{
		_sfxVolumeValue = value;
		//foreach (var ao in AudioObjList)
		//{
		//    if (ao.IsSFX)
		//    {
		//        ao.SetVolume(value);
		//    }
		//}
	}

	public void SetMasterVolume(float value)
	{
		_masterVolumeValue = value;

		foreach (var ao in AudioObjList)
		{
			ao.SetMasterVolume(value);
		}
	}

	public void StopBGM()
	{
		if (CurrentBgmObject == null) return;

		CurrentBgmObject.StopMusic();
	}
}