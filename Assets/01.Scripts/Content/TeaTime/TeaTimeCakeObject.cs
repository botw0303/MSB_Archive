using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TeaTimeCakeObject : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public bool CanCollocate { get; set; } = true;
    [SerializeField] private Image _cakeImg;
    [SerializeField] private RectTransform _rectTrm;
    private Vector2 _usuallyPos;
    private CakeInventoryElement _cakeInvenUi;
    private ItemDataBreadSO _cakeSO;
    private CakeData _info;
    public CakeData CakeInfo => _info;

    private TeaTimeUI _teaTimeUI;
    private bool _isInitThisCakeInEatRange;

    private void Start()
    {
        _teaTimeUI = UIManager.Instance.GetSceneUI<TeaTimeUI>();
        _usuallyPos = transform.position;
    }

    public void SetCakeImage(CakeInventoryElement element, ItemDataBreadSO info,CakeData data)
    {
        _cakeSO = info;
        _info = data;
        _cakeInvenUi = element;
        _cakeImg.sprite = info.itemIcon;
        _cakeImg.enabled = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = MaestrOffice.GetWorldPosToScreenPos(Input.mousePosition);
        _isInitThisCakeInEatRange = _teaTimeUI.EatRange.CheckCanEat(_rectTrm);
        _teaTimeUI.TeaTimeCreamStand.ChangeFace(_isInitThisCakeInEatRange);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(_isInitThisCakeInEatRange)
        {
            _teaTimeUI.TeaTimeCreamStand.EatCake(CakeInfo, _cakeInvenUi);
            _teaTimeUI.CakeCollocation.UnCollocateCake(CakeInfo);

            // Debug.Log($"����ũ ���� : {_cakeSO}, ī�� : {_cakeSO.ToGetCardBase.CardInfo}");

            UIManager.Instance.GetSceneUI<TeaTimeUI>().SetDirector(_info.Rank);
            _teaTimeUI.TeaTimeCreamStand.Reaction();
            for (int i = 0; i < (int)_info.Rank; i++)
            {
                UIManager.Instance.GetSceneUI<TeaTimeUI>().SetCard(i,_cakeSO.ToGetCardBase[Random.Range(0,_cakeSO.ToGetCardBase.Count)].CardInfo);
            }

            _cakeImg.enabled = false;
        }
        else
        {
            _cakeImg.enabled = true;
        }
        transform.position = _usuallyPos;
    }
}
