using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum EnemyActionEnum
{
	EnemyHeal,
	EnemySeedGunAttack,
	EnemyCandyLaser,
	EnemyHew,
	EnemyJumpAttack,
	EnemyRollingAttack,
	EnemySlideAttack,
	EnemyThorwMelon,
	EnemyThrowKiwi,
	EnemyUltraSound
}
public abstract class EnemyAction : ITurnAction
{
	public Sprite stateIcon;
	public CameraMoveTypeSO camInfo;
	public AudioClip actionSound;
	public event Action OnEndEvnet;
	protected Enemy _owner;
	protected bool isRunning;
	public EnemyAction(Enemy owner, Sprite actionIcon, CameraMoveTypeSO cameraInfo, AudioClip skillSound)
	{ _owner = owner; stateIcon = actionIcon; camInfo = cameraInfo; actionSound = skillSound; }
	public abstract void Init();
	public IEnumerator Execute()
	{
		yield return Activate();
		OnEndEvnet?.Invoke();
	}
	protected abstract IEnumerator Activate();

	public bool CanUse()
	{
		return !_owner.HealthCompo.IsDead && !BattleController.Instance.IsGameEnd;
	}
}
