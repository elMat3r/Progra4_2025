using System.Collections;
using TMPro;
using UnityEngine;

public class Timer_Manager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float currentTimer;
    int minutes, seconds;

    private void Start()
    {
        StartCoroutine(StartTimer());
    }
    private void Update()
    {
        if (currentTimer <= 0)
        {
            Level_Manager.Instance.OnPlayerDie();
        }
    }
    IEnumerator StartTimer()
    {
        while (true)
        {
            currentTimer -= Time.deltaTime;
            minutes = (int)(currentTimer / 60f);
            seconds = (int)(currentTimer - minutes * 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            yield return null;
        }
    }
}
