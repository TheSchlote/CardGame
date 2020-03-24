using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/Friend")]
public class Card : ScriptableObject
{
    public string cardName;
    public string cardDescription;
    public Sprite artWork;

    public int points;
    public int ATK;
    public int HP;

}
