using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public TextMeshProUGUI timerText;
    private float currentTime;
    private bool isRunning;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (isRunning)
        {
            currentTime += Time.deltaTime;
            UpdateTimerText();
        }
    }

    public void Initialize(TextMeshProUGUI textComponent)
    {
        timerText = textComponent;
        currentTime = 0;
        isRunning = true;
        UpdateTimerText();
    }

    public void StartTimer()
    {
        currentTime = 0;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        currentTime = 0;
        isRunning = true;
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            int centiseconds = Mathf.FloorToInt((currentTime % 1) * 100);
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, centiseconds);
        }
    }
}
