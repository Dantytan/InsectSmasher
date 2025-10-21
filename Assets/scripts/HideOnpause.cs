using UnityEngine;
using UnityEngine.UI;

public class OcultarEnPausa : MonoBehaviour
{
    private bool estadoActual;
    private Graphic[] graficos;
    private CanvasGroup canvasGroup;

    void Start()
    {
        // Intentar obtener un CanvasGroup (más eficiente si lo hay)
        canvasGroup = GetComponent<CanvasGroup>();

        // Si no hay CanvasGroup, obtener los gráficos (Image, Text, etc.)
        if (canvasGroup == null)
        {
            graficos = GetComponentsInChildren<Graphic>(true);
        }

        ActualizarVisibilidad();
    }

    void Update()
    {
        if (estadoActual != GameController.juegoPausado)
        {
            ActualizarVisibilidad();
        }
    }

    void ActualizarVisibilidad()
    {
        estadoActual = GameController.juegoPausado;
        bool visible = !estadoActual;

        if (canvasGroup != null)
        {
            // Si tiene CanvasGroup, controlamos la visibilidad y los clics
            canvasGroup.alpha = visible ? 1 : 0;
            canvasGroup.interactable = visible;
            canvasGroup.blocksRaycasts = visible;
        }
        else
        {
            // Si no tiene CanvasGroup, desactivamos los gráficos
            foreach (var g in graficos)
            {
                g.enabled = visible;
            }
        }
    }
}
