using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // --- TIEMPO ---
    private float scene1Time = 0f;
    private float scene2Time = 0f;

    // --- PUNTAJE / MONEDAS ---
    private int score = 0;
    private int itemsCount = 0;

    // --- ESTADO PERSISTENTE DEL PLAYER ---
    [Header("Estado persistente del jugador")]
    public int savedMaxHealth = 100;
    public int savedCurrentHealth = 100;
    public int savedMaxEnergySegments = 5;
    public int savedCurrentEnergySegments = 5;
    public bool hasSavedPlayerState = false;

    [Header("Estadísticas del juego")]
    public int totalEnemiesKilled = 0;
    public int totalDeaths = 0;

    // Tiempo total ya lo tienes, pero agregamos aquí uno más claro
    public float totalPlayTime = 0f;


    // Eventos para UI u otros sistemas
    public event Action<int> OnCoinsChanged;
    public event Action<int> OnScoreChanged;

    // --- PROPIEDADES ---
    public float TotalTime => totalPlayTime;
    public float Scene1Time => scene1Time;
    public float Scene2Time => scene2Time;

    public int Score { get => score; private set => score = value; }
    public int ItemsCount { get => itemsCount; private set => itemsCount = value; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);   // persiste entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        AddPlayTime(Time.deltaTime);
    }


    // --- TIEMPO ---
    public void AddSceneTime(int sceneIndex, float deltaTime)
    {
        totalPlayTime += deltaTime;

        switch (sceneIndex)
        {
            case 1:
                scene1Time += deltaTime;
                break;
            case 2:
                scene2Time += deltaTime;
                break;
        }
    }
    public void AddKill()
    {
        totalEnemiesKilled++;
    }

    public void AddDeath()
    {
        totalDeaths++;
    }

    public void AddPlayTime(float delta)
    {
        totalPlayTime += delta;
    }


    // --- SCORE / MONEDAS ---
    public void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(score);
    }

    public void AddCoin(int amount = 1)
    {
        itemsCount += amount;
        OnCoinsChanged?.Invoke(itemsCount);
    }

    // --- ESTADO DEL PLAYER: GUARDAR Y APLICAR ---
    public void SavePlayerState(PlayerStats stats)
    {
        savedMaxHealth = stats.maxHealth;
        savedCurrentHealth = stats.currentHealth;

        savedMaxEnergySegments = stats.maxEnergySegments;
        savedCurrentEnergySegments = stats.currentEnergySegments;

        hasSavedPlayerState = true;

        Debug.Log($"[GameManager] Estado guardado: Vida {savedCurrentHealth}/{savedMaxHealth}, Energía {savedCurrentEnergySegments}/{savedMaxEnergySegments}");
    }

    public void ApplySavedPlayerState(PlayerStats stats)
    {
        if (!hasSavedPlayerState) return;

        stats.maxHealth = savedMaxHealth;
        stats.currentHealth = savedCurrentHealth;

        stats.maxEnergySegments = savedMaxEnergySegments;
        stats.currentEnergySegments = savedCurrentEnergySegments;

        Debug.Log($"[GameManager] Estado aplicado: Vida {stats.currentHealth}/{stats.maxHealth}, Energía {stats.currentEnergySegments}/{stats.maxEnergySegments}");
    }

    // --- RESET TOTAL (si quieres nueva partida) ---
    public void ResetGame()
    {
        totalPlayTime = 0f;
        scene1Time = 0f;
        scene2Time = 0f;

        score = 0;
        itemsCount = 0;

        hasSavedPlayerState = false;

        OnScoreChanged?.Invoke(score);
        OnCoinsChanged?.Invoke(itemsCount);
    }
}
