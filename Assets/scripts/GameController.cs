using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool juegoPausado = false;

    [Header("🎶 Música de fondo")]
    public AudioClip musicaFondo;

    void Start()
    {
        // Reproducir música al iniciar el juego
        if (musicaFondo != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMusica(musicaFondo);
        }
    }

    // Método para pausar
    public void PausarJuego()
    {
        juegoPausado = true;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PauseMusica();
    }

    // Método para reanudar
    public void ReanudarJuego()
    {
        juegoPausado = false;

        if (AudioManager.Instance != null)
            AudioManager.Instance.ResumeMusica();
    }

    // Alternar con un solo botón (opcional)
    public void TogglePausa()
    {
        if (juegoPausado)
            ReanudarJuego();
        else
            PausarJuego();
    }

    public void SalirJuego()
    {
        Debug.Log("Cerrando el juego...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

