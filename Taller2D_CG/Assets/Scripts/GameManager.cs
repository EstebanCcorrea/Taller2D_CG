
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;






public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip pickupClip;
    [SerializeField, Range(0f, 2f)] private float pickupVolume = 1.1f;
    [SerializeField] private AudioSource sfxSource;

    public static GameManager Instance { get; private set; }

    private float globalTime = 0f;
    public float GlobalTime { get => globalTime; set => globalTime = value; }

    public float vidaActual = 3f;
    public ScriptVida panelVida;

    public int Gema = 0;
    public int Zafiro = 0;
    public int Blink = 0;

    [Header("UI Game Over")]
    public GameObject gameOverPanel;
    public TMP_Text gemaText;
    public TMP_Text zafiroText;
    public TMP_Text blinkText;

    //  NUEVO: Lista para guardar los tiempos de las escenas
    public List<float> tiemposEscenas = new List<float>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
            if (sfxSource == null) sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
            sfxSource.loop = false;
            sfxSource.spatialBlend = 0f; // 2D
            sfxSource.volume = 1f;
        }
    }

    void Start()
    {
        panelVida.ActualizarVida(vidaActual);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
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

        if (pickupClip != null && sfxSource != null)
            sfxSource.PlayOneShot(pickupClip, pickupVolume);
    }

    public void GameOver()

    {
        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);

            if (gemaText != null) gemaText.text = "x" + Gema;
            if (zafiroText != null) zafiroText.text = "x" + Zafiro;
            if (blinkText != null) blinkText.text = "x" + Blink;
        }
    }
    
    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //  NUEVO: guardar tiempo de cada escena
    public void GuardarTiempoEscena(float tiempo)
    {
        tiemposEscenas.Add(tiempo);
        Debug.Log("Tiempo guardado de escena: " + tiempo);
    }

    // NUEVO: obtener tiempo total
    public float ObtenerTiempoTotal()
    {
        float total = 0f;
        foreach (float t in tiemposEscenas) total += t;
        return total;
    }
}
