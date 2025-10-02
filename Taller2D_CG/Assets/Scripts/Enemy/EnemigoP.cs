using UnityEngine;
using UnityEngine.Rendering;

public class EnemigoP : MonoBehaviour
{
    public float velocidad = 2f;
    public Transform puntoIzquierda;
    public Transform puntoDerecha;
    public float daño;

    [Header("Ataque")]
    public float rangoDeteccion = 3f;   // distancia para detectar al jugador
    public Transform player;


    private bool moviendoDerecha = true;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool atacando = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }



    // Cuando el enemigo detecta al jugador, inicia el ataque, sino pues sigue patrullando
    private void FixedUpdate()
    {
        if (player == null) return;

        float distancia = Vector2.Distance(transform.position, player.position);

        if (!atacando)
        {
            if (distancia <= rangoDeteccion)
            {
                
                StartAttack();
            }
            else
            {
                
                Patrullar();
            }
        }
    }

    //metodo para patrullar entre dos puntos asignados en unity
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

        // efecto de empuje al atacar
        float direccion = moviendoDerecha ? 1f : -1f;
        rb.linearVelocity = new Vector2(velocidad * 3f * direccion, rb.linearVelocity.y);

        // después de 1 segundo vuelve a su estado "normal"
        Invoke(nameof(FinAtaque), 1f);
    }

    void FinAtaque()
    {
        atacando = false;
    }

    // Si el enemigo colisiona con el jugador, le hace daño
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerDamage>().TakeDamage(daño);
        }
    }
}