using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerMain : MonoBehaviour
{
    public void LoadLevel()
    {
        LoadingScreenManager.LoadScene (1);
        //SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
