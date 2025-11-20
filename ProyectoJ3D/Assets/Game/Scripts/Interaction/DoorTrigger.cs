using UnityEngine;
using UnityEngine.InputSystem;   // Nuevo Input System

public class DoorTrigger : MonoBehaviour
{
    public Door door;      // Referencia a la puerta
    bool playerInside = false;

    void Reset()
    {
        if (!door)
            door = GetComponentInParent<Door>();

        var col = GetComponent<Collider>();
        if (col) col.isTrigger = true;  // Aseguramos que sea trigger
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            InteractionUI.Instance?.ShowMessage();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            InteractionUI.Instance?.HideMessage();
        }
    }

    void Update()
    {
        if (!playerInside || door == null) return;

        // Nuevo Input System: tecla E
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            door.ToggleDoor();
            // si quieres que el mensaje siga visible, deja esto comentado:
            // InteractionUI.Instance?.HideMessage();
        }
    }
}
