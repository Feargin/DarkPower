using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerCard : AbilityCard
{
    private int _maxUses = 1;

    //Increase dice value by 1
    public override void UseAbility(FightPanel fight)
    {
        _hideDiceOnUse = false;
        _maxUses--;
        Dice dice = _diceHolders[0].ContainedDice;
        dice.Value = Mathf.Min(dice.Value + 1, 6);
        dice.SetRollAnimation();
    }

    public override bool CanUse(FightPanel fight)
    {
        return _maxUses > 0;
    }
}
