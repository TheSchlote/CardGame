using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    public CardDisplayProperties[] properties;
    public GameObject statsHolder;

    public void Start()
    {
        LoadCard(card);
    }

    public void LoadCard(Card c)
    {
        if (c == null)
            return;

        card = c;

        c.cardType.OnSetType(this);
        
        for(int i = 0; i < c.properties.Length; i++)
        {
            CardProperties cp = c.properties[i];

            CardDisplayProperties p = GetProperty(cp.element);
            if(p == null)
            {
                continue;
            }

            if(cp.element is ElementInt)
            {
                p.text.text = cp.intValue.ToString();
            }
            else
            if (cp.element is ElementArt)
            {
                p.img.sprite = cp.sprite;
            }
        }

    }

    public CardDisplayProperties GetProperty(Element e)
    {
        CardDisplayProperties result = null;

        for (int i = 0; i < properties.Length; i++)
        {
            if(properties[i].element == e)
            {
                result = properties[i];
                break;
            }
        }

        return result;
    }

}
