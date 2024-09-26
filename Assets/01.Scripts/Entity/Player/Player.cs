using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Player : Entity
{
	private readonly int _abilityHash = Animator.StringToHash("Ability");

	public PlayerStat PlayerStat { get; private set; }
	public PlayerVFXManager VFXManager { get; private set; }
	private PlayerHPUI _hpUI;

	public Cream cream;
	private bool _isFront;

	private Dictionary<CardBase, List<Entity>> _saveSkillDic = new();
	public Dictionary<CardBase, List<Entity>> GetSkillTargetEnemyList => _saveSkillDic;

	[SerializeField] private CameraMoveTypeSO deadCamSO;



	[SerializeField] private ParticleSystem hitEffect;
	[SerializeField] private AudioClip hitSfx;
	public List<SkillGauge> skillGaugeList = new List<SkillGauge>();
	public Action<SkillGaugeSO, int> OnIncreaseSkillGauge;
	public Action<SkillGaugeSO, int> OnDecreaseSkillGauge;

	public Action OnAnimationCall;
	public Action OnAnimationEnd;

	private Vector3 originPos;


	protected override void Awake()
	{
		base.Awake();

		PlayerStat = CharStat as PlayerStat;
		VFXManager = FindObjectOfType<PlayerVFXManager>();
		SkillGaugeController.Instance.Initialize(this);
	}

	public void SaveSkillToEnemy(CardBase skillCard, Entity target)
	{
		if (!_saveSkillDic.ContainsKey(skillCard))
		{
			_saveSkillDic.Add(skillCard, new List<Entity>());
		}
		_saveSkillDic[skillCard].Add(target);
	}

	private void TurnStart(bool b)
	{
		ColliderCompo.enabled = false;
	}
	private void TurnEnd()
	{
		ColliderCompo.enabled = true;
		_saveSkillDic.Clear();
		ChangePosWithCream(false);
		ChainningCardList.Clear();
	}

	protected override void Start()
	{
		base.Start();

		cream.OnAnimationCall = () => OnAnimationCall?.Invoke();
		cream.OnAnimationEnd = () => OnAnimationEnd?.Invoke();

		TurnCounter.PlayerTurnStartEvent += TurnStart;
		TurnCounter.PlayerTurnEndEvent += TurnEnd;
	}

	protected override void OnDisable()
	{
		TurnCounter.PlayerTurnStartEvent -= TurnStart;
		TurnCounter.PlayerTurnEndEvent -= TurnEnd;
		if (_hpUI != null)
			HealthCompo.OnDamageEvent -= _hpUI.SetHpOnUI;

		SkillGaugeController.Instance.EventsFinalize();
	}

	public void AnimationEndTrigger()
	{
	}
	protected override void HandleHit(int dmg)
	{
		base.HandleHit(dmg);
		hitEffect.Play();
		SoundManager.PlayAudio(hitSfx, true);
		float currentHealth = HealthCompo.GetNormalizedHealth();
		if (!HealthCompo.IsDead && currentHealth <= 0)
		{
			FeedbackManager.Instance.FreezeTime(0.3f, 1.5f);
		}
	}
	protected override void HandleDie()
	{
		StartCoroutine(DeathDelay());
		BattleController.CameraController.StartCameraSequnce(deadCamSO,
			true,
			() => UIManager.Instance.GetSceneUI<BattleUI>().SetResult(false));
	}
	private IEnumerator DeathDelay()
	{
		yield return new WaitForSeconds(0.5f);
		AnimatorCompo.SetBool(deathAnimHash, true);
	}

	public void UseAbility(CardBase card, bool isMove = false, bool isCream = false, bool isAllAttack = false, float duration = 0.6f)
	{
		if (!isCream)
		{
			AnimatorCompo.SetBool(_abilityHash, true);

			if (isMove)
			{
				originPos = transform.position;
				if (isAllAttack)
					MoveToEnemiesCenter(duration);
				else
					MoveToTargetForward(GetSkillTargetEnemyList[card][0].forwardTrm.position);
				if (_isFront) originPos = cream.transform.position;
			}
			ChangePosWithCream(false);
		}
		else
		{
			//ũ�� �ִϸ��̼� ����
			ChangePosWithCream(true, cream.InvokeAnimationCall);
		}
	}
	public void MoveToOriginPos()
	{
		transform.DOJump(originPos, 2f, 1, 0.1f);
	}
	private void MoveToTargetForward(Vector3 position)
	{
		transform.DOJump(position, 2f, 1, 0.1f);
	}

	private void MoveToEnemiesCenter(float duration)
	{
		transform.DOJump(BattleController.FormationCenterPos, 2f, 1, duration);
	}

	private void ChangePosWithCream(bool front, Action callback = null)
	{
		if (_isFront == front)
		{
			callback?.Invoke();
			return;
		}

		_isFront = front;
		transform.DOMoveX(cream.transform.position.x, 0.5f).OnComplete(() => callback?.Invoke());
		cream.transform.DOMoveX(transform.position.x, 0.5f);

	}

	public void EndAbility()
	{
		AnimatorCompo.SetBool(_abilityHash, false);
	}

	public override void Init()
	{
	}
}
