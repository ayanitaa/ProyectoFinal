using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Referencias")]
    public Transform lid;            // tapa del cofre
    public Transform dropPoint;      // punto desde donde sale la moneda
    public GameObject coinPrefab;    // prefab de la moneda

    [Header("Apertura")]
    public float openAngle = -180f;  // ángulo en Y para abrir la tapa
    public float openSpeed = 5f;     // velocidad de apertura

    [Header("Moneda")]
    public float coinForce = 4f;     // fuerza del "salto" de la moneda

    private bool isOpen = false;
    private bool hasDroppedCoin = false;

    private Quaternion closedRot;
    private Quaternion openRot;

    void Awake()
    {
        if (lid == null)
        {
            Debug.LogWarning("Chest: no se asignó Lid en " + name);
            return;
        }

        // Rotación cerrada actual
        closedRot = lid.localRotation;

        // Rotación abierta: rotamos en Xa
        openRot = Quaternion.Euler(openAngle,0f, 0f) * closedRot;
    }

    void Update()
    {
        if (lid == null) return;

        Quaternion targetRot = isOpen ? openRot : closedRot;

        lid.localRotation = Quaternion.Slerp(
            lid.localRotation,
            targetRot,
            Time.deltaTime * openSpeed
        );
    }

    public void Toggle()
    {
        bool wasClosed = !isOpen;
        isOpen = !isOpen;

        // Si estaba cerrado y ahora se abre, soltamos la moneda (una sola vez)
        if (wasClosed && isOpen && !hasDroppedCoin)
        {
            DropCoin();
            hasDroppedCoin = true;
        }
    }

    void DropCoin()
    {
        if (coinPrefab == null || dropPoint == null)
        {
            Debug.LogWarning("Chest: falta coinPrefab o dropPoint en " + name);
            return;
        }

        GameObject coin = Instantiate(coinPrefab, dropPoint.position, Quaternion.identity);

        // Si la moneda tiene Rigidbody, le damos un salto estilo "pop"
        Rigidbody rb = coin.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Hacia arriba + un poco hacia adelante desde el cofre
            Vector3 launchDir = (transform.forward + Vector3.up).normalized;
            rb.AddForce(launchDir * coinForce, ForceMode.Impulse);
        }
    }
}
