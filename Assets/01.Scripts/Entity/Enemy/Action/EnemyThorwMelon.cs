using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyThorwMelon : EnemyAction
{
	private ThrowController _throwMelon;
	public EnemyThorwMelon(Enemy owner, Sprite actionIcon, CameraMoveTypeSO cameraInfo, AudioClip skillSound) : base(owner, actionIcon, cameraInfo, skillSound)
	{
	}

		protected override IEnumerator Activate()
	{
		BattleController.Instance.CameraController.StartCameraSequnce(camInfo);
		SoundManager.PlayAudio(actionSound, true);
		isRunning = true;

		_owner.AnimatorCompo.SetBool("attack", true);
		yield return new WaitUntil(() => _owner.AnimationTrigger);

		_throwMelon = PoolManager.Instance.Pop(PoolingType.ThrowMelon) as ThrowController;
		_throwMelon.transform.position = _owner.transform.position + (Vector3)Vector2.one;
		_throwMelon.Throw(_owner, _owner.target, EndMelon);

		yield return new WaitUntil(() => !isRunning);
		_owner.AnimatorCompo.SetBool("attack", false);
	}
	private void EndMelon()
	{
		PoolManager.Instance.Push(_throwMelon);
		isRunning = false;
	}

	public override void Init()
	{
	}
}
