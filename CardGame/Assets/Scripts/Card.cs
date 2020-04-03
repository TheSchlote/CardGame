using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/Friend")]
public class Card : ScriptableObject
{
    public string cardName;
    public string cardDescription;
    public Sprite artWork;

    public CardType type;

    public int points;
    public int ATK;
    public int HP;

    //This is how we know if its not a monster
    //public bool AbilityCard;
    //public bool BuffCard;

}



