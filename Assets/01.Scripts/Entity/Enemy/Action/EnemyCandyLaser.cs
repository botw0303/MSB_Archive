using Particle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCandyLaser : EnemyAction
{
	private ParticleInfo _laser;
	public EnemyCandyLaser(Enemy owner, Sprite actionIcon, CameraMoveTypeSO cameraInfo, AudioClip skillSound) : base(owner, actionIcon, cameraInfo, skillSound)
	{
	}

	protected override IEnumerator Activate()
	{
		isRunning = true;
		BattleController.Instance.CameraController.StartCameraSequnce(camInfo);
		SoundManager.PlayAudio(actionSound, true);
		_owner.AnimatorCompo.SetBool("attack", true);
		_laser.StartParticle(null, () => isRunning = false);
		yield return new WaitForSeconds(1.5f);
		_owner.target.HealthCompo.ApplyDamage(_owner.CharStat.GetDamage(), _owner);

		yield return new WaitUntil(() => !isRunning);
		_owner.AnimatorCompo.SetBool("attack", false);
	}

	public override void Init()
	{
		_laser = _owner.transform.Find("Laser").GetComponent<ParticleInfo>();
		_laser.owner = _owner;
		_laser.damages = new int[] { _owner.CharStat.GetDamage() };
		_laser.SettingInfo(false);
	}
}
