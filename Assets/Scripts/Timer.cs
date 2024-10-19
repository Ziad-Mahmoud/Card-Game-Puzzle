using System;
using TMPro;
using UnityEngine;
public class Timer : MonoBehaviour
{
    public TimeSpan timeAllowed;
    public DateTime startTime;
    TextMeshProUGUI timerText;
    public static bool gameEnd = false;
    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        gameEnd = false;
        int levelTime = 9999;
        if (LevelManager.level < 11)
            levelTime = 60;
        else if (LevelManager.level < 21)
            levelTime = 120;
        else if (LevelManager.level < 31)
            levelTime = 200;
        else if (LevelManager.level < 41)
            levelTime = 250;
        timeAllowed = TimeSpan.FromSeconds(levelTime) ;
        startTime = DateTime.Now;
    }
    void Update()
    {
        var timePassed = DateTime.Now - startTime;
        timerText.text = ((int)(timeAllowed - timePassed).TotalSeconds).ToString();
        if (timePassed > timeAllowed)
        {
            LevelManager.lose.Invoke();
            gameObject.SetActive(false);
        }
    }
    public void Activate()
    {
        gameEnd = false;
        gameObject.SetActive(true);
    }
}