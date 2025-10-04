using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 3f;
    public float damage = 1f;

    [Header("SFX")]
    [SerializeField] private AudioClip shootClip;
    [SerializeField, Range(0f, 2f)] private float shootVolume = 1f;
    [SerializeField] private bool playOnSpawn = true;
    [SerializeField] private Vector2 pitchRange = new Vector2(0.98f, 1.02f);

    private void Start()
    {
        if (playOnSpawn && shootClip != null)
            PlayOneShot2D(shootClip, shootVolume, pitchRange);

        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            EnemigoP enemigo = collision.GetComponent<EnemigoP>();
            if (enemigo != null) enemigo.RecibirDaño(damage);
            Destroy(gameObject);
            return;
        }

        if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private static void PlayOneShot2D(AudioClip clip, float volume, Vector2 pitchRange)
    {
        var go = new GameObject("OneShot2D_SFX");
        var src = go.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.loop = false;
        src.spatialBlend = 0f;               
        src.volume = 1f;
        src.pitch = Random.Range(pitchRange.x, pitchRange.y);
        src.PlayOneShot(clip, volume);
        Object.Destroy(go, clip.length * 1.1f);  
    }
}
