using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TowerData 
{
    #region =========================== GENERAL DATA ===========================
    public static float towerFireRate 
    {
        get { return PlayerPrefs.GetFloat("T_FireRate", 1); }
        set 
        {
            PlayerPrefs.SetFloat("T_FireRate", value);
        }
    }

    public static float towerDamageDeal 
    {
        get { return PlayerPrefs.GetFloat("T_FireDamage", 1); }
        set 
        {
            PlayerPrefs.SetFloat("T_FireDamage", value);
        }
    }

    public static float towerCheckRadius 
    {
        get { return PlayerPrefs.GetFloat("T_FireRage", 12); }
        set 
        {
            PlayerPrefs.SetFloat("T_FireRage", value);
        }
    }

    #endregion
}
