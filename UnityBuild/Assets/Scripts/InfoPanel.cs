using UnityEngine;
using UnityEngine.Serialization;

public class InfoPanel : MonoBehaviour
{
    [FormerlySerializedAs("InfoParts")] [SerializeField] private GameObject[] _infoParts;
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
            foreach(var part in _infoParts)
                part.SetActive(true);
        }
        else
        {
            _currentPartIndex = 0;
            foreach(var part in _infoParts)
                part.SetActive(false);
            _infoParts[_currentPartIndex].SetActive(true);
        }
    }

    public void NextPart()
    {
        if (PlayerPrefs.HasKey("FirstStart"))
        {
            gameObject.SetActive(false);
            return;
        }
        _infoParts[_currentPartIndex].SetActive(false);
        _currentPartIndex++;
        if(_currentPartIndex < _infoParts.Length)
        {
            _infoParts[_currentPartIndex].SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
            PlayerPrefs.SetInt("FirstStart", 1);
        }
    }
}
