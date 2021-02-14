using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] InfoParts;
    private int _currentPartIndex;

    private void Start()
    {
        if (PlayerPrefs.HasKey("FirstStart"))
        {
            gameObject.SetActive(false);
        }
    }

    public void OnEnable()
    {
        if (PlayerPrefs.HasKey("FirstStart"))
        {
            foreach(var part in InfoParts)
                part.SetActive(true);
        }
        else
        {
            _currentPartIndex = 0;
            foreach(var part in InfoParts)
                part.SetActive(false);
            InfoParts[_currentPartIndex].SetActive(true);
        }
    }

    public void NextPart()
    {
        if (PlayerPrefs.HasKey("FirstStart"))
        {
            gameObject.SetActive(false);
            return;
        }
        InfoParts[_currentPartIndex].SetActive(false);
        _currentPartIndex++;
        if(_currentPartIndex < InfoParts.Length)
        {
            InfoParts[_currentPartIndex].SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
            PlayerPrefs.SetInt("FirstStart", 1);
        }
    }
}
