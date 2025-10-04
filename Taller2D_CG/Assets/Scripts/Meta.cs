using UnityEngine;
using TMPro;

public class Meta : MonoBehaviour
{
    public GameObject panelFinal;
    public TextMeshProUGUI textoTiempoFinal;
    private Timer timer;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        if (panelFinal != null) panelFinal.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (timer != null) timer.TimerStop(); // 👈 corregido

            if (panelFinal != null)
            {
                panelFinal.SetActive(true);
                float total = GameManager.Instance.ObtenerTiempoTotal();

                // Mostramos en formato mm:ss:ms
                int minutos = Mathf.FloorToInt(total / 60f);
                int segundos = Mathf.FloorToInt(total % 60f);
                int miliseg = Mathf.FloorToInt((total * 100f) % 100f);

                textoTiempoFinal.text = $"{minutos:00}:{segundos:00}:{miliseg:00}";
            }
        }
    }
}
