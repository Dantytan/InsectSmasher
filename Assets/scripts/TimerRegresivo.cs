using UnityEngine;
using TMPro; // Solo si vas a mostrar el tiempo con TextMeshPro
using UnityEngine.SceneManagement;

public class TimerRegresivo : MonoBehaviour
{
    [Header("Tiempo inicial en segundos")]
    public float tiempoInicial = 60f;

    [Header("Texto para mostrar el tiempo (opcional)")]
    public TextMeshProUGUI textoTimer;

    private float tiempoRestante;
    private bool tiempoFinalizado = false;

    void Start()
    {
        tiempoRestante = tiempoInicial;
    }

    void Update()
    {
        if (!tiempoFinalizado && !GameController.juegoPausado)
        {
            tiempoRestante -= Time.deltaTime;
            tiempoRestante = Mathf.Clamp(tiempoRestante, 0f, tiempoInicial);

            if (textoTimer != null)
            {
                int minutos = Mathf.FloorToInt(tiempoRestante / 60f);
                int segundos = Mathf.FloorToInt(tiempoRestante % 60f);
                textoTimer.text = string.Format("{0:00}:{1:00}", minutos, segundos);
            }

            if (tiempoRestante <= 0f)
            {
                tiempoFinalizado = true;
                AlTerminarElTiempo();
            }
        }
    }

    void AlTerminarElTiempo()
    {
        // Aquí haces lo que necesites cuando el tiempo se acabe
        // Por ejemplo: activar algo, mostrar un mensaje, cambiar de escena, etc.
        LoadingScreen.CargarEscena("SampleScene");
        Contador.objetosDestruidos = 0;
    }
}
