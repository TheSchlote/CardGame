using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Friends")]
public class Card : ScriptableObject
{
    public CardType cardType;
    public CardProperties[] properties;
}


