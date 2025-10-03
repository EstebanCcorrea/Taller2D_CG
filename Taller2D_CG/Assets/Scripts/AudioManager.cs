using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager I { get; private set; }

    [Header("MusicaMenu")]
    public AudioClip defaultMusic;

    private AudioSource musicSource;

    void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.spatialBlend = 0f; // 2D
        musicSource.volume = 1f;
    }

    void Start()
    {
        if (defaultMusic != null)
            PlayMusic(defaultMusic, 0f);
    }

    public void PlayMusic(AudioClip clip, float fadeTime = 0.5f)
    {
        if (clip == null) return;
        StopAllCoroutines();
        StartCoroutine(FadeInMusic(clip, fadeTime));
    }

    private IEnumerator FadeInMusic(AudioClip clip, float time)
    {
        if (musicSource.clip == clip && musicSource.isPlaying) yield break;

        float startVol = musicSource.volume;

        // fade out
        for (float t = 0; t < time; t += Time.unscaledDeltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVol, 0f, t / time);
            yield return null;
        }

        musicSource.volume = 0f;
        musicSource.clip = clip;
        musicSource.Play();

        for (float t = 0; t < time; t += Time.unscaledDeltaTime)
        {
            musicSource.volume = Mathf.Lerp(0f, 1f, t / time);
            yield return null;
        }
        musicSource.volume = 1f;
    }

    public void StopMusic() => musicSource.Stop();
}
