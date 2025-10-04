using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float speed = 5f;

    [Header("Salto")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private int saltosExtras = 1;
    private int saltosRestantes;
    private bool salto;


    [SerializeField] private AudioClip jumpClip;
    [SerializeField, Range(0f, 2f)] private float jumpVolume = 1f;


    private bool wasGrounded;


    [Header("SFX")]
    [SerializeField] private AudioSource sfxSource; 
    [SerializeField] private AudioClip[] footstepClips; 
    [SerializeField] private float stepInterval = 0.35f;
    [SerializeField] private float moveThreshold = 0.1f; 
    [SerializeField] private float minPitch = 0.95f;
    [SerializeField] private float maxPitch = 1.05f;
    [SerializeField, Range(0f, 2f)] private float footstepVolume = 1f;

    private float stepTimer;


    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundBoxSize = new Vector2(0.5f, 0.1f);
    [SerializeField] private LayerMask groundLayer;

    [Header("Ataque / Proyectil")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float fireCooldown = 0.3f;
    private float nextFireTime;

    private Rigidbody2D rb2D;
    private Animator anim;
    private bool isGrounded;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
            if (sfxSource == null) sfxSource = gameObject.AddComponent<AudioSource>();
        }
        sfxSource.playOnAwake = false;
        sfxSource.loop = false;
        sfxSource.spatialBlend = 0f; 


    }

    private void Update()
    {
        // Movimiento
        float move = Input.GetAxis("Horizontal");
        rb2D.linearVelocity = new Vector2(move * speed, rb2D.linearVelocity.y);

        if (move != 0)
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);

        // Reset saltos al tocar suelo
        if (isGrounded)
            saltosRestantes = saltosExtras;

        // Input salto
        if (Input.GetButtonDown("Jump"))
            salto = true;

        // Ataque
        HandleAttack();

        // Animaciones
        anim.SetFloat("Speed", Mathf.Abs(move));
        anim.SetFloat("VerticalVelocity", rb2D.linearVelocity.y);
        anim.SetBool("IsGrounded", isGrounded);


        float vx = rb2D.linearVelocity.x;
        bool movingHoriz = Mathf.Abs(vx) > moveThreshold;
        bool onGroundNow = isGrounded; 

        if (movingHoriz && onGroundNow)
        {
            float speedFactor = Mathf.Clamp01(Mathf.Abs(vx) / speed);
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
            stepTimer = 0.05f; 
        }





    }

    private void FixedUpdate()
    {
        // Detectar suelo
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0f, groundLayer);

        // Saltar
        if (salto)
        {
            if (isGrounded)
            {
                DoJump();
                anim.SetTrigger("Jump");
            }
            else if (saltosRestantes > 0)
            {
                DoJump();
                anim.SetTrigger("DoubleJump");
                saltosRestantes--;
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


    private void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time >= nextFireTime)
        {
            LaunchProjectile();
            nextFireTime = Time.time + fireCooldown;
        }
    }

    private void LaunchProjectile()
    {
        if (projectilePrefab == null || firePoint == null) return;

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rbProj = proj.GetComponent<Rigidbody2D>();

        float dir = Mathf.Sign(transform.localScale.x);
        rbProj.linearVelocity = new Vector2(dir * projectileSpeed, 0f);

        // Evitar que el proyectil golpee al jugador
        Collider2D playerCol = GetComponent<Collider2D>();
        Collider2D projCol = proj.GetComponent<Collider2D>();
        if (playerCol && projCol) Physics2D.IgnoreCollision(playerCol, projCol);

        // Voltear sprite según dirección
        Vector3 scale = proj.transform.localScale;
        proj.transform.localScale = new Vector3(dir > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x), scale.y, scale.z);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);
        }

        if (firePoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(firePoint.position, 0.08f);
        }
    }

    private void PlayFootstep()
    {
        if (sfxSource == null || footstepClips == null || footstepClips.Length == 0) return;

        int i = Random.Range(0, footstepClips.Length);
        var clip = footstepClips[i];
        if (clip == null) return;

        sfxSource.pitch = Random.Range(minPitch, maxPitch);
        sfxSource.PlayOneShot(clip, footstepVolume);
    }




}
