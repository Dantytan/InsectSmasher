using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource musicaSource;
    private AudioSource efectosSource;

    [Range(0f, 1f)] public float volumenMusica = 1f;
    [Range(0f, 1f)] public float volumenEfectos = 1f;

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Configuramos dos AudioSources (uno para música, otro para efectos)
            musicaSource = gameObject.AddComponent<AudioSource>();
            musicaSource.loop = true;
            musicaSource.playOnAwake = false;

            efectosSource = gameObject.AddComponent<AudioSource>();
            efectosSource.loop = false;
            efectosSource.playOnAwake = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 🔊 Reproducir música de fondo
    public void PlayMusica(AudioClip clip)
    {
        if (clip == null) return;

        musicaSource.clip = clip;
        musicaSource.volume = volumenMusica;
        musicaSource.Play();
    }

    // ⏸ Pausar música
    public void PauseMusica()
    {
        if (musicaSource.isPlaying)
            musicaSource.Pause();
    }

    // ▶ Reanudar música
    public void ResumeMusica()
    {
        if (!musicaSource.isPlaying)
            musicaSource.UnPause();
    }

    // 🔊 Reproducir efecto de sonido
    public void PlayEfecto(AudioClip clip)
    {
        if (clip == null) return;
        efectosSource.PlayOneShot(clip, volumenEfectos);
    }

    // Ajustar volúmenes globalmente
    public void SetVolumenMusica(float volumen)
    {
        volumenMusica = volumen;
        musicaSource.volume = volumenMusica;
    }

    public void SetVolumenEfectos(float volumen)
    {
        volumenEfectos = volumen;
    }

    // 👉 Obtener volúmenes actuales (para inicializar sliders)
    public float GetVolumenMusica()
    {
        return volumenMusica;
    }

    public float GetVolumenEfectos()
    {
        return volumenEfectos;
    }
}

