using UnityEngine;
public class CardsManager : MonoBehaviour
{
    public static Card[] cards;
    public static Sprite[] sprites;
    public static Sprite cardBack;
    public static int lastId = -1;
    public static int lastIndex = -1;
    // global scope
    void Start()
    {
        // local variable 
        sprites = Resources.LoadAll<Sprite>("Sprites");
        cardBack = Resources.Load<Sprite>("CardBack");
        cards = new Card[sprites.Length * 2];
        var cardPrefab = Resources.Load<GameObject>("Prefabs/Card");
        var parent = GameObject.Find("Cards Group");

        for (int i = 0; i < cards.Length; i++)
        {
            var obj = Instantiate(cardPrefab, parent.transform);
            int id = i % sprites.Length;
            var card = obj.GetComponent<Card>();
            card.FlipToBack();
            card.id = id;
            card.index = i;
            cards[i] = card;
        }
    }
}