using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TowerData 
{
    public static float _towerFireRate 
    {
        get { return PlayerPrefs.GetInt("T_FireRage", 1); }
    }

    public static float _towerDamageDeal 
    {
        get { return PlayerPrefs.GetInt("T_FireRage", 1); }
    }


    public static float _towerCheckRadius 
    {
        get { return PlayerPrefs.GetInt("T_FireRage", 12); }
    }
}
