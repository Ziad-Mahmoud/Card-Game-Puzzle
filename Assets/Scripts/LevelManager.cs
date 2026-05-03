using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static System.IO.File;
using static UnityEngine.Random;
using static UnityEngine.JsonUtility;
using static UnityEngine.Application;
using System.Collections;
public class LevelManager : MonoBehaviour
{
    public static UnityEvent win = new();
    public static UnityEvent lose = new();

    [SerializeField] Button startGame, retry, nextLevel;
    [SerializeField] GameObject winPanel, losePanel, finishPanel, watchAdTimerBtn, watchAdTimerTxt;
    [SerializeField] TextMeshProUGUI trialsText, levelsText, codeText;

    [SerializeField] AdsManager adsManager;
    [SerializeField] Timer timer;
    [SerializeField] SoundManager soundManager;

    public static int level = 96;
    public static int trials = 5;
    public static bool firstStart = true;

    static string path = string.Empty;
    [Serializable]
    public class Data
    {
        public int level;
        public int trials;
        public bool lastVersion = false;
        public bool back5Levels = false;
    }
    private void Awake()
    {
        if (path == string.Empty)
            path = persistentDataPath + "/flipData.json";
        //Load();
        levelsText.text = "Level : " + level;
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
        StopAllCoroutines();
        StartCoroutine(SaveSystem());
    }
    IEnumerator SaveSystem()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Save();
            trialsText.text = trials.ToString();
            levelsText.text = "Level : " + level;
        }
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
            codeText.text = "L" + Range(100000000, 999999999).ToString();
        }
        else
            winPanel.SetActive(true);
        FindFirstObjectByType<Timer>().gameObject.SetActive(false);
        adsManager.LoadInterstitial(false, false);
    }
    void LoseHandler()
    {
        if (Timer.gameEnd) return;
        Timer.gameEnd = true;
        soundManager.LoseSound();
        losePanel.SetActive(true);
        adsManager.LoadInterstitial(false,false);
    }

    void NextLevel()
    {
        level++;
        Save();
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
        trialsText.text = trials.ToString();
        return myReturn;
    }
    public static void Save()
    {
        WriteAllText(path, ToJson(new Data() { level = level, trials = trials , lastVersion = true, back5Levels = true }));
    }
    public static void Load()
    {
        if (!Exists(path)) return;
        var data = FromJson<Data>(ReadAllText(path));
        if (!data.back5Levels && data.level > 5)
            data.level -= 5;
        level = data.level;
        if (!data.lastVersion && level > 42)
            level = 42;
        trials = data.trials;
    }
}