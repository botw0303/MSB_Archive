using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public enum KnockBackType
{
    None,
    PushBack,
    KnockBack,
    Vibration
}
public class KnockBackSystem : MonoBehaviour
{
    public float knockBackAmount;
    public bool filpX;

    [SerializeField] private bool _isPlayer;

    private Dictionary<KnockBackType, KnockBackBase> _knockbackDic = new();

    private bool _knockBack;
    private Vector3 _knockBackBeforePos;
    private KnockBackType _lastKnockbackType;
    private Entity _owner;

    private void Awake()
    {
        _owner = GetComponent<Entity>();
        foreach (KnockBackType t in Enum.GetValues(typeof(KnockBackType)))
        {
            if (t == KnockBackType.None)
            {
                _knockbackDic.Add(t, null);
                continue;
            }
            KnockBackBase knockBack = Activator.CreateInstance(Type.GetType(t.ToString()), this, _owner) as KnockBackBase;
            _knockbackDic.Add(t, knockBack);
        }
    }

    private void Start()
    {
        if (_isPlayer)
            BattleController.Instance.OnChangeTurnEnemy += ResetPos;
    }
    private void OnDestroy()
    {
        if (_isPlayer)
            BattleController.Instance.OnChangeTurnEnemy -= ResetPos;
    }
    private void OnEnable()
    {
        if(!_isPlayer)
            CardReader.SkillCardManagement?.useCardEndEvnet.AddListener(ResetPos);
        
    }
    private void OnDisable()
    {
        if(!_isPlayer)
            CardReader.SkillCardManagement?.useCardEndEvnet.RemoveListener(ResetPos);
    }

    public void KnockBack(int dmg, KnockBackType type)
    {
        if (dmg <= 0 || type == KnockBackType.None) return;
        _lastKnockbackType = type;
        if (!_knockBack)
        {
            _knockBackBeforePos = transform.position;
            _knockBack = true;
        }
        _knockbackDic[type].Knockback(dmg);
    }

    private void ResetPos()
    {
        if (!_knockBack) return;
        _knockbackDic[_lastKnockbackType].ResetPos(_knockBackBeforePos);
        _knockBack = false;
    }
}
