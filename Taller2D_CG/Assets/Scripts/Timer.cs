using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Referencias UI - Timer")]
    public TextMeshProUGUI timerMinutes;
    public TextMeshProUGUI timerSeconds;
    public TextMeshProUGUI timerSeconds100;

    private float startTime;
    private float stopTime;
    private float timerTime;
    private bool isRunning = false;

    void Start()
    {
        TimerStart();
    }

    public void TimerStart()
    {
        if (!isRunning)
        {
            Debug.Log("Timer START");
            isRunning = true;
            startTime = Time.time;
        }
    }

    public void TimerStop()
    {
        if (isRunning)
        {
            Debug.Log("Timer STOP");
            isRunning = false;
            stopTime = timerTime;

            
            GameManager.Instance.GuardarTiempoEscena(stopTime);

            Debug.Log("Tiempo de esta escena: " + stopTime);
            Debug.Log("Tiempo total acumulado: " + GameManager.Instance.ObtenerTiempoTotal());
        }
    }

    public void TimerReset()
    {
        stopTime = 0;
        isRunning = false;
        if (timerMinutes != null) timerMinutes.text = "00";
        if (timerSeconds != null) timerSeconds.text = "00";
        if (timerSeconds100 != null) timerSeconds100.text = "00";
    }

    void Update()
    {
        if (isRunning)
        {
            timerTime = stopTime + (Time.time - startTime);

            int minutesInt = (int)timerTime / 60;
            int secondsInt = (int)timerTime % 60;
            int seconds100Int = (int)(Mathf.Floor((timerTime - (secondsInt + minutesInt * 60)) * 100));

            if (timerMinutes != null) timerMinutes.text = (minutesInt < 10) ? "0" + minutesInt : minutesInt.ToString();
            if (timerSeconds != null) timerSeconds.text = (secondsInt < 10) ? "0" + secondsInt : secondsInt.ToString();
            if (timerSeconds100 != null) timerSeconds100.text = (seconds100Int < 10) ? "0" + seconds100Int : seconds100Int.ToString();
        }
    }
}
