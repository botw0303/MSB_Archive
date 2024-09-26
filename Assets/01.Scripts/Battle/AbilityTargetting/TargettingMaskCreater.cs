using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Image = UnityEngine.UI.Image;

public class TargettingMaskCreater : MonoBehaviour
{
    private Dictionary<Enemy, TargetMask> _getTargetMaskDic = new Dictionary<Enemy, TargetMask>();  
    public Dictionary<Enemy, TargetMask> GetTargetMaskDic => _getTargetMaskDic;
    [SerializeField] private TargetMask _targetMaskRect;

    public void CreateMask(Enemy enemy, Vector3 enemyPos)
    {
        TargetMask tm = Instantiate(_targetMaskRect, transform);
        tm.MarkingEnemy = enemy;
        RectTransform rt = tm.transform as RectTransform;
        Image img = tm.transform.GetComponent<Image>();
        if(img)
        {
            //img.raycastTarget = false;
            img.maskable = false;

            tm.GetTargetMarkImage().raycastTarget = false;
            tm.GetTargetMarkImage().maskable = false;
        }
        
        rt.localPosition = MaestrOffice.GetScreenPosToWorldPos(enemyPos);

        _getTargetMaskDic.Add(enemy, tm);
    }

    public void MaskDown(Enemy enemy)
    {
        if(enemy == null) return;

        GetTargetMaskDic[enemy].ActiveTargetMark(false);
        GetTargetMaskDic[enemy].enabled = false;
    }

    public void MaskUp(Enemy enemy)
    {
        if (enemy == null) return;

        GetTargetMaskDic[enemy].enabled = true;
    }
}
