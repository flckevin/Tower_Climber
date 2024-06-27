using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesCore : MonoBehaviour
{
    [Header("General Entity Info")]
    public EntitiesData entitiesData; // data of entityw
    public ParticleSystem entityDamageParticle; // particle when entity receive damage
    
    protected float _health; // health of the entity 

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //setup entity
        Setup();
    }

    /// <summary>
    /// function to setup entities stats
    /// </summary>
    protected virtual void Setup()
    {
        //setup entity health
        _health = entitiesData.health;
    }

    /// <summary>
    /// function when entity receive damage
    /// </summary>
    /// <param name="_damageReceive"> amount of damage that entity going to receive </param>
    /// <param name="_knockBack"> amount of value knockback entity </param>
    /// <param name="_slowed"> amount of value to slow entity </param>
    protected virtual void Ondamage(int _damageReceive = 0,
                                    int _knockBack = 0,
                                    int _slowed = 0)
    {
        
        #region  Decrease health
        //decrease health
        _health -= _damageReceive;
        

        #endregion

    }

}
