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
    }
    void LoseHandler()
    {
        losePanel.SetActive(true);
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
