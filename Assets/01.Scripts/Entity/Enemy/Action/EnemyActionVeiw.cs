using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class EnemyActionVeiw : MonoBehaviour
{
	[SerializeField] private EnemyActionNode preps;
	private List<EnemyActionNode> _nodes = new();
	private bool isExpand = false;

	private void Start()
	{
		Reduce();
	}
	private void OnEnable()
	{
		foreach (var s in _nodes)
		{
			Destroy(s.gameObject);
		}
		_nodes.Clear();
		Reduce();
	}

	public void AddAction(EnemyAction ea, int i, EnemyActionEnum actionEnum)
	{
		EnemyActionNode node = Instantiate(preps, transform);
		node.transform.SetParent(BattleController.Instance.enemyActionViewParent);
		//node.transform.localScale = new Vector3(1.271249f, 1.271249f, 1.271249f);
		RectTransform rt = node.transform as RectTransform;
		rt.anchoredPosition = Vector3.zero;
		node.SetData(ea.stateIcon, i, actionEnum);
		_nodes.Add(node);

		//if (isExpand)
		//{
			//Expand();
			//return;
		//}
		//Reduce();
	}
	public void RemoveAction()
	{
		if (_nodes.Count > 0)
		{
			Destroy(_nodes[0].gameObject);
			_nodes.RemoveAt(0);
		}
	}
	public void Expand()
	{
		isExpand = true;
		float totalAng = -90f;
		float angle = 180 / (_nodes.Count + 1);
		for (int i = _nodes.Count - 1 ; i >= 0; i--)
		{
			_nodes[i].Expand();
			totalAng += angle;
			Vector3 dir = new Vector3(Mathf.Cos(totalAng * Mathf.Deg2Rad), Mathf.Sin(totalAng * Mathf.Deg2Rad), 0);
			_nodes[i].transform.DOLocalMove(dir * 2f, 0.3f).SetEase(Ease.OutBack);
		}
	}
	public void Reduce()
	{
		isExpand = false;
		for (int i = 0; i< _nodes.Count; i++)
		{
			_nodes[i].Reduce();
			Vector3 pos = Vector3.zero;
			pos.x = -0.5f;
			float t = 1.5f / _nodes.Count;
			pos.y = 0.5f - t * i;
			_nodes[i].transform.DOLocalMove(pos, 0.3f).SetEase(Ease.OutBack);
		}
	}
	public void OnDrawGizmos()
	{
		float totalAng = 90f;
		float angle = 180 / (_nodes.Count + 1);
		Gizmos.DrawLine(transform.position, transform.position + new Vector3(Mathf.Cos(totalAng * Mathf.Deg2Rad), Mathf.Sin(totalAng * Mathf.Deg2Rad), 0));
		for (int i = 0; i < _nodes.Count + 1; i++)
		{
			totalAng += angle;
			Gizmos.DrawLine(transform.position, transform.position + new Vector3(Mathf.Cos(totalAng * Mathf.Deg2Rad), Mathf.Sin(totalAng * Mathf.Deg2Rad), 0));
		}
	}
}
