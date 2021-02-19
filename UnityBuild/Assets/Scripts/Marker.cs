using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    public int StarCount;
    public MarkerBonus BonusLoot;
    public int AmoutOfAbilities;
    public List<AbilityID> _possibleAbilities;
    //public MarkerAbility Ability;
    [SerializeField] private GameObject [] _stars;
    [SerializeField] private GameObject _starPanel;

    public void Start ()
    {
        for(int i = 0; i < StarCount; i++)
        {
            _stars[i].SetActive(true);
        }
    }
    
    public void Activate(EventPanel panel)
    {
        if (StarCount == 0)
            return;
        panel.SelectedMarker = this;
        if(ResourceHolder.Instance.GetResource(ResourceHolder.ResourceType.Minions) > 0) 
            panel.gameObject.SetActive(true);
        SelectionMinions.Instance.TargetMarker = this;
    }

    public void Disable()
    {
        _starPanel.SetActive(false);
    }

    public enum MarkerBonus
    {
        none,
        bonusFightDice
    }

    public enum MarkerAbility
    {
        none,
        reroll1s
    }
}
