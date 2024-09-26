using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "SO/EnemySpawnData")]
public class SpawnDataSO : ScriptableObject
{
	[SerializeField] private Vector3 _spawnPos;

	[SerializeField] private float _moveXTime;
	[SerializeField] private Ease _moveXEasing;
	[SerializeField] private float _moveYTime;
	[SerializeField] private Ease _moveYEasing;

	public void SpawnSeq(Transform trm, Vector3 selectPos, Action spawnCallBack)
	{
		Vector3 startPos = trm.position + _spawnPos;
		trm.position = startPos;

		Sequence seq = DOTween.Sequence();
		seq.Append(trm.DOMoveX(selectPos.x, _moveXTime).SetEase(_moveXEasing));
		seq.Append(trm.DOMoveY(selectPos.y, _moveYTime).SetEase(_moveYEasing));

		seq.OnComplete(spawnCallBack.Invoke);
	}
}
