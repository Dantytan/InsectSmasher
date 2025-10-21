using UnityEngine;
using UnityEngine.UI;

public class VolumenGeneralSlider : MonoBehaviour
{
    private Slider slider;
    private float volumenMusicaMax;
    private float volumenEfectosMax;

    private const string PREF_KEY = "VolumenGeneral";

    void Start()
    {
        slider = GetComponent<Slider>();

        // Guardamos los máximos definidos en el AudioManager
        volumenMusicaMax = AudioManager.Instance.GetVolumenMusica();
        volumenEfectosMax = AudioManager.Instance.GetVolumenEfectos();

        // Recuperar valor guardado o usar 1 si no existe
        float valorGuardado = PlayerPrefs.GetFloat(PREF_KEY, 1f);
        slider.value = valorGuardado;

        // Aplicar el volumen inicial
        AplicarVolumen(valorGuardado);

        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnSliderValueChanged(float value)
    {
        AplicarVolumen(value);

        // Guardar para que se mantenga entre escenas y al cerrar
        PlayerPrefs.SetFloat(PREF_KEY, value);
        PlayerPrefs.Save();
    }

    private void AplicarVolumen(float value)
    {
        if (AudioManager.Instance == null) return;

        float volumenMusica = value * volumenMusicaMax;
        float volumenEfectos = value * volumenEfectosMax;

        AudioManager.Instance.SetVolumenMusica(volumenMusica);
        AudioManager.Instance.SetVolumenEfectos(volumenEfectos);
    }
}


