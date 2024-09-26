using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoraBuff : SpecialBuff, IOnHitDamage
{
    private bool isUsed = false;
    private MeringueSora sora;
    public override void Init()
    {
        base.Init();
        sora = entity as MeringueSora;
    }

    public void HitDamage(Entity dealer, ref int damage)
    {
        damage = 0;
        if(!isUsed)
        {
            CardReader.SkillCardManagement.useCardEndEvnet.AddListener(OnCardEndHandler);
        }
    }
    private void OnCardEndHandler()
    {
        SetIsComplete(true);
    }
    public override void SetIsComplete(bool value)
    {
        CardReader.SkillCardManagement.useCardEndEvnet.RemoveListener(OnCardEndHandler);

        sora.haveShell = false;
        base.SetIsComplete(value);
    }
}
