using Particle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/EnemyAction/SeedGun")]
public class EnemySeedGunAttack : EnemyAction
{
	private ParticleInfo _seedGun;


	public EnemySeedGunAttack(Enemy owner, Sprite actionIcon, CameraMoveTypeSO cameraInfo, AudioClip skillSound) : base(owner, actionIcon, cameraInfo, skillSound)
	{
	}

	public override void Init()
	{

		_seedGun = _owner.transform.Find("SeedGun").GetComponent<ParticleInfo>();
		_seedGun.owner = _owner;
		_seedGun.damages =new int[] { _owner.CharStat.GetDamage() };
		_seedGun.SettingInfo(false);
	}
	protected override IEnumerator Activate()

	{
		isRunning = true;
		BattleController.Instance.CameraController.StartCameraSequnce(camInfo);
		SoundManager.PlayAudio(actionSound, true);
		_owner.AnimatorCompo.SetBool("attack",true);

		_seedGun.AddTriggerTarget(_owner.target);

		Vector3 pos = (Vector3)_seedGun.transform.position - new Vector3(1.52f, 0);
		Vector3 dir = (Vector3)_owner.target.transform.position - pos;

		_seedGun.transform.right = -dir;

		_seedGun.StartParticle(null, () => isRunning = false);

		yield return new WaitUntil(() => !isRunning);
		_owner.AnimatorCompo.SetBool("attack",false);
	}
}
