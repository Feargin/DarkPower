using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public sealed class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _initialPanel;
    private GameObject _lastPanel;
    public static UIManager Instance;

    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void Exit() => Application.Quit();

    private void Awake() 
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start() => EnablePanel(_initialPanel);

    public void EnablePanel(GameObject panel)
    {
        if (_lastPanel != null)
            _lastPanel.SetActive(false);
        _lastPanel = panel;
        _lastPanel.SetActive(true);
    }
}
