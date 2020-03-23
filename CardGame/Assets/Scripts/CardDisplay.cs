using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    //public Card card;

    public Image artworkImage;
    public Text AtkText;
    public Text HPText;

    // Start is called before the first frame update
    void SetSlotHUD(Card card)
    {
        artworkImage.sprite = card.artwork;
        AtkText.text = card.attack.ToString();
        HPText.text = card.health.ToString();
    }

    public void SetATK(int hp)
    {
        AtkText.text = hp.ToString();
    }

    public void SetHP(int atk)
    {
        HPText.text = AtkText.ToString();
    }

}
