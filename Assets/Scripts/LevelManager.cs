using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static UnityEngine.PlayerPrefs;
public class LevelManager : MonoBehaviour
{
    public static UnityEvent win = new();
    public static UnityEvent lose = new();

    [SerializeField] Button startGame, retry, nextLevel;
    [SerializeField] GameObject winPanel , losePanel, finishPanel;
    [SerializeField] TextMeshProUGUI trialsText, levelsText, codeText;

    [SerializeField] AdsManager adsManager;
    [SerializeField] SoundManager soundManager;

    public static int level = 1;
    public static int trials = 5;
    public static bool firstStart = true;
    private void Awake()
    {
        if (HasKey("level"))
            level = GetInt("level");
        levelsText.text = "Level : " + level;
        if (HasKey("trials"))
            trials = GetInt("trials");
        trialsText.text = trials.ToString();
    }
    void Start()
    {
        win.AddListener(WinHandlerDelayed);
        lose.AddListener(LoseHandler);

        nextLevel.onClick.AddListener(NextLevel);
        retry.onClick.AddListener(Retry);

        if (!firstStart)
            startGame.onClick.Invoke();
        firstStart = false;
    }
    void WinHandlerDelayed()
    {
        Invoke(nameof(WinHandler), 1);
    }
    void WinHandler()
    {
        soundManager.WinSound();
        if (level == 100)
        {
            finishPanel.SetActive(true);
            codeText.text = Random.Range(100000000, 999999999).ToString();
        }
        else
            winPanel.SetActive(true);
        FindFirstObjectByType<Timer>().gameObject.SetActive(false);
        adsManager.ShowInterstitial();
    }
    void LoseHandler()
    {
        if (Timer.gameEnd) return;
        Timer.gameEnd = true;
        soundManager.LoseSound();
        losePanel.SetActive(true);
        adsManager.ShowInterstitial();
    }

    void NextLevel()
    {
        level++;
        SetInt("level",level);
        Retry();
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public bool UseTry()
    {
        bool myReturn = false;
        if (trials > 0)
        {
            myReturn = true;
            trials--;
        }
        SetInt("trials", trials);

        trialsText.text = trials.ToString();
        return myReturn;
    }
    public void AddHeart()
    {
        trials++;
        SetInt("trials", trials);
        trialsText.text = trials.ToString();
    }
}