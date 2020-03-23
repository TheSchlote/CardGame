using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardType : ScriptableObject
{
    public string typeName;

    public virtual void OnSetType(CardDisplay viz)
    {
        Element t = Settings.GetResourceManager().typeElement;
        CardDisplayProperties type = viz.GetProperty(t);

        //I dont want to display this..
        //type.text.text = typeName;
    }
}
