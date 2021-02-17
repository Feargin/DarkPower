using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollOfLuckCard : AbilityCard
{
    private int _maxUses = 1;

    //Reroll dice
    public override void UseAbility(FightPanel fight)
    {
        _hideDiceOnUse = false;
        _maxUses--;
        Dice dice = _diceHolders[0].ContainedDice;
        dice.RollWithAnim();
    }

    public override bool CanUse(FightPanel fight)
    {
        return _maxUses > 0;
    }
}
