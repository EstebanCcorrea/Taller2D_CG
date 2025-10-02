using UnityEngine;
using UnityEngine.Rendering;

public class EnemigoP : MonoBehaviour
{
    public float velocidad = 2f;
    public Transform puntoIzquierda;
    public Transform puntoDerecha;
    public float daño;
   

    private bool moviendoDerecha = true;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }

    // Movimiento entre puntos

    private void FixedUpdate()
    {
        if (moviendoDerecha)
        {
            rb.linearVelocity = new Vector2(velocidad, rb.linearVelocity.y);
            if (transform.position.x >= puntoDerecha.position.x)
                moviendoDerecha = false;
        }
        else
        {
            rb.linearVelocity = new Vector2(-velocidad, rb.linearVelocity.y);
            if (transform.position.x <= puntoIzquierda.position.x)
                moviendoDerecha = true;
        }

        spriteRenderer.flipX = !moviendoDerecha;


        //if (animator != null)
        //{
        //    animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        //}
    }

    private void OnTriggerEnter2D(Collider2D other)

    {
        if (other.gameObject.CompareTag("Player"))

        {

            other.gameObject.GetComponent<PlayerDamage>().TakeDamage(1f);

        }
    
    }

}
