using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradeM
{
    public static Dictionary<UpgradeID, Action> upgrades = new Dictionary<UpgradeID, Action>() 
    {
        {UpgradeID.attackDamage,() => {TowerData.towerDamageDeal += 0.1f;  } },
        {UpgradeID.attackSpeed,()=>{  } }
    };
}

public enum UpgradeID 
{ 
    //attack
    attackDamage,
    attackSpeed,
    attackRange,
    attackCritDamage,
    attackCritChance,
    multiShotChance,

    //health
    healthAmount,
    healthRegeneration,
    shield,
    lifeSteal,
    damageReflectChance,

    //coin
    coinBonus,


}
