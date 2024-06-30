using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(AIPath))]
public class EntitiesCore : MonoBehaviour
{
    [Header("General Entity Info")]
    public EntitiesData entitiesData; // data of entityw
    public ParticleSystem entityDamageParticle; // particle when entity receive damage
    
    protected float _health; // health of the entity 
    protected Seeker _seeker;// seeker ai

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Setup();
    }


    /// <summary>
    /// function to setup entities stats
    /// </summary>
    protected virtual void Setup()
    {
        //======================== GET ==========================
        //storing ai seeker
        _seeker = GetComponent<Seeker>();
        //======================== GET ==========================


        //======================= SET =======================
        //setup entity health
        _health = entitiesData.health;
        //setting tag
        this.gameObject.tag = "Enemy";
        //start path
        _seeker.StartPath(this.transform.position, GameManager.Instance.tower.transform.position, OnReachTarget);
        //======================= SET =======================

        //Debug.Log("Finished");
    }


    /// <summary>
    /// function on recieve damage
    /// </summary>
    /// <param name="_damageReceive"> amount of damage to receive </param>
    /// <param name="_effectAction"> effect that going to be apply on </param>
    /// <param name="_resetDefaultAction"> fucntion to reset effect </param>
    /// <param name="_delay"> delay before reseting back to normal </param>
    protected virtual void Ondamage(int _damageReceive = 0,Action _effectAction = null,
                                    Action _resetDefaultAction = null,float _delay = 0)
    {
        
        #region  Decrease health
        //decrease health
        _health -= _damageReceive;
        #endregion
    }


    protected virtual void OnReachTarget(Path p) { }

}
