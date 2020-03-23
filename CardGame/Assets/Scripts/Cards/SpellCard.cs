using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Spell")]
public class SpellCard : CardType
{
    public override void OnSetType(CardDisplay viz)
    {
        base.OnSetType(viz);
        //Eventually Ill use this to give it stats or spell stuff
        //viz.statsHolder.SetActive(false);
    }
}
