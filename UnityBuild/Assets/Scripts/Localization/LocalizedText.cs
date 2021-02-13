using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedText : MonoBehaviour
{
    [SerializeField]
    private string _key;
    private LocalizationManager _localizationManager;
    private TextMeshProUGUI _text;
 
    private void Awake()
    {
        if (_localizationManager == null)
        {
            _localizationManager = GameObject.FindGameObjectWithTag("LocalizationManager").GetComponent<LocalizationManager>();
        }
        if(_text == null)
        {
            _text = GetComponent<TextMeshProUGUI>();
        }
        _localizationManager.OnLanguageChanged += UpdateText;
    }
 
    private void Start()
    {
        UpdateText();
    }
 
    private void OnDestroy()
    {
        _localizationManager.OnLanguageChanged -= UpdateText;
    }
    private void OnDisable() 
    {
        _localizationManager.OnLanguageChanged -= UpdateText;
    }

 
    virtual protected void UpdateText()
    {
        if (gameObject == null) return;
 
        if(_localizationManager == null)
        {
            _localizationManager = GameObject.FindGameObjectWithTag("LocalizationManager").GetComponent<LocalizationManager>();
        }
        if (_text == null)
        {
            _text = GetComponent<TextMeshProUGUI>();
        }
        _text.text = _localizationManager.GetLocalizedValue(_key);
        //print("go");
    }
}