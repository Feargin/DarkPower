using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class Marker : MonoBehaviour
{
    public int StarCount;
    public MarkerBonus BonusLoot;
    public int AmoutOfAbilities;
    public List<AbilityHolder.AbilityID> _possibleAbilities;
    //public MarkerAbility Ability;
    [SerializeField] private GameObject [] _stars;
    public UnityEvent _starPanel; //вызывает заданное окно

    public void Start ()
    {
        for (int i = 0; i < _stars.Length; i++)
        {
            if (i == StarCount - 1)
            {
                _stars[i].SetActive(true);
            }
        }
    }
    
    public void OnMouseDown()
    {
        if (StarCount == 0)
            return;
        //panel.SelectedMarker = this;
        if(ResourceHolder.Instance.GetResource(ResourceHolder.ResourceType.Minions) > 0) 
            _starPanel?.Invoke();
        SelectionMinions.Instance.TargetMarker = this;
    }

    /*public void Disable()
    {
        _starPanel.SetActive(false);
    }*/

    public enum MarkerBonus
    {
        none,
        bonusFightDice
    }
}
