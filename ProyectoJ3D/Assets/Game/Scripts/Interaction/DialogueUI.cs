using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject panel;              // Panel contenedor del diálogo
    public TextMeshProUGUI dialogueText;  // Texto del mensaje

    void Start()
    {
        Hide();
    }

    public void Hide()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    /// <summary>
    /// Muestra el mensaje adecuado según las monedas actuales y las requeridas.
    /// </summary>
    public void ShowCoinsMessage(int currentCoins, int requiredCoins, bool readyToChangeScene)
    {
        if (panel == null || dialogueText == null)
        {
            Debug.LogWarning("[DialogueUI] Faltan referencias de panel o texto.");
            return;
        }

        panel.SetActive(true);

        if (currentCoins < requiredCoins)
        {
            int faltan = requiredCoins - currentCoins;

            dialogueText.text =
                $"Necesitas <b>{requiredCoins}</b> monedas para pasar al siguiente nivel.\n" +
                $"Te faltan <b>{faltan}</b> monedas.";
        }
        else
        {
            // Si ya tiene suficientes monedas:
            // readyToChangeScene indica si ya vio el mensaje antes o no.
            if (!readyToChangeScene)
            {
                dialogueText.text =
                    "¡Felicidades! Has conseguido las monedas necesarias para pasar al siguiente nivel.\n" +
                    "Presiona <b>E</b> nuevamente para continuar.";
            }
            else
            {
                // Puedes poner un mensaje distinto si quieres,
                // aunque normalmente en este punto ya vas a cambiar de escena.
                dialogueText.text =
                    "Cuando estés listo, presiona <b>E</b> para ir al siguiente nivel.";
            }
        }
    }
}
