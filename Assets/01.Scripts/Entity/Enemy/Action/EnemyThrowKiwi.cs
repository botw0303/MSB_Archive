using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyThrowKiwi : EnemyAction
{
	private Transform _kiwiSpawnTrm;
	private ThrowController _throwKiwi;

	public EnemyThrowKiwi(Enemy owner, Sprite actionIcon, CameraMoveTypeSO cameraInfo, AudioClip skillSound) : base(owner, actionIcon, cameraInfo, skillSound)
	{
	}

	protected override IEnumerator Activate()

	{
		isRunning = true;
		BattleController.Instance.CameraController.StartCameraSequnce(camInfo);
		SoundManager.PlayAudio(actionSound, true);
		_owner.AnimatorCompo.SetBool("attack", true);
		yield return new WaitUntil(() => _owner.AnimationTrigger);

		ThrowController _throwMelon = PoolManager.Instance.Pop(PoolingType.ThrowKiwi) as ThrowController;
		_throwMelon.transform.position = _kiwiSpawnTrm.position;
		_throwMelon.Throw(_owner, _owner.target, CatchKiwi);

		yield return new WaitUntil(() => !isRunning);
		_owner.AnimatorCompo.SetBool("attack", false);
	}
	private void CatchKiwi()
	{
		_owner.AnimatorCompo.SetTrigger("catchKiwi");
		PoolManager.Instance.Push(_throwKiwi);
		isRunning = false;
	}
	public override void Init()
	{
		_kiwiSpawnTrm = _owner.transform.Find("KiwiSapwnPos");
	}
}
