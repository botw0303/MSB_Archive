using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySlideAttack : EnemyAction
{
	private Vector3 lastMovePos;

	public EnemySlideAttack(Enemy owner, Sprite actionIcon, CameraMoveTypeSO cameraInfo, AudioClip skillSound) : base(owner, actionIcon, cameraInfo, skillSound)
	{
	}

	protected override IEnumerator Activate()
	{
		isRunning = true;
		BattleController.Instance.CameraController.StartCameraSequnce(camInfo);
		SoundManager.PlayAudio(actionSound, true);
		lastMovePos = _owner.transform.position;

		Vector3 targetBackPos = _owner.target.transform.position + Vector3.right * 3;

		Vector3 jumpPos = Vector3.zero;
		jumpPos.y = _owner.transform.position.y;
		jumpPos.x = _owner.transform.position.x + 3;
		jumpPos.z = _owner.transform.position.z;

		Camera cam = Camera.main;

		Vector3 rightPos = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, Mathf.Abs(cam.transform.position.z - _owner.transform.position.z)));
		Vector3 leftPos = cam.ScreenToWorldPoint(new Vector3(0, 0, Mathf.Abs(cam.transform.position.z - _owner.transform.position.z)));

		_owner.AnimatorCompo.SetBool("attack", true);

		Sequence seq = DOTween.Sequence();
		seq.Append(_owner.transform.DOJump(jumpPos, 1f, 1, 0.5f));
		seq.Append(_owner.transform.DOMove(targetBackPos, 0.5f)).SetEase(Ease.Linear);
		seq.AppendCallback(() => _owner.target.HealthCompo.ApplyDamage(_owner.CharStat.GetDamage(), _owner));
		seq.Append(_owner.transform.DOMoveX(rightPos.x + 5, 0.2f)).SetEase(Ease.Linear);
		seq.AppendCallback(() =>
			{
				Vector3 p = _owner.transform.position;
				p.x = leftPos.x - 5;
				_owner.transform.position = p;
			});
		seq.Append(_owner.transform.DOMoveX(lastMovePos.x, 0.2f)).SetEase(Ease.Linear);
		seq.AppendCallback(() => isRunning = false);


		_owner.AnimatorCompo.SetBool("attack", false);
		yield return new WaitUntil(() => !isRunning);
	}



	public override void Init()
	{
	}
}
