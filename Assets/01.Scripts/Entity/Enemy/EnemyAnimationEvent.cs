using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
	private Enemy enemy;
	private void Start()
	{
		enemy = transform.parent.GetComponent<Enemy>();
	}

	public void AnimationTrigger()
	{
		enemy.AnimationTrigger = true;
	}
}
