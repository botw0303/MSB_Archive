using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AilmentStat
{
    private Health _health;

    private Dictionary<AilmentEnum, int> _ailmentStack;

    public AilmentEnum currentAilment; //���� �� ����� ����

    public event Action<AilmentEnum> EndOFAilmentEvent; // �����̻� ����� �߻�

    public void Reset()
    {
        foreach (AilmentEnum ailm in Enum.GetValues(typeof(AilmentEnum)))
        {
            _ailmentStack[ailm] = 0;
        }
        currentAilment = AilmentEnum.None;
    }

    public AilmentStat(Health health)
    {
        _ailmentStack = new Dictionary<AilmentEnum, int>();

        _health = health;

        foreach (AilmentEnum ailment in Enum.GetValues(typeof(AilmentEnum)))
        {
            if (ailment == AilmentEnum.None) continue;
            _ailmentStack.Add(ailment, 0);
        }
    }

    public void UpdateAilment()
    {
        AilmentDamage();
    }
    public void CuredAilment(AilmentEnum ailment)
    {
        currentAilment ^= ailment; //XOR�� ���ְ�
        EndOFAilmentEvent?.Invoke(ailment); //���� �˸�.
    }

    private void AilmentDamage()
    {
        foreach (AilmentEnum ailment in Enum.GetValues(typeof(AilmentEnum)))
        {
            if (HasAilment(ailment))
            {
                switch (ailment)
                {
                    default:
                        break;
                }
            }
        }
    }
    public void UsedToAilment(AilmentEnum ailment)
    {
        if (!HasAilment(ailment))
            return;


        switch (ailment)
        {
            case AilmentEnum.None:
                break;
            case AilmentEnum.Chilled:
                break;
            case AilmentEnum.Shocked:
                _ailmentStack[ailment] = 0;
                CuredAilment(ailment);
                _health.AilmentByDamage(ailment, Mathf.RoundToInt(_health.maxHealth * 0.07f));
                break;
        }
    }

    //Ư�� ������� �����ϴ��� üũ
    public bool HasAilment(AilmentEnum ailment)
    {
        bool temp = ((currentAilment & ailment) > 0);
        return (currentAilment & ailment) > 0;
    }

    public int GetStackAilment(AilmentEnum ailment)
    {
        return _ailmentStack[ailment];
    }

    public void ApplyAilments(AilmentEnum value, int stack = 1)
    {
        currentAilment |= value; //���� �����̻� �߰� �����̻� �������

        //�����̻� ���� ���� �ֵ��� �ð� �������ְ�. 
        SetAilment(value, stack);

        if ((value & AilmentEnum.Chilled) > 0 && _ailmentStack[value] >= 5)
        {
            _ailmentStack[value] -= 5;
            if (_ailmentStack[value] <= 0)
                CuredAilment(value);
            _health.IsFreeze = true;
        }
    }
     
    //����ȿ���� ���ӽð� ����
    public void SetAilment(AilmentEnum ailment, int stack = 1)
    {
        _ailmentStack[ailment] += stack;
    }
}