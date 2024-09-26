using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHeal : EnemyAction
{
	public EnemyHeal(Enemy owner, Sprite actionIcon, CameraMoveTypeSO cameraInfo, AudioClip skillSound) : base(owner, actionIcon, cameraInfo, skillSound)
	{
	}

	public override void Init()
	{

	}
	protected override IEnumerator Activate()

	{
		isRunning = true;
		BattleController.Instance.CameraController.StartCameraSequnce(camInfo, false, () => isRunning = false);
		SoundManager.PlayAudio(actionSound, true);
		_owner.HealthCompo.ApplyHeal(10);
		yield return new WaitUntil(() => !isRunning);
	}

}
