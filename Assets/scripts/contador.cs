using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Contador : MonoBehaviour
{
    public static int objetosDestruidos = 0;

    public TextMeshProUGUI textoContador; // Asigna en el inspector el Text del Canvas

    void Update()
    {
        textoContador.text = " " + objetosDestruidos;
    }
}
