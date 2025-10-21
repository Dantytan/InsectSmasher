using UnityEngine;

public class PanelToggle : MonoBehaviour
{
    public GameObject panel; // asigna tu panel desde el inspector

    public void TogglePanel()
    {
        panel.SetActive(!panel.activeSelf);
    }
}

