using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Image artWork;
    public Text statsText;

    // Start is called before the first frame update
    void Start()
    {
        statsText.text = card.ATK.ToString("D2") + "/" + card.HP.ToString("D2") ;
        artWork.sprite = card.artWork;
    }
}
