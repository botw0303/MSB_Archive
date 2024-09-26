using SynergyClass;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class SynergyUI : MonoBehaviour
{
    public List<Synergy> enableSynergyList = new List<Synergy>();

    [SerializeField] private GameObject _synergyObject;
    [SerializeField] private Transform _synergyTrm;

    bool _onList = false;

    [SerializeField] private bool _isDeckBuilding = false;

    public void SettingSynergyList()
    {
        if (!_isDeckBuilding)
        {
            foreach(var item in SynergyManager.Instance.GetSynergyChecker().SynergyList)
            {
                if(item.Enable)
                {
                    enableSynergyList.Add(item);
                }
            }
        }
        else
        {
            Debug.Log("Test");
            enableSynergyList = SynergyManager.Instance.GetSynergyChecker().SynergyList;
        }
    }

    public void SynergyShow()
    {
        if (enableSynergyList.Count <= 0) return;

        foreach(var item in enableSynergyList)
        {
            GameObject obj = Instantiate(_synergyObject);
            obj.transform.SetParent(_synergyTrm);
            RectTransform rt = obj.transform as RectTransform;
            rt.localPosition = Vector3.zero;
            rt.localScale = Vector3.one;
            SynergyObject so = obj.GetComponent<SynergyObject>();

            so.SetName(item.SynergyName);
            so.SetDesc(item.SynergyDesc);
        }
    }

    public void EnableSynergyList()
    {
        _onList = !_onList;
        RectTransform rt = transform as RectTransform;
        
        if(_onList)
        {
            rt.DOScaleX(1, .1f);
        }
        else
        {
            rt.DOScaleX(0, .1f);
        }
    }
}