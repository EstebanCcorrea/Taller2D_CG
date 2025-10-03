using UnityEngine;

public class MusicStarter : MonoBehaviour
{
    [Tooltip("Musica_escena1")]
    public AudioClip sceneMusic;

    [Range(0f, 2f)]
    public float fadeTime = 0.5f;

    void Start()
    {
        if (sceneMusic != null && AudioManager.I != null)
        {
            AudioManager.I.PlayMusic(sceneMusic, fadeTime);
        }
    }
}

