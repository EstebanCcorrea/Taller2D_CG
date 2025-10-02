using UnityEngine;
using UnityEngine.UI;

public class ScriptVida : MonoBehaviour
{
    public Image[] corazones;
    public Sprite corazonLleno;
    public Sprite corazonMedio;
    public Sprite corazonVacio;

    public float vidaMaxima = 4f;   
    public float vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarVida(vidaActual);
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
        vidaActual -= daño;
        if (vidaActual < 0) vidaActual = 0;

        ActualizarVida(vidaActual);
    }
}
