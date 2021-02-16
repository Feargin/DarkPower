using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AbilityHolder))]
public class PlayerAbilities : MonoBehaviour
{
    public List<AbilityID> _playerAbilities;
    private AbilityHolder _abilityHolder;

    private void Start() => _abilityHolder = GetComponent<AbilityHolder>();

    public void AddAbility(AbilityID id)
    {
        if(_playerAbilities.Contains(id) == false)
        {
            _playerAbilities.Add(id);
        }
    }

    public void RemoveAbility(AbilityID id)
    {
        if(_playerAbilities.Contains(id))
        {
            _playerAbilities.Remove(id);
        }
    }

    public Ability GetAbility(AbilityID id)
    {
        if(_playerAbilities.Contains(id))
        {
            return _abilityHolder.GetAbility(id);
        }
        return null;
    }
}