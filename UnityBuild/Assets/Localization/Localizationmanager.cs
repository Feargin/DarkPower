using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
 
public class LocalizationManager : MonoBehaviour
{
    private string _currentLanguage;
    private Dictionary<string, string> _localizedText;
    public static bool isReady = false;
 
    public delegate void ChangeLangText();
    public event ChangeLangText OnLanguageChanged;
    public string CurrentLanguage
    {
        get 
        {
            return _currentLanguage;
        }
        set
        {
            PlayerPrefs.SetString("Language", value);
            _currentLanguage = PlayerPrefs.GetString("Language");
        }
    }
 
    void Awake()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian 
            || Application.systemLanguage == SystemLanguage.Ukrainian 
            || Application.systemLanguage == SystemLanguage.Belarusian)
            {
                PlayerPrefs.SetString("Language", "ru_RU");
            }   
            else
            {
                PlayerPrefs.SetString("Language", "en_US");
            }
        }
        _currentLanguage = PlayerPrefs.GetString("Language");
 
        LoadLocalizedText(_currentLanguage);
    }
 
    public void LoadLocalizedText(string langName)
    {
        string path = Application.streamingAssetsPath + "/Languages/" + langName + ".json";
 
        string dataAsJson;
 
        if (Application.platform == RuntimePlatform.Android)
        {
            WWW reader = new WWW(path);
            while (!reader.isDone) { }
 
            dataAsJson = reader.text;
        }
        else
        {
            dataAsJson = File.ReadAllText(path);
        }
 
        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
 
        _localizedText = new Dictionary<string, string>();
        for (int i = 0; i < loadedData.items.Length; i++)
        {
            _localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        }
 
        PlayerPrefs.SetString("Language", langName);
        _currentLanguage = PlayerPrefs.GetString("Language");
        isReady = true;
 
        OnLanguageChanged?.Invoke();
    }
 
    public string GetLocalizedValue(string key)
    {
        if (_localizedText.ContainsKey(key))
        {
            return _localizedText[key];
        }
        else
        {
            throw new Exception("Localized text with key \"" + key + "\" not found");
        }
    }
 
    
    public bool IsReady
    {
        get
        {
            return isReady;
        }
    }
}

[System.Serializable]
public struct LocalizationData
{
    public LocalizationItem[] items;
}
[System.Serializable]
public struct LocalizationItem
{
    public string key;
    public string value;
}