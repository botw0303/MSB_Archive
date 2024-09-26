using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HpBarMaker : MonoBehaviour
{
    [SerializeField] private Transform _enemyHealthBarParent;
    [SerializeField] private Transform _buffingPanerlTrm;
    [SerializeField] private HPBar _hpBarPrefab;

    private List<HPBar> enemyHPBars = new();
    private List<HPBar> friendHPBars = new();

    private void Awake()
    {
        enemyHPBars = new();
        friendHPBars = new();
    }

    public void SetupEnemysHpBar()
    {
        Enemy[] fieldInEnemys = FindObjectsOfType<Enemy>();

        foreach (Enemy e in fieldInEnemys)
        {
            SpawnHPBar(e);
        }
    }
    public void SetupHpBar(Entity e)
    {
        SpawnHPBar(e);
    }
    public void DeleteAllHPBar()
    {
        foreach (var b in enemyHPBars)
        {
            if (b != null)
                Destroy(b.gameObject);
        }
        enemyHPBars.Clear();
        foreach (var b in friendHPBars)
        {
            if (b != null)
                Destroy(b.gameObject);
        }
        friendHPBars.Clear();
    }
    public void DeleteEnemyHPBar(HPBar e)
    {
        if (e == null) return;
        enemyHPBars.Remove(e);
        Destroy(e.gameObject);
    }
    public void DeleteFriendHPBar(HPBar e)
    {
        if (e == null) return;
        friendHPBars.Remove(e);
        Destroy(e.gameObject);
    }

    public void SpawnHPBar(Entity e)
    {
        HPBar hpBar = Instantiate(_hpBarPrefab, _enemyHealthBarParent);
        e.OnHealthBarChanged.AddListener(hpBar.HandleHealthChanged);
        e.HealthCompo.OnBeforeHit += () => FeedbackManager.Instance.FreezeTime(0.8f, 0.2f);

        hpBar.OwnerOfThisHpBar = e.hpBarTrm;
        bool isEnemy = e is Enemy;

        hpBar.Init(isEnemy, e.hpBarTrm);

        if (isEnemy)
        {
            e.HealthCompo.OnDeathEvent.AddListener(() => DeleteEnemyHPBar(hpBar));
            enemyHPBars.Add(hpBar);
        }
        else
        {
            e.HealthCompo.OnDeathEvent.AddListener(() => DeleteFriendHPBar(hpBar));
            friendHPBars.Add(hpBar);
        }

        hpBar.BuffMarkSetter.BuffingPanelTrm = _buffingPanerlTrm;
        e.BuffSetter = hpBar.BuffMarkSetter;
    }
}
