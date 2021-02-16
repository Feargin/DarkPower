using UnityEngine;

public abstract class AbilityCard : MonoBehaviour
{
    [SerializeField] protected DiceHolder[] _diceHolders;

    //Require refactoring: separate FightPanel class to ??? and CurrentFightStats
    private FightPanel Fight;

    public abstract void UseAbility(FightPanel fight);
    public abstract bool CanUse(FightPanel fight);

    private void OnEnable()
    {
        foreach(DiceHolder dh in _diceHolders)
            dh.OnDicePlace += OnDicePlacement;
    }

    private void OnDisable()
    {
        foreach(DiceHolder dh in _diceHolders)
            dh.OnDicePlace -= OnDicePlacement;
    }

    /*
    *   REQUIRE: use Init() after instantiate!
    */
    public void Init()
    {
        Fight = FightPanel.Instance;
        bool flag = CanUse(Fight);
        foreach(DiceHolder dh in _diceHolders)
        {
            dh.canPlayerPlaceDice = flag;
        }
    }

    private void OnDicePlacement(DiceHolder holder, AbilityCard card)
    {
        int diceAmount = 0;
        foreach(DiceHolder dh in _diceHolders)
        {
            if(dh.ContainedDice != null)
                diceAmount++;
        }
        if(diceAmount == _diceHolders.Length)
        {
            UseAbility(Fight);
            foreach(DiceHolder dh in _diceHolders)
                dh.DeselectDice();
            if(!CanUse(Fight))
            {
                foreach(DiceHolder dh in _diceHolders)
                    dh.canPlayerPlaceDice = false;
            }
        }
    }
}
