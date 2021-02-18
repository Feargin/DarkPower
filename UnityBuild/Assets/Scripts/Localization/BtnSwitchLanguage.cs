using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
 
public class BtnSwitchLanguage: MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown _dropdown;

    private void Start()
    {
        if(LocalizationManager.Instance.CurrentLanguage == "en_US")
            _dropdown.value = 0;
        else if(LocalizationManager.Instance.CurrentLanguage == "ru_RU")
            _dropdown.value = 1;
    }
 
    public void OnChangeValueInDropdown()
    {
        switch(_dropdown.value)
        {
            case 0:
            if (Application.platform == RuntimePlatform.Android) 
                LocalizationManager.Instance.LoadLocalizedTextAndroid("en_US");
            else
                LocalizationManager.Instance.LoadLocalizedText("en_US");
            
            break;
            case 1:
            if (Application.platform == RuntimePlatform.Android) 
                LocalizationManager.Instance.LoadLocalizedTextAndroid("ru_RU");
            else
                LocalizationManager.Instance.LoadLocalizedText("ru_RU");
            break;
        }
        
        
        
    }
}