using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicCardStackType
{
    None,
    DEFMusicalNote,
    DMGMusicalNote,
    FAINTMusicalNote,
    All
}

public abstract class MusicCardBase : CardBase, IEndowStackSkill
{
    public int additionStack;
    public MusicCardStackType stackType;

    protected int GetNoteCount()
    {
        return Player.BuffStatCompo.GetStack(StackEnum.DEFMusicalNote) + Player.BuffStatCompo.GetStack(StackEnum.DMGMusicaldNote) + Player.BuffStatCompo.GetStack(StackEnum.FAINTMusicalNote);
    }

    protected bool HasDEFMusicalNoteStack() { return Player.BuffStatCompo.GetStack(StackEnum.DEFMusicalNote) > 0; }

    protected bool HasDMGMusicalNoteStack() { return Player.BuffStatCompo.GetStack(StackEnum.DMGMusicaldNote) > 0; }

    protected bool HasFAINTMusicalNoteStack() { return Player.BuffStatCompo.GetStack(StackEnum.FAINTMusicalNote) > 0; }

    protected void ApplyDebuffToAllEnemy()
    {
        foreach (var e in battleController.OnFieldMonsterArr)
        {
            if(e != null)
            {
                e?.BuffStatCompo.AddBuff(buffSO, (int)CombineLevel + 2, (int)CombineLevel);

                CombatMarkingData f_data = new CombatMarkingData(BuffingType.MusicFaint,
                                     buffSO.buffInfo, (int)CombineLevel + 1);
                BattleReader.CombatMarkManagement.AddBuffingData(Player, CardID, f_data, buffSO.stackBuffs[0].values[(int)CombineLevel]);
            }
        }
    }

    protected void AddDEFMusicalNoteStack()
    {
        Debug.Log($"{buffSO.stackBuffs[0].values[(int)CombineLevel]} + {additionStack} = {buffSO.stackBuffs[0].values[(int)CombineLevel] + additionStack}");
        Player.BuffStatCompo.AddStack(StackEnum.DEFMusicalNote, buffSO.stackBuffs[0].values[(int)CombineLevel] + additionStack);
    }

    protected void AddDMGMusicalNoteStack()
    {
        Debug.Log($"{buffSO.stackBuffs[0].values[(int)CombineLevel]} + {additionStack} = {buffSO.stackBuffs[0].values[(int)CombineLevel] + additionStack}");
        Player.BuffStatCompo.AddStack(StackEnum.DMGMusicaldNote, buffSO.stackBuffs[0].values[(int)CombineLevel] + additionStack);
    }

    protected void AddFAINTMusicalNoteStack()
    {
        Debug.Log($"{buffSO.stackBuffs[0].values[(int)CombineLevel]} + {additionStack} = {buffSO.stackBuffs[0].values[(int)CombineLevel] + additionStack}");
        Player.BuffStatCompo.AddStack(StackEnum.FAINTMusicalNote, buffSO.stackBuffs[0].values[(int)CombineLevel] + additionStack);
    }

    public void ResetAdditionStack()
    {
        additionStack = 0;
    }
}
