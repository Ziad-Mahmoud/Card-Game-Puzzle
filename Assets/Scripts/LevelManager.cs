using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    public static UnityEvent win = new();
    public static UnityEvent lose = new();

    [SerializeField] Button startGame;
    [SerializeField] GameObject winPanel , losePanel, finishPanel;
    [SerializeField] TextMeshProUGUI trialsText, levelsText, codeText;

    [SerializeField] AdsManager adsManager;
    [SerializeField] SoundManager soundManager;

    public static int level = 1;
    public static int trials = 5;

    static bool firstStart = true;
    private void Awake()
    {
        level = PlayerPrefs.GetInt("level");
        if (level == 0)
            level = 1;
        if(!firstStart)
        {
            startGame.gameObject.SetActive(false);
            startGame.onClick.Invoke();
        }
        firstStart = false;
        trials = PlayerPrefs.GetInt("trials");
    }
    void Start()
    {
        levelsText.text = "Level : " + level;
        win.AddListener(WinHandlerDelayed);
        lose.AddListener(LoseHandler);

        winPanel.GetComponentInChildren<Button>(true).onClick.AddListener(NextLevel);
        losePanel.GetComponentInChildren<Button>(true).onClick.AddListener(Retry);
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
        FindObjectOfType<Timer>().gameObject.SetActive(false);
        adsManager.ShowInterstitial();
    }
    void LoseHandler()
    {
        soundManager.LoseSound();
        losePanel.SetActive(true);
        adsManager.ShowInterstitial();
    }

    void NextLevel()
    {
        level++;
        PlayerPrefs.SetInt("level",level);
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
        PlayerPrefs.SetInt("trials", trials);

        trialsText.text = trials.ToString();
        return myReturn;
    }
    public void AddHeart()
    {
        trials++;
        PlayerPrefs.SetInt("trials", trials);
        trialsText.text = trials.ToString();
    }
}