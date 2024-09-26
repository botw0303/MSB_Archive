using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class EnemyActionNode : MonoBehaviour
{
	[SerializeField] private TextMeshPro text;
	[SerializeField] private SpriteRenderer sp;
	[SerializeField] private Image iconImage;
	[SerializeField] private TextMeshProUGUI typeText;
	[SerializeField] private Vector3 scale = Vector3.one;
 
	private SpriteRenderer[] sprites;
	private void Awake()
	{
		sprites = GetComponentsInChildren<SpriteRenderer>();
	}
	public void SetData(Sprite icon,int i, EnemyActionEnum actionEnum)
	{
        //sp.sprite = icon;
        iconImage.sprite = icon;

		if(actionEnum == EnemyActionEnum.EnemyHeal)
		{
			typeText.text = "회복 스킬!";
		}
		else
		{
			typeText.text = "적의 공격!";
		}

		transform.localScale = Vector3.one;
		//text.text = i.ToString();
		BattleController.Instance.enemyActionViewParent.GetComponent<VerticalLayoutGroup>().reverseArrangement = true;
	}
	public void Expand()
	{
		foreach (var sp in sprites)
		{
			sp.DOFade(1f, 0.3f);
		}
		transform.DOScale(Vector3.one * 0.7f, 0.3f);

	}
	public void Reduce()
	{
		foreach (var sp in sprites)
		{
			sp.DOFade(0.7f, 0.3f);
		}
		transform.DOScale(Vector3.one * 0.4f, 0.3f);
	}

    private void Update()
    {
        transform.localScale = scale;
		RectTransform rt = transform as RectTransform;

		rt.anchoredPosition3D = new Vector3(rt.anchoredPosition.x, rt.anchoredPosition.y, 0);
    }
}
