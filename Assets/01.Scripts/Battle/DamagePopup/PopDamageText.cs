using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Random = UnityEngine.Random;

public class PopDamageText : PoolableMono
{
    [SerializeField] private Vector3 _moveEndOffset;
    [SerializeField] private Vector2 _reactionMinOffset;
    [SerializeField] private Vector2 _reactionMaxOffset;
    [SerializeField] private GameObject _criticalFrame;
    [SerializeField] private TextMeshPro _damageText;
    public TextMeshPro DamageText => _damageText;

    public void SetDamageText(Vector3 position)
    {
        position.x += Random.Range(-.5f, .5f);
        transform.position = position;
        Vector3 pos = position + Vector3.up * 2;
        transform.DOMove(pos, 0.1f).SetEase(Ease.OutQuart);

    }
    public void UpdateText(int damage, Color color)
    {
        _damageText.color = color;
        _damageText.text = damage.ToString();
        _damageText.ForceMeshUpdate();
        float size = Mathf.Clamp(1 + damage * 0.01f, 1, 2.5f);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-10f,10f));
        Vector3 scale = Vector2.one * size;
        scale.z = 1;
        transform.DOScale(scale, 0.1f).SetEase(Ease.InOutQuad);
    }
    public void EndText()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(0.3f);
        seq.Append(transform.DOMove(transform.position + transform.right * 5, .4f).SetEase(Ease.InBack));
        seq.Join(_damageText.DOFade(0, 0.4f));
        seq.OnComplete(() => PoolManager.Instance.Push(this));
    }
    public void ShowReactionText(Vector3 position, string word, float fontSize, Color color)
    {
        _damageText.color = color;
        _damageText.fontSize = fontSize;
        _damageText.text = word;


        transform.position = position;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(transform.position + new Vector3(0, 0.2f), 0.7f));
        seq.Join(_damageText.DOFade(0, 1f));
        seq.OnComplete(() => PoolManager.Instance.Push(this));
    }

    public void ActiveCriticalDamage(bool haveCritical)
    {
        _criticalFrame.SetActive(haveCritical);

    }
    private Vector3 GetRandomnessPos()
    {
        return new Vector2(Random.Range(_reactionMinOffset.x, _reactionMaxOffset.y),
                            Random.Range(_reactionMinOffset.y, _reactionMaxOffset.y));
    }

    public override void Init()
    {
        _damageText.color = Color.white;
        _criticalFrame.SetActive(false);
    }
}
