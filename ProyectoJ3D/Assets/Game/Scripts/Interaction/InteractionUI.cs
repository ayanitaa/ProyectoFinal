using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    public static InteractionUI Instance;

    [Header("UI")]
    public GameObject interactMessage;  // Texto o panel con "Presiona E para abrir"

    void Awake()
    {
        Instance = this;

        if (interactMessage != null)
            interactMessage.SetActive(false);  // Oculto al inicio
    }

    public void ShowMessage()
    {
        if (interactMessage != null)
            interactMessage.SetActive(true);
    }

    public void HideMessage()
    {
        if (interactMessage != null) interactMessage.SetActive(false);
            interactMessage.SetActive(false);
    }
}
