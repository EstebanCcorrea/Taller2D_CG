using UnityEngine;

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
}
