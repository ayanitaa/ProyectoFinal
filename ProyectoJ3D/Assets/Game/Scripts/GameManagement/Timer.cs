using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerMinutes;
    public TextMeshProUGUI timerSeconds;
    public TextMeshProUGUI timerSeconds100;

    private float timerTime = 0f;
    private bool isRunning = true; // arranca corriendo automáticamente

    void Start()
    {
        // Opcional: reset visual
        timerMinutes.text = "00";
        timerSeconds.text = "00";
        timerSeconds100.text = "00";
    }

    void Update()
    {
        if (!isRunning) return;

        timerTime += Time.deltaTime;

        // Actualizar GameManager
        if (GameManager.Instance != null)
        {
            // Cambia el número dependiendo de qué escena es esta
            GameManager.Instance.AddSceneTime(1, Time.deltaTime);
        }

        // Actualizar UI
        int minutesInt = (int)(timerTime / 60);
        int secondsInt = (int)(timerTime % 60);
        int seconds100Int = (int)((timerTime - (minutesInt * 60 + secondsInt)) * 100);

        timerMinutes.text = (minutesInt < 10) ? "0" + minutesInt : minutesInt.ToString();
        timerSeconds.text = (secondsInt < 10) ? "0" + secondsInt : secondsInt.ToString();
        timerSeconds100.text = (seconds100Int < 10) ? "0" + seconds100Int : seconds100Int.ToString();
    }


    // Puedes usarlos desde botones o triggers
    public void TimerStop()
    {
        isRunning = false;
    }

    public void TimerResume()
    {
        isRunning = true;
    }

    public void TimerReset()
    {
        timerTime = 0f;
        timerMinutes.text = "00";
        timerSeconds.text = "00";
        timerSeconds100.text = "00";
    }

    public float GetTime()
    {
        return timerTime;
    }
}
