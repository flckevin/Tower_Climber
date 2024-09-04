using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{

    [Header("TOWER PROJECTILE")]
    public ProjectileBase projectile;                               //projectile of tower




    //======================================== PRIVATE/HIDDEN VAR ========================================

    [HideInInspector]public int enemyKilled;                        //counter for amount of enemy killed in a wave
    private SphereCollider _sphereCol;                              //get sphere collider
    private List<EntitiesCore> _enemy = new List<EntitiesCore>();   // list to store all enmey
    private EntitiesCore _currentTarget;                            // target to focus to kill
    private float _time;                                            //last time of the frame to calculate fire rate
    

    //=============================================================================================


    // Start is called before the first frame update
    void Start()
    {
        //====================== GET ==========================
        //get sphere collider
        _sphereCol = GetComponent<SphereCollider>();
        //====================== GET ==========================

        //====================== SET ==========================

        //set gamemanger to have this tower
        GameManager.Instance.tower = this;

        //set sphere radius
        _sphereCol.radius = TowerData.towerCheckRadius;
        
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
        float _nextTimeFire = 1 / TowerData.towerFireRate;

        //if focus target not exist
        if (_currentTarget == null)
        {
            //get focus target
            _currentTarget = _enemy[0];
            
        }
        else 
        {
            //if current health is less than 0
            if (_currentTarget._health <= 0)
            {
                //remove target from list
                _enemy.Remove(_currentTarget);
                //set current target to null
                _currentTarget = null;
                return;
            }
        }

        //========================= FIRE =========================
        //if time is already pass through time to shoot then shoot
        if (_time >= _nextTimeFire) 
        {

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

            //call back event of projectile on hit
            //which deal damage to the enemy
            _projectile._callBackOnHit = () => 
            { 
                //dealing damage to enemy
                _currentTarget.OndamageReceive(TowerData.towerDamageDeal,_projectile.transform);

                //increase amount of enemy been killed
                enemyKilled++;

                //calculate value of enemy left for slider to display on screen
                float _slideVal = (float)(GameManager.Instance.entitySpawner.maximumEntityPerWave - enemyKilled) / GameManager.Instance.entitySpawner.maximumEntityPerWave;
                //display value of enemy rest on screen
                GameManager.Instance.waveSlider.value = _slideVal;
                
                Debug.Log($"enemy killed {_slideVal}");
                
                //if enemy killed is larger or equal to maximum entity spawn amount
                if (enemyKilled >= GameManager.Instance.entitySpawner.maximumEntityPerWave) 
                {
                    //reset amount of enemy killed so it wont increase wave in ridiculous way
                    enemyKilled = 0;
                    //end wave
                    GameManager.Instance.entitySpawner.OnEndWave(GameManager.Instance.entitySpawner.waveDelay);
                }
            };

            //set time back to default so we can calculate next time to fire
            _time = 0;
        }
        //========================= FIRE =========================

    }

    private void OnTriggerEnter(Collider other)
    {
        //check if object enter to collider has enemy tag
        if (other.CompareTag("Enemy")) 
        {
            //Debug.Log("ENEMY");
            //add that to enemy list
            EntitiesCore _entity = other.GetComponent<EntitiesCore>();
            Debug.Log($"Adding {_entity}");
            _enemy.Add(_entity);
        }
    }

    public void UpgradeTower() 
    { 
    
    }

}
