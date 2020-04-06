using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Transform Level2Shop;
    public GameObject FoaJoey;

    public List<Card> buyableCards = new List<Card>();

    private Card FoaJoeyCard;

    // Start is called before the first frame update
    void Start()
    {
        FoaJoey.GetComponent<CardDisplay>().card = buyableCards.Find(x => x.cardName == "Foa_Joey");
        FoaJoey.GetComponent<CardDisplay>().statsText.text = "Ability";
        FoaJoey.GetComponent<CardDisplay>().artWork.sprite = buyableCards.Find(x => x.cardName == "Foa_Joey").artWork;

        FoaJoeyCard = FoaJoey.GetComponent<CardDisplay>().card;

        if (PlayerInfo.playerCardInventory.ContainsKey(FoaJoeyCard))
            Level2Shop.GetComponentInChildren<Button>().GetComponent<Text>().text = "Sold Out";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyLevel2()
    {
        if(PlayerInfo.playerLevel >= 0)
        {
            if (!PlayerInfo.playerCardInventory.ContainsKey(FoaJoeyCard))
                PlayerInfo.playerCardInventory.Add(FoaJoeyCard, 4); 
        }
    }
}
