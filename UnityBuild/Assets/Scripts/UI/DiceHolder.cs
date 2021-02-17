using UnityEngine.EventSystems;
using UnityEngine;

public class DiceHolder : MonoBehaviour
{
    [SerializeField, Range(1, 6)] private int _minValue = 1;
    [SerializeField, Range(1, 6)] private int _maxValue = 6;
    [SerializeField] private DiceHolder _sameValueRequired;
    public bool canPlayerPlaceDice = true;
    public AbilityCard Ability;
    public Dice ContainedDice;

    public System.Action<DiceHolder, AbilityCard> OnDicePlace;

    private void OnEnable()
    {
        if(_maxValue < _minValue)
            _maxValue = _minValue;
        Dice.OnDicePlacement += OnDicePlacement;
        Dice.OnDiceBeginDrag += OnDiceBeginDrag;
    }

    private void OnDisable()
    {
        Dice.OnDicePlacement -= OnDicePlacement;
        Dice.OnDiceBeginDrag -= OnDiceBeginDrag;
    }

    private void OnDiceBeginDrag(Dice dice, RaycastResult raycastResult)
    {
        if(dice == ContainedDice)
        {
            ContainedDice = null;
        }
    }

    private void OnDicePlacement(Dice dice, RaycastResult raycastResult)
    {
        if(dice == ContainedDice)
            ContainedDice = null;
        if(raycastResult.gameObject == gameObject && canPlayerPlaceDice)
        {
            if(dice.Value >= _minValue && dice.Value <= _maxValue)
            {
                if(_sameValueRequired != null && _sameValueRequired.ContainedDice != null && _sameValueRequired.ContainedDice.Value != dice.Value)
                    return;
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
