using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    public static UnityEvent win = new();
    public static UnityEvent lose = new();

    [SerializeField] GameObject winPanel , losePanel;

    public static int level = 1;
    public static int trials = 5;
    private void Awake()
    {
        
    }
    void Start()
    {
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
        Retry();
    }
    void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
