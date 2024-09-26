using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : PoolableMono
{
	#region components
	public Animator AnimatorCompo { get; private set; }
	public Health HealthCompo { get; private set; }
	public SpriteRenderer SpriteRendererCompo { get; private set; }
	public BattleController BattleController { get; set; }
	public BuffStat BuffStatCompo { get; private set; }
	[field: SerializeField] public CharacterStat CharStat { get; private set; }

	public Collider2D ColliderCompo { get; private set; }

	public bool AnimationTrigger
	{
		get
		{
			if (_animationTrigger)
			{
				_animationTrigger = false;
				return true;
			}
			return false;
		}
		set => _animationTrigger = value;
	}
	#endregion

	private bool _animationTrigger = false;

	#region animationHash
	protected int hitAnimHash = Animator.StringToHash("hit");
	protected int deathAnimHash = Animator.StringToHash("death");
	#endregion

	public UnityEvent<float> OnHealthBarChanged;

	public Entity target;

	[Header("셋팅값들")]
	public Transform hpBarTrm;
	public Transform forwardTrm;

	public List<CardBase> ChainningCardList { get; set; } = new List<CardBase>();

	private Tween _materialChangeTween;

	public BuffingMarkSetter BuffSetter { get; set; }

	public void SelectChainningCharacter(Color skillColor, float Thickness)
	{
		_materialChangeTween.Kill();

		Material mat = new Material(SpriteRendererCompo.material);
		SpriteRendererCompo.material = mat;

		mat.SetFloat("_outline_thickness", 0);
		mat.SetColor("_outline_color", skillColor);

		_materialChangeTween = mat.DOFloat(Thickness, "_outline_thickness", 0.2f);
	}

	protected virtual void Awake()
	{
		//변수 초기화
		Transform visualTrm = transform.Find("Visual");
		AnimatorCompo = visualTrm.GetComponent<Animator>();
		HealthCompo = GetComponent<Health>();
		SpriteRendererCompo = visualTrm.GetComponent<SpriteRenderer>();
		HealthCompo.SetOwner(this);


		CharStat = Instantiate(CharStat); //������ ����
		CharStat.SetOwner(this);

		ColliderCompo = GetComponent<Collider2D>();

		BuffStatCompo = new BuffStat(this);
	}

	protected virtual void Start()
	{
	}
	protected virtual void OnEnable()
	{
		HealthCompo.OnDeathEvent.RemoveAllListeners();

		HealthCompo.SetOwner(this);

		TurnCounter.RoundStartEvent += BuffStatCompo.UpdateBuff;

		HealthCompo.OnAilmentChanged.AddListener(HandleAilmentChanged);
		OnHealthBarChanged?.Invoke(HealthCompo.GetNormalizedHealth()); //�ִ�ġ�� UI����.

		HealthCompo.OnDeathEvent.AddListener(HandleDie);
		HealthCompo.OnDeathEvent.AddListener(BuffStatCompo.ClearStat);
		HealthCompo.OnHitEvent.AddListener(HandleHit);

		ColliderCompo.enabled = true;
	}
	protected virtual void OnDisable()
	{
		BuffStatCompo.ClearStat();

		HealthCompo.OnDeathEvent.RemoveListener(HandleDie);
		HealthCompo.OnDeathEvent.RemoveListener(BuffStatCompo.ClearStat);

		HealthCompo.OnAilmentChanged.RemoveListener(HandleAilmentChanged);

		HealthCompo.OnHitEvent.RemoveListener(HandleHit);
	}

	private void HandleAilmentChanged(AilmentEnum ailment)
	{
	}

	protected virtual void HandleHit(int dmg)
	{
		//UI����
		FeedbackManager.Instance.Blink(SpriteRendererCompo.material, 0.1f);

		float currentHealth = HealthCompo.GetNormalizedHealth();
		if (currentHealth > 0)
		{
			AnimatorCompo.SetTrigger(hitAnimHash);
		}

		OnHealthBarChanged?.Invoke(currentHealth);
	}

	protected virtual void HandleDie()
	{
		AnimatorCompo.SetBool(deathAnimHash, true);
	}

	public void GoToPool()
	{
		PoolManager.Instance.Push(this);
	}

}
