using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.Playables;

[Serializable]
public class SEList<T>
{
	public List<T> list;
}

public class BattleController : MonoSingleton<BattleController>
{
	private SEList<SEList<bool>> _isStuckCheckList;

	public Dictionary<TurnType, TurnSequence> turnSeq = new()
	{
		{ TurnType.Enemy, new TurnSequence() },
		{ TurnType.Player, new TurnSequence() }
	};
	public TurnSequence CurTurnSeq
	{
		get
		{
			return turnSeq[TurnCounter.CurrentTurnType];
		}
	}
	private Coroutine curSeqCor;


	public Enemy[] OnFieldMonsterArr { get; private set; }
	private int onFieldMonsterNum
	{
		get
		{
			int num = 0;
			for (int i = 0; i < OnFieldMonsterArr.Length; i++)
			{
				if (OnFieldMonsterArr[i] != null)
					num++;
			}
			return num;
		}
	}
	public List<Enemy> DeathEnemyList { get; private set; } = new List<Enemy>();

	private HpBarMaker _hpBarMaker;


	public List<Vector3> EnemyGroupPosList { get; set; }

	private Vector3 _formationCenterPos = Vector3.zero;
	public Vector3 FormationCenterPos
	{
		get
		{
			if (_formationCenterPos != Vector3.zero) return _formationCenterPos;

			Vector3 centerPos = Vector3.zero;

			foreach (Vector3 pos in EnemyGroupPosList)
			{
				centerPos += pos;
			}

			return centerPos / EnemyGroupPosList.Count;
		}
	}
	private Queue<PoolingType> _enemyQue = new Queue<PoolingType>();

	private Player _player;
	public Player Player
	{
		get
		{
			if (_player != null) return _player;
			_player = FindObjectOfType<Player>();
			return _player;
		}
	}
	private bool _isGameEnd;
	public bool IsGameEnd
	{
		get => _isGameEnd;
		set
		{
			_isGameEnd = value;
			if (_isGameEnd)
			{
				turnSeq[TurnType.Enemy].Clear();
				turnSeq[TurnType.Player].Clear();
				StopAllCoroutines();
				StopCoroutine(curSeqCor);

				DamageTextManager.Instance.PushAllText();
				for (int i = 0; i < OnFieldMonsterArr.Length; i++)
				{
					Enemy e = OnFieldMonsterArr[i];
					if (e == null) continue;

					OnFieldMonsterArr[i] = null;
					PoolManager.Instance.Push(e);
				}

				CostCalculator.Init();
				//SelectPlayerTarget(null, null);

				//UIManager.Instance.GetSceneUI<BattleUI>().SystemActive?.Invoke(true);
				_hpBarMaker.DeleteAllHPBar();

				BattleReader.OnBinding = true;
			}
		}
	}

	public Action OnChangeTurnEnemy;

	[SerializeField] private UnityEvent<Enemy, Vector3> _maskCreateEvent;
	public UnityEvent<Enemy> maskEnableEvent;
	public UnityEvent<Enemy> maskDisableEvent;
	public CameraController CameraController { get; private set; }


	private Action _spawnCallBack;

	public Transform enemyActionViewParent;


	public void AddLastAction(ITurnAction turnAction)
	{
		CurTurnSeq.AddLast(turnAction);
	}
	public void AddLastAction(TurnType type, ITurnAction turnAction)
	{
		turnSeq[type].AddLast(turnAction);
	}
	public void AddFirstAction(ITurnAction turnAction)
	{
		CurTurnSeq.AddFirst(turnAction);
	}
	public void AddFirstAction(TurnType type, ITurnAction turnAction)
	{
		turnSeq[type].AddFirst(turnAction);
	}

	public void StartTurnSequence(TurnType type, float startDelay = 1f, float delayTime = 1.5f)
	{
		curSeqCor = StartCoroutine(turnSeq[type].StartSequence(startDelay, delayTime));
	}
	private void Start()
	{
		EnemyGroupPosList = StageManager.Instanace.SelectStageData.enemyFormation.positionList;

		_hpBarMaker = FindObjectOfType<HpBarMaker>();

		CameraController = FindObjectOfType<CameraController>();
		CameraController.BattleController = this;


		OnFieldMonsterArr = new Enemy[EnemyGroupPosList.Count];
		CardReader.SkillCardManagement.useCardEndEvnet.AddListener(CalculateDeathEntity);

		TurnCounter.RoundStartEvent += HandleOnRoundStart;
		TurnCounter.PlayerTurnStartEvent += HandleCardDraw;
		TurnCounter.EnemyTurnStartEvent += OnEnemyTurnStart;
		TurnCounter.EnemyTurnEndEvent += OnEnemyTurnEnd;


		Player.BattleController = this;
		_hpBarMaker.SetupHpBar(Player);
		Player.HealthCompo.OnDeathEvent.AddListener(() => IsGameEnd = true);

		turnSeq[TurnType.Player].OnSequenceStart += HandlePlayerStartSeq;
		turnSeq[TurnType.Player].OnSequenceEnd += TurnCounter.ChangeTurn;
		turnSeq[TurnType.Player].OnEndAction += CalculateDeathEntity;
		turnSeq[TurnType.Player].OnEndAction += DamageTextManager.Instance.PushAllText;

		turnSeq[TurnType.Enemy].OnSequenceEnd += TurnCounter.ChangeTurn;

		turnSeq[TurnType.Enemy].OnStartAction += BackGroundFadeOut;

		turnSeq[TurnType.Enemy].OnEndAction += CalculateDeathEntity;
		turnSeq[TurnType.Enemy].OnEndAction += DamageTextManager.Instance.PushAllText;
		turnSeq[TurnType.Enemy].OnEndAction += BackGroundFadeIn;
		turnSeq[TurnType.Enemy].OnEndAction += () => OnChangeTurnEnemy?.Invoke();
	}
	private void HandlePlayerStartSeq()
	{
		foreach (var e in OnFieldMonsterArr)
		{
			if (e == null) return;
			e.actionVeiw.Reduce();
		}
	}
	private void HandleOnRoundStart()
	{
		List<Enemy> enemies = new();
		foreach (var item in OnFieldMonsterArr)
		{
			if (item != null && item.CanAction())
				enemies.Add(item);
		}
		for (int i = 0; i < onFieldMonsterNum; i++)
		{
			AddLastAction(TurnType.Enemy, enemies[UnityEngine.Random.Range(0, enemies.Count)].GetState(i + 1));
		}
	}

	private void HandleCardDraw(bool obj)
	{
		BattleReader.CardDrawer.DrawCard(3, false);
	}


	private void OnDestroy()
	{
		CardReader.SkillCardManagement.useCardEndEvnet.RemoveListener(CalculateDeathEntity);

		TurnCounter.RoundStartEvent -= HandleOnRoundStart;
		TurnCounter.EnemyTurnStartEvent -= OnEnemyTurnStart;
		TurnCounter.EnemyTurnEndEvent -= OnEnemyTurnEnd;
		TurnCounter.PlayerTurnStartEvent -= HandleCardDraw;
	}
	private void OnEnemyTurnStart(bool value)
	{
		foreach (var e in OnFieldMonsterArr)
		{
			if (e is null) continue;

			maskEnableEvent?.Invoke(e);
		}
		if (!IsGameEnd) StartTurnSequence(TurnType.Enemy);
	}



	private void OnEnemyTurnEnd()
	{
		foreach (var e in OnFieldMonsterArr)
		{
			if (e is null) continue;

			maskDisableEvent?.Invoke(e);
		}
	}


	private IEnumerator EnemySquence()
	{
		foreach (var e in OnFieldMonsterArr)
		{
			float betweenTime = 1.5f;
			if (e is null) continue;
			//Player.VFXManager.SetBackgroundColor(Color.gray);

			if (!e.HealthCompo.AilmentStat.HasAilment(AilmentEnum.Faint))
			{
				betweenTime = 1.5f;
			}
			else
			{
				betweenTime = 0.3f;
			}
			if (e is null) continue;


			//e.TurnAction();





			if (_isGameEnd) break;
			yield return new WaitForSeconds(1.5f);
		}

		if (!_isGameEnd)
		{
			TurnCounter.ChangeTurn();
		}
	}
	private void CalculateDeathEntity()
	{
		foreach (var e in OnFieldMonsterArr)
		{
			if (e is null) continue;

			if (e.HealthCompo.IsDead)
				e.HealthCompo.InvokeDeadEvent();
		}
		if (Player.HealthCompo.IsDead)
			Player.HealthCompo.InvokeDeadEvent();
	}
	public void SetStage()
	{
		if (StageManager.Instanace.SelectStageData.stageCutScene != null) return;
		InitField();
	}
	public void InitField()
	{
		foreach (var e in StageManager.Instanace.SelectStageData.enemyGroup.firstSpawns)
		{
			if (!SpawnMonster(e.enemy.poolingType, e.mapIdx))
			{
				_enemyQue.Enqueue(e.enemy.poolingType);
			}
		}
		foreach (var e in StageManager.Instanace.SelectStageData.enemyGroup.enemies)
		{
			_enemyQue.Enqueue(e.poolingType);
		}
		for (int i = 0; i < EnemyGroupPosList.Count; i++)
		{
			if (_enemyQue.Count > 0)
				SpawnMonster(_enemyQue.Dequeue(), i);
		}
	}
	private bool SpawnMonster(PoolingType enemyType, int idx)
	{
		if (OnFieldMonsterArr[idx] != null)
			return false;
		Vector3 selectPos = EnemyGroupPosList[idx];
		Enemy selectEnemy = PoolManager.Instance.Pop(enemyType) as Enemy;

		selectEnemy.transform.position = selectPos;
		selectEnemy.BattleController = this;

		int posChecker = ((idx + 3) % 2) * 2;

		_spawnCallBack = null;
		_spawnCallBack += () => _maskCreateEvent?.Invoke(selectEnemy, selectPos);
		_spawnCallBack += () => _hpBarMaker.SetupHpBar(selectEnemy);

		selectEnemy.Spawn(selectPos, _spawnCallBack);

		selectEnemy.SpriteRendererCompo.sortingOrder = posChecker;

		selectEnemy.HealthCompo.OnDeathEvent.AddListener(() => DeadMonster(selectEnemy));

		OnFieldMonsterArr[idx] = selectEnemy;
		selectEnemy.target = Player;

		return true;
	}

	public void DeadMonster(Enemy enemy)
	{
		OnFieldMonsterArr[Array.IndexOf(OnFieldMonsterArr, enemy)] = null;
		DeathEnemyList.Add(enemy);
		maskDisableEvent?.Invoke(enemy);
	}

	public bool IsStuck(int to, int who)
	{
		return _isStuckCheckList.list[to].list[who];
	}

	public void ChangePosition(Transform e1, Transform e2, Action callback = null)
	{
		e1.DOMove(e2.position, 0.5f);
		e2.DOMove(e1.position, 0.5f).OnComplete(() => callback?.Invoke());
	}
	public void ChangeXPosition(Transform e1, Transform e2, Action callback = null)
	{
		e1.DOMoveX(e2.position.x, 0.5f);
		e2.DOMoveX(e1.position.x, 0.5f).OnComplete(() => callback?.Invoke());
	}

	public void SelectPlayerTarget(CardBase cardBase, Entity entity)
	{
		Player.SaveSkillToEnemy(cardBase, entity);
	}


	public void BackGroundFadeIn()
	{
		Player.VFXManager.SetBackgroundFadeIn(0.5f);
	}

	public void BackGroundFadeOut()
	{
		Player.VFXManager.SetBackgroundFadeOut(0.5f);
	}
}
