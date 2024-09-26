using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/Tsumego/Test")]
public class TestTsumegoCondition : TsumegoCondition
{
    public override bool CheckCondition()
    {
        if(Time.time > 10000)
        {
            Debug.Log("10�� ����");
            return true;
        }
        else
        {
            Debug.Log($"10�� �� ���� {Time.time}");
            return false;
        }
    }
}
