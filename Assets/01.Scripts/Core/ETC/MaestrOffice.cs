using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public static class MaestrOffice
{
    private static Camera _cam;
    public static Camera Camera
    {
        get
        {
            if(_cam != null)
            {
                return _cam;
            }

            _cam = Camera.main;

            return _cam;
        }
    }

    private static Camera _eCam;
    public static Camera EffectCamera
    {
        get
        {
            if (_eCam != null)
            {
                return _eCam;
            }

            _eCam = Camera.main.transform.Find("EffectFilterCamera").GetComponent<Camera>();

            return _eCam;
        }
    }

    private static Canvas _can;
    public static Canvas Canvas
    {
        get
        {
            if (_can != null)
            {
                return _can;
            }

            _can = GameObject.FindObjectOfType<Canvas>();

            return _can;
        }
    }

    public static int GetPlusOrMinus()
    {
        return (int)Mathf.Sign(UnityEngine.Random.Range(0, 2) * 2 - 1);
    }

    public static Vector2 GetWorldPosToScreenPos(Vector2 screenPos)
    {
        //return Camera.ScreenToWorldPoint(screenPos);
        RectTransformUtility.ScreenPointToWorldPointInRectangle(Canvas.transform as RectTransform, screenPos, Camera, out Vector3 worldPosition);
        return worldPosition;
    }

    public static Vector2 GetWorldPosToScreenPos(Vector3 screenPos)
    {
        RectTransformUtility.WorldToScreenPoint(Camera.main, screenPos);
        Vector3 pos = Vector3.zero;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(UIManager.Instance.CanvasTrm, screenPos, Camera.main, out pos);

        return pos;
    }

    public static Vector3 GetScreenPosToWorldPos(Vector3 worldPos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Vector2 localPos = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle
        (UIManager.Instance.CanvasTrm, screenPos, Camera.main, out localPos);

        Vector3 returnPos = new Vector3(localPos.x, localPos.y, 0);

        return returnPos;
    }

    public static int BoolToInt(bool value)
    {
        return Convert.ToInt16(value);
    }

    public static int SelectedNumberOfFlagEnums(int flagEnum)
    {
        int count = 0;
        while (flagEnum > 0)
        {
            count += flagEnum & 1;
            flagEnum >>= 1;
        }
        return count;
    }
}
