using UnityEngine;

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
}
