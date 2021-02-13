using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] InfoParts;
    private int _currentPartIndex;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Training"))
        {

        }
    }

    public void OnEnable()
    {
        _currentPartIndex = 0;
        foreach(var part in InfoParts)
            part.SetActive(false);
        InfoParts[_currentPartIndex].SetActive(true);
    }

    public void NextPart()
    {
        InfoParts[_currentPartIndex].SetActive(false);
        _currentPartIndex++;
        if(_currentPartIndex < InfoParts.Length)
        {
            InfoParts[_currentPartIndex].SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
