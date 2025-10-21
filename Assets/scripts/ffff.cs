using UnityEngine;
using UnityEngine.SceneManagement;

public class ffff : MonoBehaviour
{
    [Header("🎵 Clip de sonido para el botón")]
    public AudioClip sonidoClick;

    // 👉 Opción 1: Cargar la escena inmediatamente (con pantalla de carga)
    public void loadScene(string sceneName)
    {
        LoadingScreen.CargarEscena(sceneName);
        Contador.objetosDestruidos = 0;
    }

    // 👉 Opción 2: Solo reproducir sonido
    public void playSound()
    {
        if (AudioManager.Instance != null && sonidoClick != null)
        {
            AudioManager.Instance.PlayEfecto(sonidoClick);
        }
    }

    // 👉 Opción 3: Reproducir sonido y luego cargar la escena con pantalla de carga
    public void loadSceneWithSound(string sceneName)
    {
        if (AudioManager.Instance != null && sonidoClick != null)
        {
            AudioManager.Instance.PlayEfecto(sonidoClick);
            StartCoroutine(CargarDespuesDeSonido(sceneName, sonidoClick.length));
        }
        else
        {
            // Si no hay sonido, carga normal
            loadScene(sceneName);
        }
    }

    private System.Collections.IEnumerator CargarDespuesDeSonido(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadingScreen.CargarEscena(sceneName);
        Contador.objetosDestruidos = 0;
    }
}


