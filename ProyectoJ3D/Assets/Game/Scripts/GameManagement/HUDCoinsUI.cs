using UnityEngine;
using TMPro;
using System.Collections;

public class HUDCoinsUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject coinsPanel;   // el panel (empieza desactivado en el Inspector)
    public TMP_Text coinsText;      // asigna el TMP_Text

    public int maxCoins = 10;

    void Awake()
    {
        if (coinsPanel) coinsPanel.SetActive(false);
        SetText(0);
    }

    void OnEnable()
    {
        StartCoroutine(SubscribeWhenReady());
    }

    void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnCoinsChanged -= HandleCoinsChanged;
    }

    IEnumerator SubscribeWhenReady()
    {
        // Espera hasta que GameManager.Instance exista (por si despierta 1º el Canvas)
        while (GameManager.Instance == null) yield return null;

        // Evita doble suscripción si el objeto se re-habilita
        GameManager.Instance.OnCoinsChanged -= HandleCoinsChanged;
        GameManager.Instance.OnCoinsChanged += HandleCoinsChanged;

        // Refresca estado actual si ya había monedas antes de que se montara el HUD
        int current = GameManager.Instance.ItemsCount;
        if (current > 0 && coinsPanel && !coinsPanel.activeSelf) coinsPanel.SetActive(true);
        SetText(current);
    }

    void HandleCoinsChanged(int total)
    {
        if (coinsPanel && !coinsPanel.activeSelf) coinsPanel.SetActive(true);
        SetText(total);
    }

    void SetText(int v)
    {
        if (coinsText)
            coinsText.text = $"Monedas: {v}/{maxCoins}";
    }
}
