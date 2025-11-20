using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    void Start() => currentHealth = maxHealth;

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Jugador recibe daño. Vida: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Jugador muerto");
            // Aquí puedes desactivar movimiento o cargar Game Over
        }
    }
}
