using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDataToStart : MonoBehaviour
{
    private void Start()
    {
        SoundData data = new SoundData();
        if(DataManager.Instance.IsHaveData(DataKeyList.volumeDataKey))
        {
            data = DataManager.Instance.LoadData<SoundData>(DataKeyList.volumeDataKey);
        }

        SoundManager.Instance.SetBGMVolume(data.BgmVolume);
        SoundManager.Instance.SetMasterVolume(data.MasterVoume);
        SoundManager.Instance.SetSFXVolume(data.SfxVolume);
    }
}
