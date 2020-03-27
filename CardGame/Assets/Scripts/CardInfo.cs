using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour
{
    public Text nameText;
    public Text descriptionText;
    public GameObject cardObject;

    public GameObject CardInfoPanel;
    public PopulateAvailableCards cardGrids;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCardInfo(Card card)
    {
        CardInfoPanelShow();
        nameText.text = card.cardName;
        descriptionText.text = card.cardDescription;
        cardObject.GetComponent<CardDisplay>().card = card;
        cardObject.GetComponent<CardDisplay>().artWork.sprite = card.artWork;
        cardObject.GetComponent<CardDisplay>().statsText.text = card.ATK.ToString("D2") + "/" + card.HP.ToString("D2");
    }

    public void CardInfoPanelShow()
    {
        CardInfoPanel.SetActive(true);
    }
    public void CardInfoPanelHide()
    {
        CardInfoPanel.SetActive(false);
    }
}
