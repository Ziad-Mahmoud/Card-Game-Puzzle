using UnityEngine;
using UnityEngine.UI;
public class CardsManager : MonoBehaviour
{
    // global scope
    void Start()
    {
        // local variable 
        var sprites = Resources.LoadAll<Sprite>("Sprites");
        var cardPrefab = Resources.Load<GameObject>("Prefabs/Card");
        var parent = GameObject.Find("Cards Group");

        for (int i = 0; i < sprites.Length * 2; i++)
        {
            var cardObject = Instantiate(cardPrefab, parent.transform);
            cardObject.GetComponent<Image>().sprite = sprites[i%sprites.Length];
        }
    }
}