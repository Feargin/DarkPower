using System.Linq;
using UnityEngine;



public class AbilityHolder : MonoBehaviour
{
    [SerializeField] private Ability[] _allAbilities;
    public AbilityID AbilityId;
    
    public Ability GetAbility(AbilityID id) 
    {
        return _allAbilities.FirstOrDefault(a => a.ID == id);
    }
    public enum AbilityID
    {
        Slayer,
        Banner,
        RitualDagger,
        ScrollOfLuck
    }
    [System.Serializable]
    public class Ability
    {
        public AbilityID ID;
        public AbilityCard Card;
    }
}


