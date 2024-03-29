using System.Collections.Generic;
using System;
using UnityEngine;

public class ResourceHolder : MonoBehaviour
{
    public static ResourceHolder Instance;
    public Action<ResourceType, int> OnResourceChange;
    public List<int> PlayerAbilityIDPool;

    #region Player achivements
    public int BonusFightDices = 0;
    #endregion

    public enum ResourceType
    {
        Candles,
        Minions,
        PowerDise
    }

    private readonly Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _resources.Add(ResourceType.Candles, 0);
        _resources.Add(ResourceType.Minions, 0);
        _resources.Add(ResourceType.PowerDise, 0);
    }

    public void AddResource(ResourceType type, int amount)
    {
        if (!_resources.ContainsKey(type)) return;
        _resources[type] += amount;
        OnResourceChange?.Invoke(type, amount);
    }
    public int GetResource (ResourceType resource)
    {
        return _resources[resource];
    }
}
