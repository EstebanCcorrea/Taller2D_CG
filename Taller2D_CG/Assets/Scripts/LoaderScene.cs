using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScene : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
       

        SceneManager.LoadScene(sceneName);
    }
}