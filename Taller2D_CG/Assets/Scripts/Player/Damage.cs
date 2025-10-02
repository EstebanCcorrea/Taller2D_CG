using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Movimiento movimiento; //  referencia al script de movimiento
    private bool invulnerable = false;

    public float invulnerabilityTime = 1.5f; 
    public float flashSpeed = 0.1f;          // velocidad del parpadeo
    public ScriptVida ScriptVida;            

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        movimiento = GetComponent<Movimiento>();
        ScriptVida = Object.FindFirstObjectByType<ScriptVida>();
    }

    public void TakeDamage(float daño)
    {
        if (!invulnerable)
        {
            if (ScriptVida != null)
                ScriptVida.RecibirDaño(daño);

            StartCoroutine(DamageEffect());
        }
    }

    IEnumerator DamageEffect() 
    {
        invulnerable = true;

        // Desactivar movimiento mientras parpadea
        if (movimiento != null)
            movimiento.enabled = false;

        float elapsed = 0f;
        while (elapsed < invulnerabilityTime)
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(flashSpeed);

            sprite.color = Color.white;
            yield return new WaitForSeconds(flashSpeed);

            elapsed += flashSpeed * 2;
        }

        // Reactivar movimiento
        if (movimiento != null)
            movimiento.enabled = true;

        sprite.color = Color.white;
        invulnerable = false;
    }
}
