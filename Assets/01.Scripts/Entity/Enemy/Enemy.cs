using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public struct EnemyActionData
{
	public EnemyActionEnum actionType;
	public Sprite actionIcon;
	public CameraMoveTypeSO cameraInfo;
	public AudioClip skillSound;
	public int amount;
}
public abstract class Enemy : Entity
{

	[SerializeField] private List<EnemyActionData> statesData = new();
	protected Dictionary<EnemyActionEnum, EnemyAction> states = new();
	[SerializeField] private SpawnDataSO spawnData;
	[SerializeField] private SpriteDessolve dessolve;
	[HideInInspector] public EnemyActionVeiw actionVeiw;

	protected override void Awake()
	{
		base.Awake();
		actionVeiw = transform.Find("EnemyActionView").GetComponent<EnemyActionVeiw>();
		
		foreach (var data in statesData)
		{
			Type t = Type.GetType(data.actionType.ToString());
			var cState = Activator.CreateInstance(t, this, data.actionIcon, data.cameraInfo, data.skillSound) as EnemyAction;
			cState.Init();
			states.Add(data.actionType, cState);
		}
	}
	protected override void OnEnable()
	{
		base.OnEnable();
		foreach (var i in states)
		{
			i.Value.OnEndEvnet += actionVeiw.RemoveAction;
		}
	}
	protected override void OnDisable()
	{
		base.OnDisable();
		actionVeiw.RemoveAction();
		foreach (var i in states)
		{
			i.Value.OnEndEvnet -= actionVeiw.RemoveAction;
		}
	}
	public ITurnAction GetState(int i)
	{
		int sumAmount = 0;
		foreach (var d in statesData)
		{
			sumAmount += d.amount;
		}
		int rand = UnityEngine.Random.Range(1, sumAmount + 1);
		EnemyActionEnum t = EnemyActionEnum.EnemyHeal;
		foreach (var d in statesData)
		{
			rand -= d.amount;
			if(rand <= 0)
			{
				t = d.actionType;
				break;
			}
		}
		EnemyAction action = states[t];
		actionVeiw.AddAction(action, i, t);
		return action;
	}
	protected override void HandleDie()
	{
		base.HandleDie();
		EnemyStat es = CharStat as EnemyStat;
		Inventory.Instance.GetIngredientInThisBattle.Add(es.DropItem);
		Inventory.Instance.AddItem(es.DropItem);
	}
	public void Spawn(Vector3 selectPos, Action spawnCallBack)
	{
		spawnData.SpawnSeq(transform, selectPos, spawnCallBack);
	}
	public void SelectedOnAttack(CardBase selectCard)
	{
		BattleController.SelectPlayerTarget(selectCard, this);
	}
	public bool CanAction()
	{
		if (HealthCompo.IsDead) return false;
		return true;
	}


	private void OnMouseEnter()
	{
		//actionVeiw.Expand();
	}
	private void OnMouseExit()
	{
		//actionVeiw.Reduce();
	}
}
