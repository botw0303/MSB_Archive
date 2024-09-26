using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Particle;

[Serializable]
public struct PlayerVFXData
{
    public CardInfo info;
    public ParticleSystem[] particle;
    public ParticlePoolObject poolObject;
}

public class PlayerVFXManager : MonoBehaviour
{
    [SerializeField] private List<PlayerVFXData> cardAndEffects = new();
    private Dictionary<CardInfo, ParticleSystem[]> _cardByEffects = new();
    private Dictionary<CardInfo, ParticlePoolObject> _cardByEffects2 = new();

    public Action OnEffectEvent;
    public Action OnEndEffectEvent;

    [SerializeField] private Player _player;
    [SerializeField] private SpriteRenderer _fadePanel;

    private void Awake()
    {
        foreach (var c in cardAndEffects)
        {
            if (c.info == null) continue;

            if (!_cardByEffects.ContainsKey(c.info))
            {
                _cardByEffects.Add(c.info, c.particle);
                _cardByEffects2.Add(c.info, c.poolObject);
            }
            else
            {
                Debug.LogError($"{c.info}");
            }
        }

    }

    internal void EndParticle(CardInfo cardInfo, int combineLevel)
    {
        if (!_cardByEffects.ContainsKey(cardInfo))
        {
            Debug.LogError("����Ʈ�� �����");
            return;
        }
        _cardByEffects[cardInfo][combineLevel].Stop();
        _cardByEffects[cardInfo][combineLevel].gameObject.SetActive(false);
    }

    public void PlayParticle(CardInfo card, Vector3 pos, int combineLevel, float duration)
    {
        if (!_cardByEffects.ContainsKey(card))
        {
            Debug.LogError("����Ʈ�� �����");
            return;
        }
        _cardByEffects[card][combineLevel].transform.position = pos;
        _cardByEffects[card][combineLevel].gameObject.SetActive(true);
        SetBackgroundFadeOut(1);
        StartCoroutine(EndEffectCo(duration));
        _cardByEffects[card][combineLevel].Play();
    }
    public void PlayParticle(CardBase card, Vector3 pos, bool invokeFunc = false)
    {
        int level = (int)card.CombineLevel;
        ParticlePoolObject obj = PoolManager.Instance.Pop(_cardByEffects2[card.CardInfo].poolingType) as ParticlePoolObject;
        obj.transform.position = pos;
        obj[level].owner = _player; ;
        obj[level].combineLevel = level;
        obj[level].damages = card.GetDamages();
        obj[level].SettingInfo();
        foreach (var t in _player.GetSkillTargetEnemyList[card])
        {
            obj[level].AddTriggerTarget(t);
        }
        if (!invokeFunc)
        {
            obj.Active(level, OnEffectEvent, null);
        }
        else
        {
            obj.Active(level, OnEffectEvent, OnEndEffectEvent);
        }
    }

    public void PlayParticle(CardBase card, Vector3 pos, out ParticlePoolObject particle)
    {
        int level = (int)card.CombineLevel;
        ParticlePoolObject obj = PoolManager.Instance.Pop(_cardByEffects2[card.CardInfo].poolingType) as ParticlePoolObject;
        obj.transform.position = pos;
        obj[level].owner = _player; ;
        obj[level].combineLevel = (int)card.CombineLevel;
        obj[level].damages = card.GetDamages();
        obj[level].SettingInfo();

        foreach (var t in _player.GetSkillTargetEnemyList[card])
        {
            obj[level].AddTriggerTarget(t);
        }
        obj.Active(level, null, OnEndEffectEvent);
        particle = obj;
    }

    public void PlayParticle(CardBase card)
    {
        int level = (int)card.CombineLevel;
        ParticlePoolObject obj = PoolManager.Instance.Pop(_cardByEffects2[card.CardInfo].poolingType) as ParticlePoolObject;
        obj.transform.right = Vector2.left;
        obj[level].owner = _player; ;
        obj[level].combineLevel = (int)card.CombineLevel;
        obj[level].damages = card.GetDamages();
        obj[level].SettingInfo();

        foreach (var t in _player.GetSkillTargetEnemyList[card])
        {
            //obj[level].SetTriggerTarget(t);
        }
        obj.Active(level, null, OnEndEffectEvent);
    }

    public void PlayParticle(CardInfo card, int combineLevel, float duration)
    {
        if (!_cardByEffects.ContainsKey(card))
        {
            Debug.LogError(card);
            return;
        }

        _cardByEffects[card][combineLevel].gameObject.SetActive(true);
        SetBackgroundFadeOut(1);
        ParticleSystem.MainModule mainModule = _cardByEffects[card][combineLevel].main;
        StartCoroutine(EndEffectCo(duration));
        _cardByEffects[card][combineLevel].Play();
    }

    public void PlayPianissimoParticle(CardBase card, Vector3 pos, bool invokeFunc = false)
    {
        int level = (int)card.CombineLevel;
        ParticlePoolObject obj = PoolManager.Instance.Pop(_cardByEffects2[card.CardInfo].poolingType) as ParticlePoolObject;
        obj[level].owner = _player;
        obj[level].combineLevel = (int)card.CombineLevel;
        obj[level].damages = card.GetDamages();
        obj[level].SettingInfo();

        obj.transform.position = pos;
        List<Entity> TEList = _player.GetSkillTargetEnemyList[card];
        Pianissimo[] particleArr = obj.gameObject.GetComponentsInChildren<Pianissimo>();
        for (int i = 0; i < 2; ++i)
        {
            obj[level].SetTriggerTarget(i,TEList[i]);
            particleArr[i].isTriggered = false;
            particleArr[i].target = TEList[i % TEList.Count].transform;
            particleArr[i].Ready();
        }
        if (!invokeFunc)
        {
            obj.Active(level, OnEffectEvent, null);
        }
        else
        {
            obj.Active(level, OnEffectEvent, OnEndEffectEvent);
        }
    }

    public void PlayWaterAndFireHarpoonParticle(CardBase card, Vector3 pos, bool invokeFunc = false)
    {
        int level = (int)card.CombineLevel;
        ParticlePoolObject obj = PoolManager.Instance.Pop(_cardByEffects2[card.CardInfo].poolingType) as ParticlePoolObject;
        obj[level].owner = _player;
        obj[level].combineLevel = (int)card.CombineLevel;
        obj[level].damages = card.GetDamages();
        obj[level].SettingInfo();

        obj.transform.position = pos;
        List<Entity> TEList = _player.GetSkillTargetEnemyList[card];
        WaterAndFireHarpoon[] particleArr = obj.gameObject.GetComponentsInChildren<WaterAndFireHarpoon>();
        for (int i = 0; i < 1; ++i)
        {
            obj[level].SetTriggerTarget(i, TEList[i]);
            particleArr[i].isTriggered = false;
            particleArr[i].targetTrm = TEList[i % TEList.Count].transform;
            particleArr[i].Ready();
        }

        if (!invokeFunc)
        {
            obj.Active(level, OnEffectEvent, null);
        }
        else
        {
            obj.Active(level, OnEffectEvent, OnEndEffectEvent);
        }
    }

    private IEnumerator EndEffectCo(float f)
    {
        yield return new WaitForSeconds(f);
        SetBackgroundFadeIn(1);
        OnEndEffectEvent?.Invoke();
    }

    public void SetBackgroundFadeOut(float time)
    {
        _fadePanel.DOFade(0.7f, time);
    }

    public void SetBackgroundFadeIn(float time)
    {
        _fadePanel.DOFade(0f, time);
    }
}
