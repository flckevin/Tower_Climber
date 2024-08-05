using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;
using DG.Tweening;


[RequireComponent(typeof(Seeker))]

public class EntitiesCore : MonoBehaviour
{
    //============================= ENTITY VAR =================================
    [Header("General Entity Info"),Space(10)]
    public EntitiesData entitiesData; // data of entityw
    public ParticleSystem entityDamageParticle; // particle when entity receive damage
    public MeshRenderer _entityMesh; // mesh of the entity
    public float _health; // health of the entity 
    public MeshFilter _meshFilt;

    [Header("Entity State Info"), Space(10)]
    public Mesh deathStateMesh;
    public Mesh aliveMehs;
    public bool ableToSpawn = true;

    [Header("General Weapon Info"), Space(10)]
    public GameObject entityWeapon;

    protected Sequence _walkSequence;
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

        /******************************************************************************************
         * NOTE 
         * In PrimeTween, tweens and sequences are non-reusable, so there is no direct equivalent.
         * Instead, start a new animation in the desired direction (see the example below).
         * Starting new animations in PrimeTween is extremely fast, so there is no need for caching.

         ******************************************************************************************/

        _walkSequence = DOTween.Sequence()
        .Append(_entityMesh.transform.DOMoveY(_entityMesh.transform.position.y + 0.5f, 0.1f))
        .Append(_entityMesh.transform.DOMoveY(0, 0.1f))
        .SetLoops(-1,LoopType.Restart);

        _walkSequence.Play();
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
    public virtual void OndamageReceive(float _damageReceive = 0,Transform _hitPos = null,Action _effectAction = null,
                                         Action _resetDefaultAction = null,float _delay = 0)
    {
        #region ===================== BLOOD PARTICLE =====================
        //get blood splat from pool
        ParticleSystem _blood = PoolManager.GetItem<ParticleSystem>("BloodSplat");
        //move blood to hit position
        _blood.transform.position = _hitPos.transform.position;
        //activate it blood
        _blood.gameObject.SetActive(true);
        //play blood
        _blood.Play();
        #endregion

        #region ===================== DECREASE HEALTH =====================
        //decrease health
        _health -= _damageReceive;
        //if healthless or equal to 0
        if (_health <= 0) 
        {
            //call on death event
            OnDeath();
        }
        #endregion

        Debug.Log("RECEVING DAMAGE");
    }

    public virtual void OnDeath() 
    {
        _walkSequence.Pause();
        _entityMesh.transform.localPosition = Vector3.zero;

        //disable ai script
        this.enabled = false;

        //set pose to death by changing mesh
        _meshFilt.mesh = deathStateMesh;

        //make AI look at it heading direction
        //_entityMesh.transform.LookAt(GameManager.Instance.tower.transform.position);

        //set rotation to be lying down for suitable at death state
        _entityMesh.transform.rotation = Quaternion.Euler(new Vector3(-90,
                                                        _entityMesh.transform.eulerAngles.y,
                                                        _entityMesh.transform.eulerAngles.z));
        
        //set position so it won't clip over land
        _entityMesh.transform.position = new Vector3(_entityMesh.transform.position.x,
                                                    _entityMesh.transform.position.y + 0.2f,
                                                    _entityMesh.transform.position.z);

        //deactivate entity weapon
        entityWeapon.SetActive(false);

        
        _entityMesh.transform.DOLocalMove(_entityMesh.transform.position + (-this.transform.forward * 1.2f), 2f).OnComplete(() =>
        {
            DOTween.Sequence()
            .SetDelay(1f)
            .Append(_entityMesh.transform.DOMoveY(_entityMesh.transform.position.y - 1.2f, 3f))
            .OnComplete(() => { ableToSpawn = true; });
        });

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

    public void OnResetDefault() 
    {

        //reset health
        _health = entitiesData.health;

        //set rotation to be lying down for suitable at death state
        _entityMesh.transform.rotation = Quaternion.Euler(Vector3.zero);

        //set position so it stand up
        _entityMesh.transform.localPosition = Vector3.zero;
        //setting tag
        this.gameObject.tag = "Enemy";

        //set mesh back to alive mesh
        _meshFilt.mesh = aliveMehs;

        //deactivate entity weapon
        entityWeapon.SetActive(true);

        _walkSequence.Play();

        //set able to spawn so that spawner can not spawn it
        ableToSpawn = false;
    }

    #endregion


}
