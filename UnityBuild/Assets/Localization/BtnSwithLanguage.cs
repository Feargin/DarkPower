using UnityEngine;
 
public class BtnSwitchLanguage: MonoBehaviour
{
    [SerializeField]
    private LocalizationManager localizationManager;
 
    void OnButtonClick()
    {
        localizationManager.CurrentLanguage = name;
    }
}