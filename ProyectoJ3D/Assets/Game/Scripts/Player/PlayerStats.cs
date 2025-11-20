using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Vida")]
    public int maxHealth = 100;
    public int currentHealth;
    public int heartsCount = 5;
    public int damagePerHit = 10;

    [Header("Energía (rayos)")]
    public int maxEnergySegments = 5;
    public int currentEnergySegments;
    public float energyDrainInterval = 60f;

    [Header("Dulces de maíz")]
    public int candiesPerEnergy = 2;
    private int candyCounter = 0;

    float energyTimer = 0f;

    public PlayerHUD hud;

    void Awake()
    {
        // 1) Si hay estado guardado en GameManager, lo aplicamos
        if (GameManager.Instance != null && GameManager.Instance.hasSavedPlayerState)
        {
            GameManager.Instance.ApplySavedPlayerState(this);
        }
        else
        {
            // 2) Si no hay estado guardado (primera escena / nueva partida), empezamos full
            currentHealth = maxHealth;
            currentEnergySegments = maxEnergySegments;
        }

        // 3) Actualizar HUD con los valores actuales
        if (hud != null)
        {
            hud.UpdateHearts(GetActiveHearts());
            hud.UpdateEnergy(currentEnergySegments, maxEnergySegments);
        }
    }

    void Update()
    {
        // TIMER de energía (como ya lo tenías)
        energyTimer += Time.deltaTime;
        if (energyTimer >= energyDrainInterval)
        {
            energyTimer = 0f;
            LoseEnergySegment();
        }
    }

    // --- VIDA ---

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (hud != null)
            hud.UpdateHearts(GetActiveHearts());

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        // Registrar muerte global
        GameManager.Instance.AddDeath();

        // Animación de muerte
        Animator anim = GetComponent<Animator>();
        if (anim != null)
            anim.SetTrigger("Die");

        // Desactivar movimiento del player
        GetComponent<PlayerMovement>().enabled = false;

        // Mostrar panel final después de unos segundos
        PanelResultados.ShowFinalScreen();
    }


    int GetActiveHearts()
    {
        float lifePerHeart = (float)maxHealth / heartsCount;
        return Mathf.CeilToInt(currentHealth / lifePerHeart);
    }

    // --- ENERGÍA ---

    void LoseEnergySegment()
    {
        if (currentEnergySegments <= 0) return;

        currentEnergySegments--;
        if (hud != null)
            hud.UpdateEnergy(currentEnergySegments, maxEnergySegments);

        Debug.Log("Energía reducida, segmentos actuales: " + currentEnergySegments);
    }

    public void AddCandy()
    {
        candyCounter++;

        if (candyCounter >= candiesPerEnergy)
        {
            candyCounter -= candiesPerEnergy;
            RegainEnergySegment();
        }
    }

    void RegainEnergySegment()
    {
        if (currentEnergySegments >= maxEnergySegments) return;

        currentEnergySegments++;
        if (hud != null)
            hud.UpdateEnergy(currentEnergySegments, maxEnergySegments);

        Debug.Log("Energía regenerada, segmentos actuales: " + currentEnergySegments);
    }
}
