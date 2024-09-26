using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CutSceneBindingHelper
{
    public static Object GetBindingObject(CutSceneBindingEnum type)
    {
        Object obj = null;
        switch (type)
        {
            case CutSceneBindingEnum.TartSprite:
                obj = BattleController.Instance.Player.SpriteRendererCompo.gameObject;
                break;
            case CutSceneBindingEnum.TartShadow:
                obj = BattleController.Instance.Player.transform.Find("Shadow").gameObject;
                break;
            case CutSceneBindingEnum.CreamSprite:
                obj = BattleController.Instance.Player.cream.transform.Find("Visual").gameObject;
                break;
            case CutSceneBindingEnum.CreamShadow:
                obj = BattleController.Instance.Player.cream.transform.Find("Shadow").gameObject;
                break;
        }
        return obj;
    }
}
