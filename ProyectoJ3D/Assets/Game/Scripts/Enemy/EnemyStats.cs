using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Vida")]
    public int maxHealth = 20;
    private int currentHealth;

    [Header("Referencias")]
    public Animator animator;        // arrastra el Animator del enemigo
    public GameObject coinPrefab;    // prefab de la moneda
    public Transform dropPoint;      // punto desde donde salen las monedas

    


    [Header("Opciones")]
    public float deathDelay = 2f;

    private bool isDead = false;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;


        if (currentHealth <= 0)
            Die();
    }


    void Die()
    {
        isDead = true;

        animator.SetTrigger("Die");

        GameManager.Instance.AddKill();

        DropCoins(3);

        GetComponent<EnemyAI>().enabled = false;
        GetComponent<Collider>().enabled = false;

        Destroy(gameObject, deathDelay);
    }


    void DropCoins(int amount)
    {
        if (coinPrefab == null || dropPoint == null)
        {
            Debug.LogWarning("Falta coinPrefab o dropPoint en EnemyStats");
            return;
        }

        // Radio del círculo donde aparecerán las monedas
        float radius = 0.7f;

        for (int i = 0; i < amount; i++)
        {
            // Ángulo para separarlas (120° si son 3)
            float angle = i * Mathf.PI * 2f / amount;

            Vector3 offset = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * radius;

            // Posición de cada moneda, alrededor del enemigo
            Vector3 pos = dropPoint.position + offset + Vector3.up * 0.2f;

            GameObject coin = Instantiate(coinPrefab, pos, Quaternion.identity);

            // Fuerza tipo Mario: hacia arriba y un poco hacia afuera
            Rigidbody rb = coin.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 launchDir = (offset.normalized + Vector3.up).normalized;
                rb.AddForce(launchDir * 4f, ForceMode.Impulse);
                rb.AddTorque(Random.insideUnitSphere * 5f, ForceMode.Impulse);
            }
        }
    }

}
