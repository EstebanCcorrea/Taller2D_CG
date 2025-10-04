using UnityEngine;
using TMPro;

public class GameControllerScene2 : MonoBehaviour
{
    public GameObject finalPanel;
    public TextMeshProUGUI tiempoTotalText;

    void Start()
    {
        if (finalPanel != null)
            finalPanel.SetActive(false);
    }

    public void MostrarPanelFinal()
    {
        if (finalPanel != null)
        {
            finalPanel.SetActive(true);

            if (tiempoTotalText != null)
            {
                float total = GameManager.Instance.ObtenerTiempoTotal();

                int minutos = (int)total / 60;
                int segundos = (int)total % 60;

                tiempoTotalText.text = $"{minutos:D2}:{segundos:D2}";
            }
        }
    }
}
