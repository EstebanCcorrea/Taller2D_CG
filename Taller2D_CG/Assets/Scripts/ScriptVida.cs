using UnityEngine;
using UnityEngine.UI;

public class ScriptVida : MonoBehaviour
{
    [Header("Sprites de corazones")]
    public Image[] corazones;
    public Sprite corazonLleno;
    public Sprite corazonMedio;
    public Sprite corazonVacio;

    [Header("Valores de vida")]
    public float vidaMaxima = 3f;
    public float vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarVida(vidaActual);
    }

    public void RecibirDaño(float daño)
    {
        vidaActual -= daño;
        if (vidaActual < 0) vidaActual = 0;

        ActualizarVida(vidaActual);

        //  Avisar al GameManager si ya no hay vida
        if (vidaActual <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    //public void CurarVida(float cantidad)
    //{
    //    vidaActual += cantidad;
    //    if (vidaActual > vidaMaxima) vidaActual = vidaMaxima;

    //    ActualizarVida(vidaActual);
    //}

    public void ActualizarVida(float vida)
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            if (vida >= i + 1)
                corazones[i].sprite = corazonLleno;
            else if (vida >= i + 0.5f)
                corazones[i].sprite = corazonMedio;
            else
                corazones[i].sprite = corazonVacio;
        }
    }
}
