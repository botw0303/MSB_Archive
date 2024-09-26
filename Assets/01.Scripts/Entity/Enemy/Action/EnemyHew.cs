using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHew : EnemyAction
{
	public EnemyHew(Enemy owner, Sprite actionIcon, CameraMoveTypeSO cameraInfo, AudioClip skillSound) : base(owner, actionIcon, cameraInfo, skillSound)
	{
	}

	protected override IEnumerator Activate()

	{
		isRunning = true;
		Vector3 lastMovePos = _owner.transform.position;
		BattleController.Instance.CameraController.StartCameraSequnce(camInfo);
		SoundManager.PlayAudio(actionSound, true);
		Vector3 pos = _owner.target.forwardTrm.position;
		_owner.AnimatorCompo.SetBool("attack", true);

		Sequence seq = DOTween.Sequence();
		seq.Append(_owner.transform.DOMove(pos + Vector3.up * 5f, 1.2f));
		seq.Append(_owner.transform.DOMove(pos + Vector3.up * 5.2f, 0.25f));
		seq.AppendCallback(() => _owner.AnimatorCompo.SetTrigger("attackTrigger"));
		seq.Append(_owner.transform.DOMove(pos, 0.05f)).SetEase(Ease.Unset);
		seq.AppendCallback(() => _owner.target.HealthCompo.ApplyDamage(_owner.CharStat.GetDamage(), _owner));
		seq.Append(_owner.transform.DOMove(lastMovePos, 0.5f));
		seq.AppendCallback(() =>
		{
			_owner.AnimatorCompo.SetBool("attack", false);
			isRunning = false;
		});

		yield return new WaitUntil(() => !isRunning);
	}

	public override void Init()
	{
	}
}
