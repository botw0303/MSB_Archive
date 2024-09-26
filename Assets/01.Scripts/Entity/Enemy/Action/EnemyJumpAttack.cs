using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyJumpAttack : EnemyAction
{
	public EnemyJumpAttack(Enemy owner, Sprite actionIcon, CameraMoveTypeSO cameraInfo, AudioClip skillSound) : base(owner, actionIcon, cameraInfo, skillSound)
	{
	}

	protected override IEnumerator Activate()

	{
		isRunning = true;
		BattleController.Instance.CameraController.StartCameraSequnce(camInfo);
		SoundManager.PlayAudio(actionSound, true);
		Vector3 startPos = _owner.transform.position;
		Sequence seq = DOTween.Sequence();
		seq.Append(_owner.transform.DOJump(_owner.target.forwardTrm.position, 1f, 1, 0.3f));
		seq.AppendCallback(Attack);
		seq.Append(_owner.transform.DOMove(startPos, 0.3f));
		seq.AppendCallback(() => isRunning = false);
		yield return new WaitUntil(() => !isRunning);
	}
	private void Attack()
	{
		_owner.AnimatorCompo.SetTrigger("attackTrigger");
		_owner.target.HealthCompo.ApplyDamage(_owner.CharStat.GetDamage(), _owner);
	}

	public override void Init()
	{
	}
}
