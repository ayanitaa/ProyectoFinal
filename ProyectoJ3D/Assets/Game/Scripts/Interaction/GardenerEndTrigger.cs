using UnityEngine;
using UnityEngine.InputSystem;

public class GardenerEndTrigger : MonoBehaviour
{
    [Header("UI")]
    public GameObject uiMessage;   // Mensaje "Presiona ESC para terminar"

    private bool playerInside = false;
    private bool gameEnded = false;

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
        if (!playerInside || gameEnded)
            return;

        if (Keyboard.current == null)
            return;

        // 👉 TECLA DEFINITIVA DE FINAL: ESC
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            gameEnded = true;

            // Ocultar mensaje
            if (uiMessage != null)
                uiMessage.SetActive(false);

            // Congelar movimiento del player (opcional)
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                var move = player.GetComponent<PlayerMovement>();
                if (move != null) move.enabled = false;
            }

            // Mostrar panel final
            PanelResultados.ShowFinalScreen();

            // Opcional: pausar el tiempo
            // Time.timeScale = 0f;
        }
    }
}
