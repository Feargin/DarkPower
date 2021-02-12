using UnityEngine;
using UnityEngine.UI;
using TMPro;
 
public class BtnSwitchLanguage: MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown _dropdown;
    private LocalizationManager _localizationManager;

    private void Start()
    {
        _localizationManager = GetComponent<LocalizationManager>();
    }
 
    public void OnChangeValueInDropdown()
    {
        switch(_dropdown.value)
        {
            case 0:
            _localizationManager.LoadLocalizedText("en_US");
            break;
            case 1:
            _localizationManager.LoadLocalizedText("ru_RU");
            break;
        }
        
        
        
    }
}