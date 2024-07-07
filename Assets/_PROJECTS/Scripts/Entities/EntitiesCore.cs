using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;

[RequireComponent(typeof(Seeker))]

public class EntitiesCore : MonoBehaviour
{
    //============================= ENTITY VAR =================================
    [Header("General Entity Info")]
    public EntitiesData entitiesData; // data of entityw
    public ParticleSystem entityDamageParticle; // particle when entity receive damage
    [HideInInspector] public float _health; // health of the entity 
    //============================= ENTITY VAR =================================

    //============================= AI VAR =================================
    [HideInInspector]public Path path; //store a star path to move to desired target
    [HideInInspector] public int currentPathIndex; // current path
    protected Seeker _seeker;// seeker ai
    protected IEntityStates _states; //storage of the state to execute
    //============================= AI VAR =================================

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
        //store target position
        Vector3 _target = new Vector3(GameManager.Instance.tower.transform.position.x,
                                      this.transform.position.y,
                                      GameManager.Instance.tower.transform.position.z);
        //start path
        _seeker.StartPath(transform.position, _target, OnScanPathComplete);
        //======================= SET =======================

    }

    private void Update()
    {
        OnApproachExecute();
    }

    #region======================== PATH FUNCTIONS ========================

    /// <summary>
    /// function to check path left of the entity
    /// </summary>
    protected virtual void OnApproachExecute() 
    {
        //if path does not exist then do not execute
        if (path == null || _states == null) { return; }
        //execute behaviour
        _states.Execute(this);
    }

    void OnScanPathComplete(Path p) 
    {
        //if there is not error while scannign
        if (!p.error)
        {
            //store those path so our AI can follow
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
        }
    }

    #endregion


    #region======================== ENTITY FUNCTIONS ========================
    /// <summary>
    /// function on recieve damage
    /// </summary>
    /// <param name="_damageReceive"> amount of damage to receive </param>
    /// <param name="_effectAction"> effect that going to be apply on </param>
    /// <param name="_resetDefaultAction"> fucntion to reset effect </param>
    /// <param name="_delay"> delay before reseting back to normal </param>
    public virtual void OndamageReceive(float _damageReceive = 0,Action _effectAction = null,
                                         Action _resetDefaultAction = null,float _delay = 0)
    {
        #region  Decrease health
        //decrease health
        _health -= _damageReceive;
        
        //if healthless or equal to 0
        if (_health <= 0) 
        {
            //disable ai script
            this.enabled = false;
            //call on death event
            OnDeath();
        }
        #endregion

        Debug.Log("RECEVING DAMAGE");
    }

    public virtual void OnDeath() 
    { 
    
    }

    /// <summary>
    /// function to change state
    /// </summary>
    /// <param name="_stateToChange"> state to change to </param>
    public void StateChange( IEntityStates _stateToChange)
    { 
        //changing state
        _states = _stateToChange;
    }

    #endregion


}
