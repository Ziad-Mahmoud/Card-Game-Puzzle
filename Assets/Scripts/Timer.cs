using TMPro;
using UnityEngine;
public class Timer : MonoBehaviour
{
    public GameObject losePanel;
    public float timer = 10;
    TextMeshProUGUI timerText;
    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        timerText.text = ((int)(timer - Time.time)).ToString();
        if (Time.time >= timer)
        {
            losePanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}