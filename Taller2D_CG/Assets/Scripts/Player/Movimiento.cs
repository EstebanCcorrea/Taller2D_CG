using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float speed = 5f;

    [Header("Salto")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private int saltosExtras = 1;   // 1 = doble salto, 2 = triple salto...
    private int saltosRestantes;
    private bool salto;                              // buffer del input

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundBoxSize = new Vector2(0.5f, 0.1f);
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb2D;
    private Animator anim;

    private bool isGrounded;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // --- Movimiento horizontal ---
        float move = Input.GetAxis("Horizontal");
        rb2D.linearVelocity = new Vector2(move * speed, rb2D.linearVelocity.y);

        if (move != 0)
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);

        // --- Reset de saltos al tocar suelo ---
        if (isGrounded)
        {
            saltosRestantes = saltosExtras;
        }

        // --- Input de salto ---
        if (Input.GetButtonDown("Jump"))
        {
            salto = true; // se procesa en FixedUpdate
        }

        // --- Animaciones ---
        anim.SetFloat("Speed", Mathf.Abs(move));
        anim.SetFloat("VerticalVelocity", rb2D.linearVelocity.y);
        anim.SetBool("IsGrounded", isGrounded);
    }

    private void FixedUpdate()
    {
        // Detectar suelo
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0f, groundLayer);

        // Procesar salto
        if (salto)
        {
            if (isGrounded)
            {
                DoJump();
                anim.SetTrigger("Jump"); // Trigger animación
                Debug.Log(">>> Trigger Jump lanzado");
            }
            else if (saltosRestantes > 0)
            {
                DoJump();
                anim.SetTrigger("DoubleJump"); // Trigger animación
                saltosRestantes--;
                Debug.Log(">>> Trigger DoubleJump lanzado");
            }

            salto = false;
        }
    }

    private void DoJump()
    {
        rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpForce);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);
    }

}
