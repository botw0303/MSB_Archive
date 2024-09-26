using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using TMPro;

[Serializable]
public enum DamageCategory
{
    Noraml = 0,
    Critical = 1,
    Heal = 2,
    Debuff = 3,
}

[Serializable]
public struct ReactionWord
{
    public string[] reactionWordArr;
}

public class DamageTextManager : MonoSingleton<DamageTextManager>
{
    public bool _popupDamageText;

    [Header("Font")]
    [SerializeField] private TMP_FontAsset _damageTextFont;
    [SerializeField] private TMP_FontAsset _reactionTextFont;

    [Header("normal, critical, heal, debuff")]
    [ColorUsage(true, true)]
    [SerializeField] private Color[] _textColors;
    [SerializeField] private ReactionWord[] _reactionType;

    private Dictionary<Health, (int, PopDamageText)> entityByText = new();
    private List<PopDamageText> popupTxt = new();

    public void PopupDamageText(Health health, Vector3 position, int damage, DamageCategory category)
    {
        if (!_popupDamageText) return; //텍스트가 뜨기로 되어 있을 때만 띄운다.


        PopDamageText dmgText = null;
        (int, PopDamageText) value;
        if (!entityByText.TryGetValue(health, out value))
        {
            dmgText = PoolManager.Instance.Pop(PoolingType.DamageText) as PopDamageText;
            dmgText.transform.position = Camera.main.transform.position;
            dmgText.DamageText.font = _damageTextFont;
            dmgText.DamageText.fontSize = 6;

            value = (0, dmgText);
            entityByText.Add(health, value);
        }
        dmgText = value.Item2;
        value.Item1 += damage;

        dmgText.SetDamageText(position);
        int idx = (int)category;
        //이후 더하고 효과 추가
        dmgText.UpdateText(value.Item1, _textColors[idx]);
        dmgText.ActiveCriticalDamage(category == DamageCategory.Critical);

        entityByText[health] = value;
    }
    public void PopupExtraDamageText(Health health, Vector3 position, int damage, DamageCategory category)
    {
        PopDamageText dmgText = PoolManager.Instance.Pop(PoolingType.DamageText) as PopDamageText;
        dmgText.transform.position = Camera.main.transform.position;
        dmgText.DamageText.font = _damageTextFont;
        dmgText.DamageText.fontSize = 3;

        dmgText.SetDamageText(position);
        int idx = (int)category;
        dmgText.UpdateText(damage, _textColors[idx]);
        popupTxt.Add(dmgText);
    }

    public void PushAllText()
    {
        foreach (var t in popupTxt)
        {
            t.EndText();
        }
        popupTxt.Clear();
        foreach (var t in entityByText)
        {
            t.Value.Item2.EndText();
        }
        entityByText.Clear();
    }

    public void PopupReactionText(Vector3 position, DamageCategory category)
    {
        PopDamageText _reactionText = PoolManager.Instance.Pop(PoolingType.DamageText) as PopDamageText;
        _reactionText.DamageText.font = _reactionTextFont;
        int idx = (int)category;

        int randomIdx = UnityEngine.Random.Range(0, _reactionType[idx].reactionWordArr.Length);
        _reactionText.ShowReactionText(position, _reactionType[idx].reactionWordArr[randomIdx], 10, _textColors[idx]);
    }

    public void PopupReactionText(Vector3 position, Color color, string message)
    {
        PopDamageText _reactionText = PoolManager.Instance.Pop(PoolingType.DamageText) as PopDamageText;
        _reactionText.DamageText.font = _reactionTextFont;
        _reactionText.ShowReactionText(position, message, 5, color);
    }
}
