using UnityEngine;

public class PlayerAttackHitbox : MonoBehaviour
{
    public int damage = 5;                 // daño del player
    public PlayerAttack playerAttack;      // referencia al script del player

    private bool hasHitThisAttack = false;

    void Reset()
    {
        var col = GetComponent<Collider>();
        if (col) col.isTrigger = true;
    }

    void Update()
    {
        // Cuando termina la animación de ataque,
        // reseteamos para el siguiente golpe
        if (!playerAttack.IsAttacking && hasHitThisAttack)
        {
            hasHitThisAttack = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!playerAttack.IsAttacking) return;   // sólo cuando está atacando
        if (hasHitThisAttack) return;            // ya golpeó en este ataque

        if (!other.CompareTag("Enemy")) return;

        EnemyStats enemy = other.GetComponent<EnemyStats>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            hasHitThisAttack = true;             // este ataque ya hizo daño
            Debug.Log("Player pegó a " + other.name + " por " + damage);
        }
    }
}
