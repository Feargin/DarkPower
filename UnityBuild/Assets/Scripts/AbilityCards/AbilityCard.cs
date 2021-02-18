using UnityEngine.EventSystems;
using UnityEngine;

public abstract class AbilityCard : MonoBehaviour
{
    [SerializeField] protected DiceHolder[] _diceHolders;
    [SerializeField] protected GameObject _disablePanel;
    protected bool _hideDiceOnUse = true;

    //Require refactoring: separate FightPanel class to ??? and CurrentFightStats 
    private FightPanel Fight;

    public abstract void UseAbility(FightPanel fight);
    public abstract bool CanUse(FightPanel fight);

    private void OnEnable()
    {
        foreach(DiceHolder dh in _diceHolders)
            dh.OnDicePlace += OnDicePlacement;
        Dice.OnDicePlacement += OnAnyDicePlacement;
    }

    private void OnDisable()
    {
        foreach(DiceHolder dh in _diceHolders)
            dh.OnDicePlace -= OnDicePlacement;
        Dice.OnDicePlacement -= OnAnyDicePlacement;
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

    public void CheckAvailable()
    {
        if(!CanUse(Fight))
        {
            foreach(DiceHolder dh in _diceHolders)
                dh.canPlayerPlaceDice = false;
            if(_disablePanel != null)
                _disablePanel.SetActive(true);
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
            var dices = Fight.GetPlayerDice();
            foreach(DiceHolder dh in _diceHolders)
            {
                if(_hideDiceOnUse)
                    dices.Remove(dh.ContainedDice);
                dh.DeselectDice(_hideDiceOnUse);
            }
            CheckAvailable();
        }
    }

    private void OnAnyDicePlacement(Dice dice, RaycastResult result)
    {
        CheckAvailable();
    }
}
