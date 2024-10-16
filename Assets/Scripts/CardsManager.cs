using System.Collections.Generic;
using UnityEngine;
public class CardsManager : MonoBehaviour
{
    public static Card[] cards;
    public static Sprite[] sprites;
    public static Sprite cardBack;
    public static int lastId = -1;
    public static int lastIndex = -1;

    [SerializeField] SoundManager soundManager;

    // global scope
    void Start()
    {
        if(FindObjectOfType<LevelManager>().UseTry())
            StartGame();
    }
    void StartGame()
    {
        string path = "Sprites";
        if (LevelManager.level >= 95)
            path = "Arrow";
        var temp = Resources.LoadAll<Sprite>(path);
        int cardsCount = LevelManager.level + 1;
        if (cardsCount > 49)
            cardsCount = 49;
        sprites = temp[..cardsCount];
        cardBack = Resources.Load<Sprite>("CardBack");
        cards = new Card[sprites.Length * 2];
        var cardPrefab = Resources.Load<GameObject>("Prefabs/Card");
        var parent = GameObject.Find("Cards Group");

        List<int> ints = new();
        for (int i = 0; i < cards.Length; i++)
            ints.Add(i);
        List<int> shuffled = new();
        while (ints.Count > 0)
        {
            int index = Random.Range(0, ints.Count);
            shuffled.Add(ints[index]);
            ints.RemoveAt(index);
        }

        for (int i = 0; i < cards.Length; i++)
        {
            var obj = Instantiate(cardPrefab, parent.transform);
            int id = shuffled[i] % sprites.Length;
            var card = obj.GetComponent<Card>();
            card.FlipToBack();
            card.id = id;
            card.index = i;
            cards[i] = card;
            card.button.onClick.AddListener(soundManager.CardClick);
        }
    }
}