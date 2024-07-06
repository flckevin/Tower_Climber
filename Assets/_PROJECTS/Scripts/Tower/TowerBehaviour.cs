using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    [Header("GENERAL TOWER INFO")]
    public float checkRadius; //radius of tower to check

    [Header("PROJECTILE")]
    public ProjectileBase projectile;//projectile of tower

    [Header("WEAPON INFO")]
    public float fireRate;//fire rate of the tower

    private SphereCollider _sphereCol;//get sphere collider
    private List<EntitiesCore> _enemy = new List<EntitiesCore>(); // list to store all enmey
    private EntitiesCore _currentTarget; // target to focus to kill
    private float _time;//last time of the frame to calculate fire rate

    // Start is called before the first frame update
    void Start()
    {
        //====================== GET ==========================
        //get sphere collider
        _sphereCol = GetComponent<SphereCollider>();
        //====================== GET ==========================

        //====================== SET ==========================

        //set sphere radius
        _sphereCol.radius = checkRadius;
        
        //if there is projectile to spawn
        if (projectile != null) 
        {
            //setup projectile pool
            PoolManager.Setup(projectile,20);
        } 
        
        //====================== SET ==========================
    }

    void Update()
    {
        //call tower shoot behaviour
        TowerShootBehaviour();
    }

    /// <summary>
    /// function of tower behaviour
    /// </summary>
    private void TowerShootBehaviour() 
    {
        //if enemy count is less than 0 then dont execute
        if (_enemy.Count <= 0) return;

        //increase time rate
        _time += Time.deltaTime;
        //calculate next time to fire
        float _nextTimeFire = 1 / fireRate;

        //if time is already pass through time to shoot then shoot
        if (_time >= _nextTimeFire) 
        {
            //=============== FIRE ===============
            //if focus target not exist
            if (_currentTarget == null)
            {
                //get focus target
                _currentTarget = _enemy[0];
                //if current health is less than 0
                if (_currentTarget._health <= 0) 
                { 
                    //remove target from list
                    _enemy.Remove(_currentTarget);
                    return;
                }
            }

            //get projectile from list
            ProjectileBase _projectile = PoolManager.GetItem<ProjectileBase>(projectile.name);
            
            //set start position for them
            Vector3 _startPos = new Vector3(this.transform.position.x,
                                            this.transform.position.y + 6,
                                            this.transform.position.z);
            //activate projectile
            _projectile.gameObject.SetActive(true);
            //set back to default value
            _projectile.ResetDefault();
            //shoot to target
            _projectile.Shoot(_startPos,_currentTarget.transform, 0.3f);
            //=============== FIRE ===============

            //set time back to default so we can calculate next time to fire
            _time = 0;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //check if object enter to collider has enemy tag
        if (other.CompareTag("Enemy")) 
        {
            //add that to enemy list
            _enemy.Add(other.GetComponent<EntitiesCore>());
        }
    }

}
