using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;




public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;
    [Header("Video")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private AudioSource videoAudio;

    [Header("Flujo")]
    [SerializeField] private string sceneToLoad = "Scene1";
    [SerializeField] private bool allowSkip = true;

    public void OnPressStart()
    {
        StartCoroutine(PlayIntroThenLoad());
    }

    private IEnumerator PlayIntroThenLoad()
    {
        if (menuCanvas) menuCanvas.SetActive(false); 


        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared) yield return null;

        videoPlayer.Play();
        if (videoAudio != null && videoPlayer.audioOutputMode == VideoAudioOutputMode.AudioSource)
            videoAudio.Play();

        bool finished = false;
        videoPlayer.loopPointReached += (vp) => finished = true;

        while (!finished)
        {
            if (allowSkip && (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
            {
                if (videoAudio) videoAudio.Stop();
                videoPlayer.Stop();
                break;
            }
            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
