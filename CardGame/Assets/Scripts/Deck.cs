using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{

    //public static List<Card> deck = new List<Card>();
    public static Dictionary<int, Card> deck = new Dictionary<int, Card>();
    public List<Card> cards = new List<Card>();

    private void Awake()
    {
        int i = 0;
        foreach(Card card in cards)
        {
            deck.Add(i, card);
                i++;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //Lets add our cards to our deck
        //deck.Add(Card. );
    }

    // Update is called once per frame
    void Update()
    {
        //When we draw cards lets take them from here.
    }
}
