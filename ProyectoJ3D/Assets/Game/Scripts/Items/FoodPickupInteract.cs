using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class FoodPickupInteract : MonoBehaviour
{
    [Header("UI")]
    public GameObject uiMessage; // "Presiona E para comer"

    [Header("Feedback")]
    public GameObject eatParticlesPrefab; // prefab de partículas
    public AudioClip eatSound;            // clip de sonido
    public float soundVolume = 1f;

    private bool playerInside = false;
    private PlayerStats playerStats;

    void Reset()
    {
        var col = GetComponent<Collider>();
        if (col) col.isTrigger = true;
    }

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
            playerStats = other.GetComponent<PlayerStats>();

            if (uiMessage != null)
                uiMessage.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            playerStats = null;

            if (uiMessage != null)
                uiMessage.SetActive(false);
        }
    }

    void Update()
    {
        if (!playerInside || playerStats == null)
            return;

        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            // 👉 1. Lógica de juego: sumar "comida" (candy)
            playerStats.AddCandy(); // cada 2 = +1 rayo

            // 👉 2. Partículas
            if (eatParticlesPrefab != null)
            {
                GameObject p = Instantiate(
                    eatParticlesPrefab,
                    transform.position + Vector3.up * 0.5f,
                    Quaternion.identity
                );
                Destroy(p, 2f); // limpiar partículas después
            }

            // 👉 3. Sonido
            if (eatSound != null)
            {
                AudioSource.PlayClipAtPoint(
                    eatSound,
                    transform.position,
                    soundVolume
                );
            }

            // 👉 4. Quitar comida y mensaje
            if (uiMessage != null)
                uiMessage.SetActive(false);

            Destroy(gameObject);
        }
    }
}
