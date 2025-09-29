using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager I { get; private set; }

    [Header("Mainmusicgame")]
    public AudioClip defaultMusic;

    private AudioSource musicSource;


    void Awake()
    {
        if (I != null && I != this) {  Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.spatialBlend = 0f;
        musicSource.volume = 1f;


    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (defaultMusic != null)
        {
            musicSource.clip = defaultMusic;
            musicSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
