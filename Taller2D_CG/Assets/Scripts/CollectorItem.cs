using UnityEngine;

public class CollectorItem : MonoBehaviour
{
    public string itemType; 
    private bool collected = false;
    [SerializeField] private AudioClip pickupClip;
    [SerializeField, Range(0f, 2f)] private float pickupVolume = 1f;


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
