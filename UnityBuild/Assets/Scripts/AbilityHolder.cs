using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityID{
    Slayer,
    Banner
}

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] private Ability[] _allAbilities;
    
    public Ability GetAbility(AbilityID id)
    {
        foreach(Ability a in _allAbilities)
        {
            if(a.ID == id)
            {
                return a;
            }
        }
        return null;
    }
}

[System.Serializable]
public class Ability
{
    public AbilityID ID;
    public AbilityCard Card;
}