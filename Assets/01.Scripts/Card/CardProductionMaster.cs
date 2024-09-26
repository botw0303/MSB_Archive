using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using System;

public enum CardProductionType
{
    Hover,
    Select
}

public class CardProductionMaster : MonoBehaviour 
{
    private List<CardProductionRecord> _recordList = new();

    private Dictionary<CardProductionType, Action<Transform>> _toPlayActionDic = new();
    private Dictionary<CardProductionType, Action<Transform>> _toQuitActionDic = new();
    [SerializeField] private float _onTweeningEasingTime;

    [Header("카드 선택")]
    [SerializeField] private float _onSelectScaleValue;
    [SerializeField] private float _shadowMovingValue;
    [SerializeField] private float _shadowAppearTime;

    [Header("카드 아이들")]
    private List<CardBase> _onHandCardList = new List<CardBase>();
    private float _onPointerInCardValue = 1;

    private void Start()
    {
        foreach(CardProductionType type in Enum.GetValues(typeof(CardProductionType)))
        {
            switch (type)
            {
                case CardProductionType.Hover:
                    break;
                case CardProductionType.Select:
                    _toPlayActionDic.Add(type, OnSelectCard);
                    _toQuitActionDic.Add(type, QuitSelectCard);
                    break;
            }
        }
    }

    #region Select
    public void OnSelectCard(Transform cardTrm)
    {
        CardProductionRecord re = _recordList.Find(x => x.IsSameType(CardProductionType.Select, cardTrm));
        re?.Kill();
        _recordList.Remove(re);

        cardTrm.localScale = Vector3.one;

        cardTrm.rotation = Quaternion.identity;

        RectTransform cardTransform = cardTrm as RectTransform;
        cardTransform.SetAsLastSibling();

        Tween onSelectTween = 
        cardTrm.DOScale(cardTrm.localScale * _onSelectScaleValue, _onTweeningEasingTime).
        SetEase(Ease.OutBounce);

        CardProductionRecord record = new CardProductionRecord(CardProductionType.Select, cardTrm);
        _recordList.Add(record);
    }
    public void QuitSelectCard(Transform cardTrm)
    {
        CardProductionRecord re = _recordList.Find(x => x.IsSameType(CardProductionType.Select, cardTrm));
        re?.Kill();
        _recordList.Remove(re);

        Tween outSelectTween = 
        cardTrm.DOScale(Vector3.one, _onTweeningEasingTime).
        SetEase(Ease.OutBounce);

        CardProductionRecord record = new CardProductionRecord(CardProductionType.Select, cardTrm);
        _recordList.Add(record);
    }
    #endregion

    public void OnCardIdling(CardBase cardBase)
    {
        // 모든 카드가 똑같은 주기로 움직임이 일어나면 안되기 때문에 
        // 임의의 랜덤 값을 설정해 움직임을 줄 카드에 입력해 준다.
        cardBase.CardIdlingAddValue = UnityEngine.Random.Range(-4, 4);

        // 움직임을 줄 카드를 리스트에 삽입하여 관리한다.
        _onHandCardList.Add(cardBase);
    }

    public void QuitCardling(CardBase cardBase)
    {
        // 움직임을 주지 않을 카드는 리스트에서 제거한다.
        _onHandCardList.Remove(cardBase);
    }

    private void Update()
    {
        // 움직임을 관리할 카드들이 들어있는 리스트를 순회
        foreach(var card in _onHandCardList)
        {
            // 카드가 선택 되었을 때, 카드를 사용할 수 없을 때는 움직임을 주지 않는다.
            if (BattleReader.OnPointerCard == card || !card.CanUseThisCard) continue;

            // 싸인과 코싸인 값을 이용하여 부드러운 움직임을 구현
            float sineX = Mathf.Sin((Time.time + card.CardIdlingAddValue));
            float cosineY = Mathf.Cos((Time.time + card.CardIdlingAddValue));

            // 구현한 움직임을 localEulerAngles에 넣어 적용해준다.
            // local에 넣는 이유는 카드 Combine이 일어날 때 카드 자체의 eulerAngle이
            // 변하기 때문에 그 자식인 VisualTrm도 영향을 받아 움직임이 이상해 진다.
            card.VisualTrm.localEulerAngles = new Vector3(sineX, cosineY, 0) * 5;
        }
    }
}
