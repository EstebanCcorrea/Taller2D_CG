using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour

{
    public static GameManager Instance { get; private set; }

    private float globalTime = 0f;
    public float GlobalTime { get => globalTime; set => globalTime = value; }

    public float vidaActual = 3f;
    public ScriptVida panelVida;

    public int Gema = 0;
    public int Zafiro = 0;
    public int Blink = 0;


    [Header("UI Game Over")]
    public GameObject PanelGameOver;
    public TMP_Text TextGema;
    public TMP_Text TextZafiro;
    public TMP_Text TextBlink;




    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        panelVida.ActualizarVida(vidaActual);
        if (PanelGameOver != null)
            PanelGameOver.SetActive(false);
    }

    public void SumarTiempoGlobal(float tiempoEscena)
    {
        globalTime += tiempoEscena;
    }

    public void ModificarVida(float cantidad)
    {
        vidaActual = Mathf.Clamp(vidaActual + cantidad, 0f, 3f);
        panelVida.ActualizarVida(vidaActual);
    }

    public void AddItem(string type)
    {
        switch (type)
        {
            case "Gema":
                Gema++;
                break;
            case "Zafiro":
                Zafiro++;
                break;
            case "Blink":
                Blink++;
                break;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        if (PanelGameOver != null)
            PanelGameOver.SetActive(true);

        Rigidbody2D[] allRigidbodies = FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.None);
        foreach (Rigidbody2D rb in allRigidbodies)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false; // congela la física

            TextGema.text = "Gemas: " + Gema;
            TextZafiro.text = "Zafiros: " + Zafiro;
            TextBlink.text = "Blink: " + Blink;
        }
    }

    public void ResetGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
