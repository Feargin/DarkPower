using System.Collections.Generic;
using UnityEngine;

public class SlayerCard : AbilityCard
{
    //Destroy 2 random enemy dice
    public override void UseAbility(FightPanel fight)
    {
        List<Dice> dice = fight.GetEnemyDice();

        //Dice#1
        int index = Random.Range(0, dice.Count);
        dice[index].Disable();
        dice.RemoveAt(index);

        if(fight.GetEnemyDice().Count > 1)
        {
            //Dice#2
            index = Random.Range(0, dice.Count);
            dice[index].Disable();
            dice.RemoveAt(index);
        }
    }

    public override bool CanUse(FightPanel fight)
    {
        return fight.GetEnemyDice().Count > 1 && fight.GetPlayerDice().Count > 2;
    }
}
