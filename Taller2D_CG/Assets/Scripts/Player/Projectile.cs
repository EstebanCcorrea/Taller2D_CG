using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 3f;
    public float damage = 1f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Golpea enemigo
        if (collision.CompareTag("Enemigo"))
        {
            EnemigoP enemigo = collision.GetComponent<EnemigoP>();
            if (enemigo != null)
            {
                enemigo.RecibirDaño(damage);
            }
            Destroy(gameObject);
            return;
        }

        // Choca con cualquier otra cosa (pared, suelo, etc.)
        if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
