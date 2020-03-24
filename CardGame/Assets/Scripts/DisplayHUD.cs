using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHUD : MonoBehaviour
{
    public Text stats;

    public void SetHud(Unit unit)
    {
        stats.text = unit.ATK + "/" + unit.HP;
    }

   
}
