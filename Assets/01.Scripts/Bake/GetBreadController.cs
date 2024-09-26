using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBreadController : MonoBehaviour
{
    [SerializeField] private GameObject _grapicsElementParentObj;
    private IBakingProductionObject[] _productionGraphicsObjArr;
    [SerializeField] private DoughHandler _dough;

    [SerializeField] private int _testidx;

    private void Awake()
    {
        _productionGraphicsObjArr =
        _grapicsElementParentObj.GetComponentsInChildren<IBakingProductionObject>();
    }

    public void OnProduction()
    {
        foreach(var production in _productionGraphicsObjArr)
        {
            production.OnProduction();
        }
    }

    public void ExitProduction()
    {
        foreach (var production in _productionGraphicsObjArr)
        {
            production.ExitProduction();
        }
    }

    public void DoughInStove()
    {

        int cakeCount = 0;
        CakeRank rank = BakingManager.Instance.cacheBread.Rank;

        switch(rank)
        {
            case CakeRank.Legend:
                cakeCount = 150;
                break;
            case CakeRank.Epic:
                cakeCount = 50;
                break;
            case CakeRank.Normal:
                cakeCount = 10;
                break;
            default:
                break;
        }

        UIManager.Instance.GetSceneUI<BakeryUI>().ToGetCakeCount = cakeCount;

        foreach (var production in _productionGraphicsObjArr)
        {
            production.DoughInStove(rank);
        }
    }

}
