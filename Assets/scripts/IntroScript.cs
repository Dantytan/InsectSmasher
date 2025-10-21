using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class VideoLoading : MonoBehaviour
{
    public string loadScene = "SampleScene";
    public VideoPlayer video;

    void Start()
    {
        StartCoroutine(CargarConVideo());
    }

    IEnumerator CargarConVideo()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(loadScene);
        async.allowSceneActivation = false;

        video.Play();

        // Espera a que el video termine
        video.loopPointReached += (v) =>
        {
            async.allowSceneActivation = true;
        };

        // Mientras carga, puedes hacer algo como mostrar barra o texto si quieres
        while (!async.isDone)
            yield return null;
    }
}

