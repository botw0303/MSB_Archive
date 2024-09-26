using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[Flags]
public enum AilmentEnum : int
{
	None = 0,
	Chilled = 1,
	Shocked = 2,
	Faint = 4,
}

public enum StackEnum : int
{
	Forging = 0, // ����
	Lightning, // ����
	DEFMusicalNote, // ���� ����
	DMGMusicaldNote, // �޴� ������ ����
	FAINTMusicalNote, // ����
}

public class Health : MonoBehaviour, IDamageable
{
	public int maxHealth;

	[SerializeField] private int _currentHealth;

	public Action<Color, int> OnDamageText; //������ �ؽ�Ʈ�� ����� �Ҷ�.
	public Action<float, float> OnDamageEvent;

	public Action OnBeforeHit;
	public UnityEvent OnDeathEvent;
	public UnityEvent<int> OnHitEvent;
	public UnityEvent<AilmentEnum> OnAilmentChanged;

	private Entity _owner;
	[SerializeField] private bool _isDead = false;
	public bool IsDead
	{
		get => _isDead;
		set
		{
			_isDead = value;
		}
	}
	private bool _isInvincible = false; //��������
	[SerializeField] private AilmentStat _ailmentStat; //���� �� ����� ���� ����
	public AilmentStat AilmentStat => _ailmentStat;

	public bool isLastHitCritical = false; //������ ������ ũ��Ƽ�÷� �����߳�?

	public bool IsFreeze;
	private KnockBackSystem _knockBack;

	protected void Awake()
	{
		_ailmentStat = new AilmentStat(this);
		_knockBack = GetComponent<KnockBackSystem>();


	}
	private void OnEnable()
	{
		TurnCounter.RoundEndEvent += UpdateHealth;
		_ailmentStat.EndOFAilmentEvent += HandleEndOfAilment;

		_ailmentStat.Reset();

		_isDead = false;
	}
	private void OnDisable()
	{
		_ailmentStat.EndOFAilmentEvent -= HandleEndOfAilment;
		TurnCounter.RoundEndEvent -= UpdateHealth;
	}

	private void HandleEndOfAilment(AilmentEnum ailment)
	{
		Debug.Log($"{gameObject.name} : cure from {ailment.ToString()}");
		//���⼭ ������ ���ŵ��� �ϵ��� �Ͼ�� �Ѵ�.
		OnAilmentChanged?.Invoke(_ailmentStat.currentAilment);

	}

	public void AilementDamage(AilmentEnum ailment, int damage)
	{
		//������ ���� ���ڰ� �ߵ��� �ؾ��Ѵ�.
		Debug.Log($"{ailment.ToString()} dot damaged : {damage}");
		_currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
		AfterHitFeedbacks(damage);
	}

	protected void UpdateHealth()
	{
		_ailmentStat.UpdateAilment(); //���� ������Ʈ
	}

	public void SetOwner(Entity owner)
	{
		_owner = owner;
		_currentHealth = maxHealth = _owner.CharStat.GetMaxHealthValue();
	}

	public float GetNormalizedHealth()
	{
		if (maxHealth <= 0) return 0;
		return Mathf.Clamp((float)_currentHealth / maxHealth, 0, 1f);
	}

	public void ApplyHeal(int amount)
	{
		_currentHealth = Mathf.Min(_currentHealth + amount, maxHealth);
		Debug.Log($"{_owner.gameObject.name} is healed!! : {amount}");
		_owner.OnHealthBarChanged?.Invoke(GetNormalizedHealth());
	}

	public void ApplyTrueDamage(int damage)
	{
		if (_isDead || _isInvincible) return; //����ϰų� �������¸� ���̻� ������ ����.
		_currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
	}
	public void ApplyDamage(int damage, Entity dealer, KnockBackType type = KnockBackType.PushBack)
	{
		if (_isInvincible || _isDead) return; //����ϰų� �������¸� ���̻� ������ ����.


		if (dealer.CharStat.IsCritical(ref damage))
		{

			isLastHitCritical = true;
		}
		else
		{
			isLastHitCritical = false;
		}
		if (!_isDead)
			_owner.BuffStatCompo.OnHitDamageEvent?.Invoke(dealer, ref damage);

		damage = _owner.CharStat.ArmoredDamage(damage, IsFreeze);

		Stat receivedDmgIncreaseStat = _owner.CharStat.GetStatByType(StatType.receivedDmgIncreaseValue);
		if (receivedDmgIncreaseStat != null)
			damage += Mathf.RoundToInt(damage * (receivedDmgIncreaseStat.GetValue() * 0.01f));

		DamageTextManager.Instance.PopupDamageText(this, _owner.transform.position, damage, isLastHitCritical ? DamageCategory.Critical : DamageCategory.Noraml);
		//if (!_isDead)
		//	foreach (var b in dealer.OnAttack)
		//	{
		//		b?.TakeDamage(this, ref damage);
		//	}

		if (_owner.CharStat.CanEvasion())
		{
			Debug.Log($"{_owner.gameObject.name} is evasion attack!");
			return;
		}


		_currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
		if (!_isDead)
			_owner.BuffStatCompo.OnHitDamageAfterEvent?.Invoke(dealer, this, ref damage);

		OnDamageEvent?.Invoke(_currentHealth, maxHealth);


		//���⼭ ������ ����ֱ�
		//DamageTextManager.Instance.PopupReactionText(_owner.transform.position, isLastHitCritical ? DamageCategory.Critical : DamageCategory.Noraml);
		_knockBack?.KnockBack(damage, type);

		AfterHitFeedbacks(damage);

	}

	private void AfterHitFeedbacks(int damage)
	{
		OnHitEvent?.Invoke(damage);
		if (_currentHealth <= 0)
		{
			_isDead = true;
		}
	}
	public void InvokeDeadEvent() => OnDeathEvent?.Invoke();


	//�����̻� �ɱ�.
	public void SetAilment(AilmentEnum ailment, int duration)
	{
		//if (_owner.Disappear) return;
		_ailmentStat.ApplyAilments(ailment, duration);

		OnAilmentChanged?.Invoke(_ailmentStat.currentAilment);
	}

	public void AilmentByDamage(AilmentEnum ailment, int damage)
	{
		//��ũ������ �߰� �κ�.
		//������� ������ �ؽ�Ʈ �߰�
		_currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
		DamageTextManager.Instance.PopupExtraDamageText(this, _owner.transform.position, damage, DamageCategory.Debuff);
		OnDamageEvent?.Invoke(_currentHealth, maxHealth);
		AfterHitFeedbacks(damage);

		//Debug.Log($"{gameObject.name} : shocked damage added = {shockDamage}");
	}


	public void MakeInvincible(bool value)
	{
		_isInvincible = value;
	}

    public void SetHp(int value)
    {
        _currentHealth = maxHealth = value;
    }
}