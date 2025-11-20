using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("Corazones")]
    public Image[] heartIcons;   // arrastra aquí los 5 corazones

    [Header("Rayos (energía)")]
    public Image[] energyIcons;  // arrastra aquí los 5 rayos

    // --- VIDA ---
    public void UpdateHearts(int activeHearts)
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            // activa solo los corazones que sigan “vivos”
            heartIcons[i].enabled = i < activeHearts;
        }
    }

    // --- ENERGÍA ---
    public void UpdateEnergy(int currentSegments, int maxSegments)
    {
        // OJO: aquí NO limitamos por maxSegments, usamos todos los que existan
        for (int i = 0; i < energyIcons.Length; i++)
        {
            energyIcons[i].enabled = i < currentSegments;
        }
    }
}
