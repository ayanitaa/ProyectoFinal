using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PanelResultados : MonoBehaviour
{
    public static PanelResultados Instance;

    [Header("UI")]
    public GameObject panelRoot;
    public TextMeshProUGUI textEnemies;
    public TextMeshProUGUI textTime;
    public TextMeshProUGUI textCoins;
    public TextMeshProUGUI textDeaths;

    void Awake()
    {
        Instance = this;
    }

    public static void ShowFinalScreen()
    {
        if (Instance != null)
            Instance.Show();
    }

    public void Show()
    {
        var gm = GameManager.Instance;

        panelRoot.SetActive(true);

        textEnemies.text = $"Enemigos eliminados: {gm.totalEnemiesKilled}";
        textCoins.text = $"Monedas recogidas: {gm.ItemsCount}";
        textDeaths.text = $"Veces que moriste: {gm.totalDeaths}";

        // Tiempo total jugado
        int minutes = Mathf.FloorToInt(gm.totalPlayTime / 60);
        int seconds = Mathf.FloorToInt(gm.totalPlayTime % 60);

        textTime.text = $"Tiempo total: {minutes:00}:{seconds:00}";
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
