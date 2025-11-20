using UnityEngine;
using UnityEngine.InputSystem;

public class ChestTrigger : MonoBehaviour
{
    public Chest chest;           // referencia al script Chest
    public GameObject uiMessage;  // "Presiona E para abrir"

    private bool playerInside = false;

    void Start()
    {
        if (uiMessage != null)
            uiMessage.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            if (uiMessage != null)
                uiMessage.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;

            if (uiMessage != null)
                uiMessage.SetActive(false);
        }
    }

    void Update()
    {
        if (!playerInside) return;

        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            // Abrir/cerrar cofre
            if (chest != null)
                chest.Toggle();

            // Si quieres ocultar el mensaje después de abrir:
            // uiMessage.SetActive(false);
        }
    }
}
