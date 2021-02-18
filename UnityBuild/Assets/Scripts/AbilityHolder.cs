using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum AbilityID{
    Slayer,
    Banner,
    RitualDagger,
    ScrollOfLuck
}

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] private Ability[] _allAbilities;
    
    public Ability GetAbility(AbilityID id)
    {
        return _allAbilities.FirstOrDefault(a => a.ID == id);
    }
}

[System.Serializable]
public class Ability
{
    public AbilityID ID;
    public AbilityCard Card;
}