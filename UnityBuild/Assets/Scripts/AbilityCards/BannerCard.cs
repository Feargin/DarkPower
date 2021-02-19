using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BannerCard : AbilityCard
{
    private int _maxUses = 1;

    //Increase dice value by 1
    protected override void UseAbility(FightPanel fight)
    {
        _hideDiceOnUse = false;
        _maxUses--;
        Dice dice = _diceHolders[0].ContainedDice;
        dice.Value = Mathf.Min(dice.Value + 1, 6);
        dice.SetRollAnimation();
    }

    protected override bool CanUse(FightPanel fight)
    {
        return _maxUses > 0;
    }
}
