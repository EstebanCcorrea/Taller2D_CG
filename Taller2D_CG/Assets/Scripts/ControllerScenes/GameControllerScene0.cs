using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerScene0 : MonoBehaviour
{
    [SerializeField]

   
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }
}
