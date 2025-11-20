using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CandyPickup : MonoBehaviour
{
    void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerStats stats = other.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.AddCandy(); // suma 1 dulce; cada 2 dulces = +1 rayo
        }

        Destroy(gameObject);
    }
}
