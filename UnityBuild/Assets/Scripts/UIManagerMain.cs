using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerMain : MonoBehaviour
{
    public void LoadLevel()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            SceneManager.LoadScene(1);
            //return;
        }
        SceneManager.LoadScene(1);
        //LoadingScreenManager.LoadScene (1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
