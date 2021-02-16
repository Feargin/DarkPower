using UnityEngine.UI;
using UnityEngine;

public class TextUpdate : MonoBehaviour
{
    [SerializeField] private ResourceHolder.ResourceType _resourceType;
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        ResourceHolder.Instance.OnResourceChange += OnResourceChange;
    }

    private void OnDisable()
    {
        ResourceHolder.Instance.OnResourceChange -= OnResourceChange;
    }

    private void OnResourceChange(ResourceHolder.ResourceType type, int amount)
    {
        if(_resourceType == type)
            _text.text = "" + amount;
    }
}
