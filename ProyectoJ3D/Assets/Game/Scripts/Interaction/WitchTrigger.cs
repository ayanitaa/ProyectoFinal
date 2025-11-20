using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WitchTrigger : MonoBehaviour
{
    public DialogueUI dialogueUI;    // referencia al script del panel de diálogo
    public string nextSceneName = "Scene2";
    public int requiredCoins = 10;

    private bool playerInside = false;
    private bool readyToChangeScene = false;

    void Start()
    {
        if (dialogueUI != null)
            dialogueUI.Hide();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            readyToChangeScene = false; // por si vuelve a entrar más adelante
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            readyToChangeScene = false;

            if (dialogueUI != null)
                dialogueUI.Hide();
        }
    }

    void Update()
    {
        if (!playerInside) return;
        if (Keyboard.current == null) return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            int coins = GameManager.Instance != null ? GameManager.Instance.ItemsCount : 0;

            if (coins < requiredCoins)
            {
                // Solo mostrar mensaje de que faltan monedas
                if (dialogueUI != null)
                    dialogueUI.ShowCoinsMessage(coins, requiredCoins, false);

                readyToChangeScene = false;
                return;
            }

            // Aquí ya tiene suficientes monedas
            if (!readyToChangeScene)
            {
                // Primera vez que llega con >= requiredCoins
                if (dialogueUI != null)
                    dialogueUI.ShowCoinsMessage(coins, requiredCoins, false);

                readyToChangeScene = true;
            }
            else
            {
                // Segunda vez que presiona E con suficientes monedas -> cambiar de escena

                // 1) Guardar estado del jugador en el GameManager
                if (GameManager.Instance != null)
                {
                    PlayerStats ps = FindObjectOfType<PlayerStats>();
                    if (ps != null)
                    {
                        GameManager.Instance.SavePlayerState(ps);
                    }
                }

                // 2) Ocultar diálogo
                if (dialogueUI != null)
                    dialogueUI.Hide();

                // 3) Cargar la siguiente escena
                SceneManager.LoadScene(nextSceneName);
            }

        }
    }

}
