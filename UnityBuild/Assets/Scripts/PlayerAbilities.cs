using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AbilityHolder))]
public class PlayerAbilities : MonoBehaviour
{
    public List<AbilityHolder.AbilityID> _playerAbilities;
    private AbilityHolder _abilityHolder;

    private void Start() => _abilityHolder = GetComponent<AbilityHolder>();

    public void AddAbility(AbilityHolder.AbilityID id)
    {
        if(_playerAbilities.Contains(id) == false)
        {
            _playerAbilities.Add(id);
        }
    }

    public void RemoveAbility(AbilityHolder.AbilityID id)
    {
        if(_playerAbilities.Contains(id))
        {
            _playerAbilities.Remove(id);
        }
    }

    public AbilityHolder.Ability GetAbility(AbilityHolder.AbilityID id)
    {
        return _playerAbilities.Contains(id) ? _abilityHolder.GetAbility(id) : null;
    }
}