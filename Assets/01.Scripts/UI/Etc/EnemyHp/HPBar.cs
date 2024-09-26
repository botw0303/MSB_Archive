using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private RectTransform _thisTrm;
    public RectTransform ThisTrm => _thisTrm;

    private bool _canStartFollowOwner;
    private Transform _ownerOfThisHpBar;
    public Transform OwnerOfThisHpBar
    {
        set
        {
            _ownerOfThisHpBar = value;
            _canStartFollowOwner = true;
        }
    }
    private Sequence _playerGetDamageSequence;

    [SerializeField] private Slider _hpBar;
    [SerializeField] private Slider _hpBarTurm;
    [SerializeField] private float _easingTime;

    [SerializeField] private Image _fillImg;
    [SerializeField] private Sprite _enemySprite;
    [SerializeField] private Sprite _freindSprite;

    public BuffingMarkSetter BuffMarkSetter { get; private set; }

    private Transform _targetTrm;

    public void Init(bool isEnemy, Transform myTrm)
    {
        BuffMarkSetter = GetComponent<BuffingMarkSetter>();

        if (!_canStartFollowOwner) return;
        transform.position = _ownerOfThisHpBar.position;

        _fillImg.sprite = isEnemy ? _enemySprite : _freindSprite;

        _targetTrm = myTrm;
    }

    private void Update()
    {
        if(_targetTrm != null)
        {
            ThisTrm.localPosition = MaestrOffice.GetScreenPosToWorldPos(_targetTrm.position);
        }
    }

    public void HandleHealthChanged(float generatedHealth)
    {
        _playerGetDamageSequence = DOTween.Sequence();
        _playerGetDamageSequence.Append
        (
            DOTween.To(() => _hpBar.value, v => _hpBar.value = v,
            generatedHealth, _easingTime)
        );
        _playerGetDamageSequence.Join
        (
            DOTween.To(() => _hpBarTurm.value, v => _hpBarTurm.value = v,
            generatedHealth, _easingTime + 0.5f)
        );
    }
}
