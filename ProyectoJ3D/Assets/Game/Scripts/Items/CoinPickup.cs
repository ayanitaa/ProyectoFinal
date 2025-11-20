using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CoinPickup : MonoBehaviour
{
    public AudioClip sfx;
    [Range(0, 1)] public float sfxVol = 0.8f;

    public GameObject pickupParticlesPrefab;

    void Reset() { GetComponent<Collider>().isTrigger = true; }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        GameManager.Instance.AddCoin(1);       

        if (sfx) AudioSource.PlayClipAtPoint(sfx, transform.position, sfxVol);

        Destroy(gameObject);
    }
}
