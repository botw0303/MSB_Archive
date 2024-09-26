using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardInfoBattlePanel : PoolableMono
{
    [SerializeField] private TextMeshProUGUI _cardNameText;
    [SerializeField] private TextMeshProUGUI _cardInfoText;

    private Tween _seqTween;

    public void SetUp(string nameText, string infoText)
    {
        _cardNameText.text = nameText;
        _cardInfoText.text = infoText;
        transform.localScale = Vector3.one * 0.3f;

        Sequence seq = DOTween.Sequence();
        _seqTween = seq;

        float inX = 0;

        if(transform.position.x > 90)
        {
            inX = -74;
        }
        else if(transform.position.x < -90)
        {
            inX = 74;
        }

        seq.Append(transform.DOScale(1, 0.15f)).SetEase(Ease.OutBack);
        seq.Join(transform.DOLocalMove(new Vector3(inX, 240, 0), 0.15f).SetEase(Ease.OutBack));
    }

    public void SetDown()
    {
        _seqTween.Kill();

        Sequence seq = DOTween.Sequence();
        _seqTween = seq;

        seq.Append(transform.DOScale(0.3f, 0.15f)).SetEase(Ease.InBack);
        seq.Join(transform.DOLocalMoveY(0, 0.15f).SetEase(Ease.InBack));
        seq.AppendCallback(() => PoolManager.Instance.Push(this));
    }

    public override void Init()
    {
        _seqTween.Kill();
        transform.localScale = Vector3.one * 0.3f;
        transform.rotation = Quaternion.identity;
    }
}
