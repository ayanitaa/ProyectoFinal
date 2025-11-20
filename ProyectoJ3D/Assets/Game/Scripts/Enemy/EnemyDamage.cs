using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 10;
    public float hitCooldown = 0.4f;  // tiempo mínimo entre golpes

    public Animator animator;         // referencia al Animator del enemigo

    float lastHitTime = -999f;

    void Reset()
    {
        var col = GetComponent<Collider>();
        if (col) col.isTrigger = true;
    }

    void OnTriggerStay(Collider other)
    {
        // Solo golpea al Player
        if (!other.CompareTag("Player")) return;

        // Solo hacer daño si está en animación de ataque (sin usar Animation window)
        if (animator != null && !animator.GetBool("IsAttacking"))
            return;

        // Control de cooldown
        if (Time.time < lastHitTime + hitCooldown)
            return;

        lastHitTime = Time.time;

        PlayerStats stats = other.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.TakeDamage(damage);
            Debug.Log("Enemy hit player for " + damage);
        }
    }
}
