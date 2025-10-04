using UnityEngine;
using UnityEngine.Rendering;

public class EnemigoP : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 2f;
    public Transform puntoIzquierda;
    public Transform puntoDerecha;

    [Header("Combate")]
    public float rangoDeteccion = 3f;
    public float daño = 1f;
    public float vida = 3f;
    public Transform player;

    private bool moviendoDerecha = true;
    private bool atacando = false;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distancia = Vector2.Distance(transform.position, player.position);

        if (!atacando)
        {
            if (distancia <= rangoDeteccion)
                StartAttack();
            else
                Patrullar();
        }
    }

    void Patrullar()
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
        animator.SetBool("Run", true);
    }

    void StartAttack()
    {
        atacando = true;
        animator.SetBool("Run", false);
        animator.SetTrigger("Roll");

        float direccion = moviendoDerecha ? 1f : -1f;
        rb.linearVelocity = new Vector2(velocidad * 3f * direccion, rb.linearVelocity.y);

        Invoke(nameof(FinAtaque), 1f);
    }

    void FinAtaque()
    {
        atacando = false;
    }

    public void RecibirDaño(float daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            Muerte();
        }
    }

    void Muerte()
    {
        animator.SetTrigger("Death");
        rb.linearVelocity = Vector2.zero;
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDamage pd = other.GetComponent<PlayerDamage>();
            if (pd != null)
            {
                pd.TakeDamage(daño);
            }
        }
    }
}