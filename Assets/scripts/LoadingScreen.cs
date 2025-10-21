using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    [Header("UI Opcional")]
    public Slider barraProgreso;
    public Text textoProgreso;

    private static string escenaDestino;

    // Llamar a este método antes de cargar
    public static void CargarEscena(string nombreEscena)
    {
        escenaDestino = nombreEscena;
        SceneManager.LoadScene("Transicion"); // 👈 tu escena de transición
    }

    void Start()
    {
        StartCoroutine(CargarAsincrono());
    }

    IEnumerator CargarAsincrono()
    {
        AsyncOperation operacion = SceneManager.LoadSceneAsync(escenaDestino);
        operacion.allowSceneActivation = false;

        float tiempoTransicion = 3f; // ⏳ duración fija de la transición
        float tiempo = 0f;

        while (!operacion.isDone)
        {
            // Contador del tiempo transcurrido
            tiempo += Time.deltaTime;
            float progresoSimulado = Mathf.Clamp01(tiempo / tiempoTransicion);

            // Mostrar el progreso en el slider
            if (barraProgreso != null)
                barraProgreso.value = progresoSimulado;

            if (textoProgreso != null)
                textoProgreso.text = (progresoSimulado * 100f).ToString("F0") + "%";

            // Cuando pasó el tiempo Y la escena ya está lista
            if (tiempo >= tiempoTransicion && operacion.progress >= 0.9f)
            {
                operacion.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
