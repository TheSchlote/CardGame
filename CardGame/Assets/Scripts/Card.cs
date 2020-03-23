using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Friends")]
public class Card : ScriptableObject
{
    //Not sure about these\/
    [SerializeField] string id;
    public string ID { get { return id; } }
    // /\

    public string cardName;
    public string description;
    public Sprite artwork;

    public int friendPoint;
    public int attack;
    public int health;

    public void Print()
    {
        Debug.Log(cardName + ": " + description + "Card gives you this many Points!:" + friendPoint);
    }

    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);

    }

}


