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

    [Header("SFX")]
    [SerializeField] private AudioSource sfxSource;        
    [SerializeField] private AudioClip[] footstepClips;    
    [SerializeField] private float stepInterval = 0.35f;   
    [SerializeField] private float moveThreshold = 0.1f;   
    [SerializeField] private float minPitch = 0.95f;      
    [SerializeField] private float maxPitch = 1.05f;
    [SerializeField, Range(0f, 2f)] private float footstepVolume = 1f;

 
    [SerializeField] private AudioClip jumpClip;
    [SerializeField, Range(0f, 2f)] private float jumpVolume = 1f;



    private float stepTimer;

    private Rigidbody2D rb2D;
    private Animator anim;
    private bool isGrounded;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Configurar AudioSource si no se asignó
        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
            if (sfxSource == null) sfxSource = gameObject.AddComponent<AudioSource>();
        }
        sfxSource.playOnAwake = false;
        sfxSource.loop = false;
        sfxSource.spatialBlend = 0f; // 2D
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

        bool movingHoriz = Mathf.Abs(move) > moveThreshold;
        bool onGroundNow = isGrounded && Mathf.Abs(rb2D.linearVelocity.y) < 0.05f;

        if (movingHoriz && onGroundNow)
        {
            // Acelera la cadencia un poco si te mueves más rápido
            float speedFactor = Mathf.Clamp01(Mathf.Abs(move)); // 0..1
            float interval = Mathf.Lerp(stepInterval * 1.1f, stepInterval * 0.8f, speedFactor);

            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = interval;
            }
        }
        else
        {
            // Resetea para que al volver a tocar suelo no “dispare” demasiado seguido
            stepTimer = 0.05f;
        }
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

        if (jumpClip != null && sfxSource != null)
            sfxSource.PlayOneShot(jumpClip, jumpVolume);
    }

    private void PlayFootstep()
    {
        if (footstepClips == null || footstepClips.Length == 0 || sfxSource == null) return;

        int i = Random.Range(0, footstepClips.Length);
        var clip = footstepClips[i];
        if (clip == null) return;

        sfxSource.pitch = Random.Range(minPitch, maxPitch);
        sfxSource.PlayOneShot(clip, footstepVolume);




    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);
    }



}
