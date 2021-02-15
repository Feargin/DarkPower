using UnityEngine.EventSystems;
using UnityEngine;

public class DiceHolder : MonoBehaviour
{
    [SerializeField, Range(0, 6)] private int _requiredValue = 0;
   public bool canPlayerPlaceDice = true;
    public AbilityCard Ability;
    [HideInInspector] public Dice ContainedDice;

    public System.Action<DiceHolder, AbilityCard> OnDicePlace;

    private void OnEnable()
    {
        Dice.OnDicePlacement += OnDicePlacement;
    }

    private void OnDisable()
    {
        Dice.OnDicePlacement -= OnDicePlacement;
    }

    private void OnDicePlacement(Dice dice, RaycastResult raycastResult)
    {
        if(dice == ContainedDice)
            ContainedDice = null;
        if(raycastResult.gameObject == gameObject && canPlayerPlaceDice)
        {
            if(_requiredValue == 0 || _requiredValue == dice.Value)
            {
                ContainedDice = dice;
                ContainedDice.transform.position = transform.position;
                OnDicePlace?.Invoke(this, Ability);
            }
        }
    }

    public void SelectDice(Dice dice, bool setPos = true)
    {
        DeselectDice();
        ContainedDice = dice;
        if(setPos)
            ContainedDice.transform.position = transform.position;
    }

    public void DeselectDice(bool hideDice = true)
    {
        if(ContainedDice != null && hideDice)
            ContainedDice.Disable();
        ContainedDice = null;
    }
}
