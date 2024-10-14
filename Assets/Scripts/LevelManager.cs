using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    public static UnityEvent win = new();
    public static UnityEvent lose = new();

    [SerializeField] GameObject winPanel , losePanel;
    [SerializeField] TextMeshProUGUI trialsText;
    [SerializeField] TextMeshProUGUI levelsText;

    public static int level = 1;
    public static int trials = 500;
    private void Awake()
    {
        level = PlayerPrefs.GetInt("level");
        //trials = PlayerPrefs.GetInt("trials");
    }
    void Start()
    {
        levelsText.text = "Level : " + level;
        win.AddListener(WinHandler);
        lose.AddListener(LoseHandler);

        winPanel.GetComponentInChildren<Button>(true).onClick.AddListener(NextLevel);
        losePanel.GetComponentInChildren<Button>(true).onClick.AddListener(Retry);
    }

    void WinHandler()
    {
        winPanel.SetActive(true);
        FindObjectOfType<Timer>().gameObject.SetActive(false);
        AdsManager.ShowInterstitial();
    }
    void LoseHandler()
    {
        losePanel.SetActive(true);
        AdsManager.ShowInterstitial();
    }

    void NextLevel()
    {
        level++;
        PlayerPrefs.SetInt("level",level);
        Retry();
    }
    void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public bool UseTry()
    {
        bool myReturn = false;
        if (trials > 1)
        {
            myReturn = true;
            trials--;
        }
        PlayerPrefs.SetInt("trials", LevelManager.trials);

        trialsText.text = trials.ToString();
        return myReturn;
    }
}
