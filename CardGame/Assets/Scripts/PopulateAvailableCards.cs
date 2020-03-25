using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PopulateAvailableCards : MonoBehaviour
{
    public GameObject prefab;
    public Transform grid;
    

    public List<Card> cards = new List<Card>();
    //public int numberToCreate;

    // Start is called before the first frame update
    void Start()
    {
        Populate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Populate()
    {
        GameObject newObj;

        foreach(Card card in cards)
        {
            newObj = (GameObject)Instantiate(prefab, grid);
            newObj.GetComponent<CardDisplay>().card = card;
        }
    }

    public void SelectCard()
    {
        //GameObject newObj;
        Debug.Log(grid.childCount);
    }

}
