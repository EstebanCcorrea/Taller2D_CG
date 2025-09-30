using UnityEngine;


public class GameManager : MonoBehaviour

{
    public static GameManager Instance;

    // Tiempo global acumulado
    private float globalTime = 0f;
    public float GlobalTime { get => globalTime; set => globalTime = value; }

    // Vida del jugador
    public float vidaActual = 3f;
    public ScriptVida panelVida;

    // Monedas recolectadas
    public int Monedas { get; private set; } = 0;
    public TMPro.TextMeshProUGUI textoMonedas;


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

    public void SumarMonedas(int cantidad)
    { 
        Monedas += cantidad;

        if (textoMonedas != null)
        {
            textoMonedas.text = Monedas.ToString();
        }
    }
}



