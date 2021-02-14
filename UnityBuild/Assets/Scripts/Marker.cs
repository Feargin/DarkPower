using UnityEngine;

public class Marker : MonoBehaviour
{
    public int StarCount;
    public MarkerBonus BonusLoot;
    public MarkerAbility Ability;
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
        if(GameManager.Instance.GetResource(GameManager.ResourceType.Minions) > 0) 
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
