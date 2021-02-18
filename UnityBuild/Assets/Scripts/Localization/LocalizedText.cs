using TMPro;
using UnityEngine;

namespace Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string _key;
        private TextMeshProUGUI _text;
 
        private void Awake()
        {
            if(_text == null)
            {
                _text = GetComponent<TextMeshProUGUI>();
            }
        
        }
 
        private void Start()
        {
            LocalizationManager.Instance.OnLanguageChanged += UpdateText;
            UpdateText();

        }
 
        private void OnDestroy()
        {
            LocalizationManager.Instance.OnLanguageChanged -= UpdateText;
        }
        private void OnDisable() 
        {
            LocalizationManager.Instance.OnLanguageChanged -= UpdateText;
        }

 
        protected virtual void UpdateText()
        {
            if (gameObject == null) return;
            if (_text == null)
            {
                _text = GetComponent<TextMeshProUGUI>();
            }
            _text.text = LocalizationManager.Instance.GetLocalizedValue(_key);
        }
    }
}