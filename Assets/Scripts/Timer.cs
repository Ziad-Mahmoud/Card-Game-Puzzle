using System;
using TMPro;
using UnityEngine;
public class Timer : MonoBehaviour
{
    public TimeSpan timeAllowed;
    DateTime startTime;
    TextMeshProUGUI timerText;
    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        timeAllowed = TimeSpan.FromSeconds(7 + LevelManager.level * 3) ;
        startTime = DateTime.Now;
    }
    void Update()
    {
        var timePassed = DateTime.Now - startTime;
        timerText.text = (timeAllowed - timePassed).ToString("ss");
        if (timePassed > timeAllowed)
        {
            LevelManager.lose.Invoke();
            gameObject.SetActive(false);
        }
    }
}