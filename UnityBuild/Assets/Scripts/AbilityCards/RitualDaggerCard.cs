using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualDaggerCard : AbilityCard
{
    private int _maxUses = 2;

    //Decrease dice value by 1
    public override void UseAbility(FightPanel fight)
    {
        _hideDiceOnUse = false;
        _maxUses--;
        Dice dice = _diceHolders[0].ContainedDice;
        dice.Value = Mathf.Max(dice.Value - 1, 1);
        dice.SetRollAnimation();
    }

    public override bool CanUse(FightPanel fight)
    {
        return _maxUses > 0;
    }
}
