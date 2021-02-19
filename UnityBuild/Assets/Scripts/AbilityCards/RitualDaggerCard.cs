using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class RitualDaggerCard : AbilityCard
{
    private int _maxUses = 2;

    //Decrease dice value by 1
    protected override void UseAbility(FightPanel fight)
    {
        _hideDiceOnUse = false;
        _maxUses--;
        Dice dice = _diceHolders[0].ContainedDice;
        dice.Value = Mathf.Max(dice.Value - 1, 1);
        dice.SetRollAnimation();
    }

    protected override bool CanUse(FightPanel fight)
    {
        return _maxUses > 0;
    }
}
