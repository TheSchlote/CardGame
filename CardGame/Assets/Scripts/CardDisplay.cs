using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Image artWork;
    public Text statsText;
    //I think we can use this to keep track of which card is which.
    public int cardKey;

    // Start is called before the first frame update
    void Start()
    {
        if(card.AbilityCard || card.BuffCard)
        {
            statsText.text = "Ability";
        }
        else
        {
            statsText.text = card.ATK.ToString("D2") + "/" + card.HP.ToString("D2");
        }
        artWork.sprite = card.artWork;
    }
}
