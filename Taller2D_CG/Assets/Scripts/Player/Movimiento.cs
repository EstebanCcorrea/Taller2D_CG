using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [SerializeField] public float speed;
    private Rigidbody2D rb2D;

    private float move;

    public float jumpforce = 4;
    private bool isGrounded;
    [Header("Ground Check")]
    public Transform groundCheck;
    public Vector2 groundBoxSize = new Vector2(0.5f, 0.1f); // ancho/alto del box
    public LayerMask groundLayer;
    private Animator anim;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");
        rb2D.linearVelocity = new Vector2(move * speed, rb2D.linearVelocity.y);

        if (move != 0)
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);
        if (Input.GetButtonDown("Jump") && isGrounded)
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpforce);

        anim.SetFloat("Speed", Mathf.Abs(move));
        anim.SetFloat("VerticalVelocity", rb2D.linearVelocity.y);
        anim.SetBool("IsGrounded", isGrounded);

        // Debug para ver si el personaje detecta el suelo
        //Debug.Log($"{gameObject.name} - Grounded: {isGrounded}");


    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0f, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);
    }

   
}
