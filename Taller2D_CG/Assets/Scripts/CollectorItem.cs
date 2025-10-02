using UnityEngine;

public class CollectorItem : MonoBehaviour
{
    public string itemType; // Asigna en el Inspector: "Gema", "Zafiro" o "Blink"
    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected) return; // evita doble conteo
        if (collision.CompareTag("Player"))
        {
            CollecItem collector = collision.GetComponent<CollecItem>();
            if (collector != null)
            {
                collected = true;
                collector.AddItem(itemType);
                gameObject.SetActive(false); // lo quitamos de escena
                Destroy(gameObject);         // destrucción definitiva
            }
        }
    }
}
