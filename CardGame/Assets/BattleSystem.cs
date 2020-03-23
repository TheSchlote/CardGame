using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Phases { START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleSystem : MonoBehaviour
{
    public Phases state;

    //public ScriptableObject slot1;
    public GameObject cardSlot1;

    public Image artworkImage;
    public Text AtkText;
    public Text HPText;

    Card Slot1Card;

    // Start is called before the first frame update
    void Start()
    {
        state = Phases.START;
        SetupBattle();
    }
    void SetupBattle()
    {
        //possibly a for loop for each of the card slots
        GameObject Slot1GO = Instantiate(cardSlot1);
        Slot1Card = Slot1GO.GetComponent<Card>();

        cardSlot1.GetComponentInChildren<Image>(artworkImage);
    }

}
