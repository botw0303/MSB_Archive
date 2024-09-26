using Particle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUltraSound : EnemyAction
{
	private ParticleInfo _ultraSound;

	public EnemyUltraSound(Enemy owner, Sprite actionIcon, CameraMoveTypeSO cameraInfo, AudioClip skillSound) : base(owner, actionIcon, cameraInfo, skillSound)
	{
	}

	protected override IEnumerator Activate()

	{
		_owner.AnimatorCompo.SetBool("attack", true);
		BattleController.Instance.CameraController.StartCameraSequnce(camInfo);
		SoundManager.PlayAudio(actionSound, true);
		isRunning = true;
		_ultraSound.StartParticle(null,null);
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.15f);
            _owner.target.HealthCompo.ApplyDamage(_owner.CharStat.GetDamage(), _owner);
        }
        yield return new WaitForSeconds(1f);
		isRunning = false;

		_owner.AnimatorCompo.SetBool("attack", false);
	}

	public override void Init()
	{
		_ultraSound = _owner.transform.Find("UltraSound").GetComponent<ParticleInfo>();
	}

}
