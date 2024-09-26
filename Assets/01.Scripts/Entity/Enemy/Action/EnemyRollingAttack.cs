using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRollingAttack : EnemyAction
{
	public EnemyRollingAttack(Enemy owner, Sprite actionIcon, CameraMoveTypeSO cameraInfo, AudioClip skillSound) : base(owner, actionIcon, cameraInfo, skillSound)
	{
	}

	protected override IEnumerator Activate()

	{
		BattleController.Instance.CameraController.StartCameraSequnce(camInfo);
		SoundManager.PlayAudio(actionSound, true);
		isRunning = true;
		_owner.AnimatorCompo.SetBool("attack", true);
		yield return new WaitUntil(() => _owner.AnimationTrigger);
		_owner.AnimatorCompo.SetBool("roll", true);

		bool isEnd = false;
		Sequence seq = DOTween.Sequence();
		Vector3 originPos = _owner.transform.position;
		seq.Append(_owner.transform.DOJump(_owner.target.transform.position, 1, 1, 0.6f));
		seq.AppendCallback(TakeDamage);
		seq.Append(_owner.transform.DOJump(originPos, 1, 1, 0.6f));
		seq.OnComplete(() => isRunning = false);
		_owner.AnimatorCompo.SetBool("attack", false);

		yield return new WaitUntil(() => !isRunning);
	}
	private void TakeDamage()
	{
		_owner.AnimatorCompo.SetBool("roll", false);
		_owner.target.HealthCompo.ApplyDamage(_owner.CharStat.GetDamage(), _owner);
	}

	public override void Init()
	{
	}
}
