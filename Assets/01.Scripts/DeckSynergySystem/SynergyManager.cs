using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SynergyClass;
using System;
using System.Reflection;

public enum SynergyImpactType
{
    AtkUpContinuous,            // Increase ATK during battle
    DefUpContinuous,            // Increase DEF during battle
    MaxHpUpContinuous,          // Increase Max Hp during battle
    AtkUpBuff,                  // Increase ATK during n turn
    DefUpBuff,                  // Increase DEF during n turn
    ForgingStackGainUp,         // Increase Forging stack gain
    DEFMusicalNoteGainUp,       // Increase Music 1 stack gain
    DMGMusicalNoteGainUp,       // Increase Mucic 2 stack gain
    FAINTMusicalNoteGainUp,     // Increase Music 3 stack gain
    ReleasedDMGUp,              // 'Released' skill extra damage coefficient increase
    LightningDMGUp,             // 'Lightning Theme' skill extra damage coefficient increase
    MusicValueUp,               // 'Finale' skill debuff coefficient increase
    AtkDownDebuff,              // Decrease ATK during n turn
    DefDownDebuff,              // Decrease DEF during n turn
}

public class SynergyManager : MonoSingleton<SynergyManager>
{
    [SerializeField]
    private SynergyChecker _synergyChecker;

    [SerializeField]
    private SynergyUI _synergyUI;

    private Dictionary<SynergyImpactType, SynergyImpact> _synergyImpactDic = new Dictionary<SynergyImpactType, SynergyImpact>();

    public void SynergyInit()
    {
        _synergyChecker.SetAllSynergyDisable();
        SynergySetting();
        SynergyActiveEnable();

        ShowSynergy();
    }

    public void SynergySetting()
    {
        foreach (SynergyImpactType impact in Enum.GetValues(typeof(SynergyImpactType)))
        {
            string typeName = impact.ToString();
            Type t = Type.GetType($"SynergyClass.{typeName}Impact");

            if(t != null)
            {
                SynergyImpact synergyImpact = Activator.CreateInstance(t) as SynergyImpact;
                _synergyImpactDic.Add(impact, synergyImpact);
            }
        }

        SetSynergyImpact();
    }

    public void SetSynergyImpact()
    {
        foreach(Synergy synergy in _synergyChecker.SynergyList)
        {
            if(_synergyImpactDic.TryGetValue(synergy.ImpactType, out synergy.SynergyImpact))
            {
                synergy.SynergyImpact.SetOwnerSynergy(synergy);
                synergy.SynergyImpact = _synergyImpactDic[synergy.ImpactType];
            }
        }

        _synergyChecker.CheckSynergyEnable();
    }

    public void SynergyActiveEnable()
    {
        foreach(Synergy synergy in _synergyChecker.SynergyList)
        {
            if (synergy.Enable)
                synergy.SynergyImpact.ImpactExcution();
        }
    }

    public SynergyChecker GetSynergyChecker()
    {
        return _synergyChecker;
    }

    public void ShowSynergy()
    {
        _synergyUI.SettingSynergyList();
        _synergyUI.SynergyShow();
    }

    public void ShowAllSynergy()
    {
        ShowSynergy();
    }
}
