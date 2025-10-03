using UnityEngine;

public class CollectorItem : MonoBehaviour
{
    public string itemType; 
    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected) return; 
        if (collision.CompareTag("Player"))
        {
            collected = true;

           
            GameManager.Instance.AddItem(itemType);

            // Actualizar UI (corrigiendo obsolescencia y null propagation)
            CollecItem collecItem = Object.FindFirstObjectByType<CollecItem>();
            if (collecItem != null)
            {
                collecItem.UpdateUI();
            }

            // Destruir ítem
            Destroy(gameObject);
        }
    }
}
