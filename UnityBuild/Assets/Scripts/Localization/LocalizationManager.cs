using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
 
public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;
    private string _currentLanguage;
    private Dictionary<string, string> _localizedText;
    public static bool IsReady = false;
    public Action OnLanguageChanged;
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
 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

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
 
        if (Application.platform == RuntimePlatform.Android) 
            LoadLocalizedTextAndroid(_currentLanguage);
        else
            LoadLocalizedText(_currentLanguage);
            
    }       
 
    public void LoadLocalizedTextAndroid(string langName)
    {
        //string path = Path.Combine("jar:file://" + Application.dataPath + "!assets/Languages/" + langName + ".json");
        var path = Resources.Load("Languages/" + langName) as TextAsset; 
        string dataAsJson;
        //UnityWebRequest reader = UnityWebRequest.Get(path.text);
        //yield return reader.SendWebRequest();
        //Debug.LogWarning(path);
        if (!path)
        {
            throw new Exception("Localized text with " + path);
        }
        dataAsJson = path.text;
        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
 
        _localizedText = new Dictionary<string, string>();
        for (int i = 0; i < loadedData.items.Length; i++)
        {
            _localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        }
        //Debug.Log("Загруженные данные: " + _localizedText.Count + " объектов");
        
        PlayerPrefs.SetString("Language", langName);
        _currentLanguage = PlayerPrefs.GetString("Language");
        IsReady = true;
        OnLanguageChanged?.Invoke();
    }

    public void LoadLocalizedText(string langName)
    {
        _localizedText = new Dictionary<string, string>();


        //string filePath = Path.Combine(Application.streamingAssetsPath, "Languages/" + langName + ".json");
        var filePath = Resources.Load("Languages/" + langName) as TextAsset;
        Debug.Log(filePath);
        if (filePath)
        {
            string dataAsJson = filePath.text;
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                _localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }
            Debug.Log("Загруженные данные: " + _localizedText.Count + " объектов");
        }
        else
        {
            Debug.LogError("Невозможно найти файл!");
        }
        PlayerPrefs.SetString("Language", langName);
        _currentLanguage = PlayerPrefs.GetString("Language");
        IsReady = true;
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