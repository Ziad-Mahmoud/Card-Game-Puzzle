using UnityEngine;
public class CardSelector : MonoBehaviour
{
    // this is comment 
    // variable is box stores data
    // definition syntax is
    // dataType nameIdentifier;
    static int cardCounter;

    // semi-colon ; is line terminator

    public GameObject card1;
    public GameObject card2;



    // Start is called before first frame update
    // Called once at game start
    private void Start()
    {
        cardCounter = 0;
    }

    // Method / function
    // accessModifier returnType MethodName() { }
    // public/private
    // default is private
    // void means return nothing
    public void CardClick()
    {
        cardCounter++;
        if (cardCounter == 2) 
        {
            card1.SetActive(false);
            card2.SetActive(false);
        }
    }
}