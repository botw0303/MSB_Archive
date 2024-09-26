using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteDessolve : MonoBehaviour
{
	public UnityEvent OnEndDessolve;

	private SpriteRenderer sp;
	private void Awake()
	{
		sp = GetComponent<SpriteRenderer>();
	}
	private void OnEnable()
	{
		sp.material.SetFloat("_dissolve_amount", 0);
	}
	public void Dessolve(float time)
	{
		StartCoroutine(DessolveCor(time));
	}
	private IEnumerator DessolveCor(float time)
	{
		float timer = 0;
		float per = 0;
		while(per < 1)
		{
			timer += Time.deltaTime;
			per = timer / time;
			sp.material.SetFloat("_dissolve_amount", per);
			yield return null;
		}
		OnEndDessolve?.Invoke();

	}
}