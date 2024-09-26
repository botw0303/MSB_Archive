using Particle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyAction/SeedGun")]
public class EnemySeedGunAttack : EnemyAction
{
	private ParticleInfo _seedGun;

	public override void Init()
	{

		_seedGun = _owner.transform.Find("SeedGun").GetComponent<ParticleInfo>();
		_seedGun.owner = _owner;
		_seedGun.damages =new int[] { _owner.CharStat.GetDamage() };
		_seedGun.SettingInfo(false);
	}
	public override IEnumerator Execute()
	{
		_owner.AnimatorCompo.SetBool("attack",true);

		_seedGun.AddTriggerTarget(_owner.target);

		Vector3 pos = (Vector3)_seedGun.transform.position - new Vector3(1.52f, 0);
		Vector3 dir = (Vector3)_owner.target.transform.position - pos;

		_seedGun.transform.right = -dir;

		bool isEnd = false;
		_seedGun.StartParticle(null, () => isEnd = true);

		yield return new WaitUntil(() => isEnd);
		_owner.AnimatorCompo.SetBool("attack",false);
	}
}
