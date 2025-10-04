using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScriptVida : MonoBehaviour
{

    public Image[] corazones;
    public Sprite corazonLleno;
    public Sprite corazonMedio;
    public Sprite corazonVacio;


    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip loseLifeClip;
    [SerializeField, Range(0f, 2f)] private float loseLifeVolume = 1.2f;


    public float vidaMaxima = 4f;   


    public float vidaActual;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            vidaActual = GameManager.Instance.vidaActual;
        }
        else
        {
            
            vidaActual = vidaMaxima;
        }
        ActualizarVida(vidaActual);

        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
            if (sfxSource == null) sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
            sfxSource.loop = false;
            sfxSource.spatialBlend = 0f; // 2D
        }
    }


    public void ActualizarVida(float nuevaVida)
    {
        vidaActual = nuevaVida;

        for (int i = 0; i < corazones.Length; i++)
        {
            if (vidaActual >= i + 1)
                corazones[i].sprite = corazonLleno;
            else if (vidaActual >= i + 0.5f)
                corazones[i].sprite = corazonMedio;
            else
                corazones[i].sprite = corazonVacio;
        }
    }

    public void RecibirDaño(float daño)
    {

        int beforeLives = Mathf.FloorToInt(vidaActual);

        vidaActual -= daño;
        if (vidaActual < 0) vidaActual = 0;

        int afterLives = Mathf.FloorToInt(vidaActual);

        if (afterLives < beforeLives && loseLifeClip != null && sfxSource != null)
            sfxSource.PlayOneShot(loseLifeClip, loseLifeVolume);

        ActualizarVida(vidaActual);


        if (vidaActual <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

}
