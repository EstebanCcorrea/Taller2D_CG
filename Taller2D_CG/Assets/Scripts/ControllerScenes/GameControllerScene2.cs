using UnityEngine;
using TMPro;

public class ControllerScene : MonoBehaviour
{
    [Header("UI Resumen")]
    public GameObject panelResumen;   // Panel de resumen oculto en la escena
    public TMP_Text itemsTotalText;   // Texto para mostrar total de ítems

    private void Start()
    {
        if (panelResumen != null)
            panelResumen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MostrarResumen(collision.gameObject);
        }
    }

    void MostrarResumen(GameObject player)
    {
        if (panelResumen != null)
            panelResumen.SetActive(true);

        // Mostrar total de ítems guardados en el GameManager
        int totalItems = GameManager.Instance.GetTotalItems();
        itemsTotalText.text = "Ítems obtenidos: " + totalItems;

        // Congelar movimiento del player
        Movimiento mov = player.GetComponent<Movimiento>();
        if (mov != null) mov.enabled = false;
    }
}
