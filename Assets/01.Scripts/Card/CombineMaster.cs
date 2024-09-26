using DG.Tweening;
using UnityEngine;

public class CombineMaster : MonoBehaviour
{
    [SerializeField] private GameObject _combineFX;
    [SerializeField] private float _combinningSce;
    [SerializeField] private float _combineCompleteSec;

    [Header("ÄÞ¹ÙÀÎ ÄÃ·¯")]

    [SerializeField] private Color _level_1_Color;
    public Color Level_1_Color => _level_1_Color;

    [SerializeField] private Color _level_2_Color;
    public Color Level_2_Color => _level_2_Color;

    [SerializeField] private Color _level_3_Color;
    public Color Level_3_Color => _level_3_Color;

    public void CombineGenerate()
    {
        for(int i = 0; i < BattleReader.CountOfCardInHand(); i++)
        {
            CardBase frontC;
            CardBase currentC = BattleReader.GetCardinfoInHand(i);

            if (currentC.CheckCanCombine(out frontC) && frontC != currentC)
            {
                CombineCard(currentC, frontC);
                return;
            }
        }

        for(int i = 0; i < BattleReader.CountOfCardInHand(); i++)
        {
            CardBase selectC = BattleReader.GetCardinfoInHand(i);
            selectC.SetUpCard(BattleReader.GetHandPos(selectC), false);
        }

        BattleReader.CardDrawer.CanDraw = true;
    }

    public void CombineCard(CardBase cb_1, CardBase cb_2)
    {
        BattleReader.IsOnTargetting = true;

        float combineXPos = (cb_1.transform.localPosition.x + cb_2.transform.localPosition.x) * 0.5f;
        Vector3 fxSpawnPos = (cb_1.transform.position + cb_2.transform.position) * 0.5f;
        float targeXtPos = cb_2.transform.localPosition.x;
        Vector2 visualNormalPos = cb_1.VisualTrm.transform.localPosition;

        Sequence seq = DOTween.Sequence();
        seq.Append(cb_1.transform.DOLocalMoveX(combineXPos, _combinningSce).SetEase(Ease.InCubic));
        seq.Join(cb_2.transform.DOLocalMoveX(combineXPos, _combinningSce).SetEase(Ease.InCubic));
        seq.Join(cb_1.VisualTrm.DOShakePosition(_combinningSce, 10, 30));
        seq.Join(cb_2.VisualTrm.DOShakePosition(_combinningSce, 10, 30));
        seq.InsertCallback(0, () =>
        {
            Instantiate(_combineFX, fxSpawnPos, Quaternion.identity);
        });
        seq.AppendCallback(() =>
        {
            BattleReader.RemoveCardInHand(cb_1);
            Destroy(cb_1.gameObject);

            cb_2.CombineLevel = (cb_2.CombineLevel + 1);
            cb_2.transform.rotation = Quaternion.identity;
        });
        seq.Append(cb_2.transform.DORotate(new Vector3(0, 360, 0), 0.3f, RotateMode.FastBeyond360));
        seq.AppendCallback(() => 
        { 
            cb_2.SetUpCard(targeXtPos, true);
            BattleReader.IsOnTargetting = false;
        });
    }
}
