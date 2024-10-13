using UnityEngine;
using UnityEngine.UI;
using static CardsManager;
public class Card : MonoBehaviour
{
    public Image image;
    public Button button;
    public int id;
    public int index;
    public bool hit = false;
    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }
    private void Start()
    {
        button.onClick.AddListener(OnClickHandler);
    }
    void OnClickHandler()
    {
        if (image.sprite != cardBack) return;

        image.sprite = sprites[id];
        if(lastId == -1)
        {
            lastId = id;
            lastIndex = index;
        }
        else
        {
            if (lastId == id && lastIndex != index)
            {
                hit = true;
                cards[lastIndex].hit = true;

                bool win = true;
                foreach (var card in cards)
                    if (!card.hit)
                        win = false;
                if (win)
                    LevelManager.win.Invoke();

                lastId = -1;
                lastIndex = -1;
            }
            else
            {
                foreach (Card card in cards) 
                    card.button.interactable = false;
                Invoke(nameof(CardMiss), 1);
            }
        }
    }
    void CardMiss()
    {
        FlipToBack();
        cards[lastIndex].FlipToBack();
        lastId = -1;
        lastIndex = -1;
        foreach (Card card in cards)
            card.button.interactable = true;
    }
    public void FlipToBack()
    {
        if (!hit)
            image.sprite = cardBack;
    }
}