using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GameObject popupPanel; // asigna el panel desde el inspector

    // Mostrar popup
    public void ShowPopup()
    {
        popupPanel.SetActive(true);
    }

    // Ocultar popup
    public void HidePopup()
    {
        popupPanel.SetActive(false);
    }
}
