using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesSpawner : MonoBehaviour
{
    [Header("SPAWNER GENERAL INFO"), Space(10)]
    public int amountEntityPerTypeOnScene;          // amount per enemy that going to be in scene
    public int amountBloodPrefabOnScene;            //amount of blood particle that going to be on scene
    public float randSphereRadius;                  // sphere radius

    [Header("SPAWNER TEMPLATE"), Space(10)]
    public EntitySpawnerWaveTemplate[] entityWaveSpawnTemplate; // template of which type enemy to spawn next
    public AnimationCurve[] spawnCurve;                         //animation curve so the spawner can use it to spawn enemy base on curve

    [Header("ENTITY"),Space(10)]
    public int maximumEntityPerWave = 13;               // maximum amount of enemy that can spawn during current wave
    public float delayBetween;                          // delay between waves
    public List<EntitiesCore> entities;                 //list of all avalible entity in game

    [Header("ENTITY ULTILIY"), Space(10)]
    public ParticleSystem bloodParticlePrefab;//blood particle prefab


    #region ==================================== PRIVATE VAR ====================================
    //==================================== PRIVATE VAR ====================================


    // ENEMY TYPE ID
    private int _currentEnemyTypeID = 0; // enemy type id IN TEMPLATE
    private int _CurrentEnemyTypeID // enemy type id IN TEMPLATE
    {

        get 
        {
            return _currentEnemyTypeID;
        }
        set 
        {
            if (_currentEnemyTypeID >= entityWaveSpawnTemplate[_TemplateID].entity.Length - 1)
            {
                _currentEnemyTypeID = 0;
            }
            else 
            {
                _currentEnemyTypeID = value;
            }
        }
    
    }

    //SPAWN TEMPLATE ID
    private int _templateID = 0; // id of template currently using to spawn enemy
    private int _TemplateID // id of template currently using to spawn enemy
    {
        get 
        {
            return _templateID;
        }
        set 
        {
            if (_templateID >= entityWaveSpawnTemplate.Length - 1)
            {
                _templateID = 0;
            }
            else 
            {
                _templateID = value;
            }
        }
    }

    //ANIMATION CURVE ID
    private int _currentCurveID;
    private int _CurrentCurveID
    {
        get 
        {
            return _currentCurveID;
        }
        set 
        {
            if (_currentCurveID >= spawnCurve.Length || _currentCurveID < 0)
            {
                _currentCurveID = 0;
            }
            else
            {
                _currentCurveID = value;
            }

        }
    }

    private float _currentCurveKeyID;
    private float _CurrentCurveKeyID
    {
        get 
        {
            return _currentCurveKeyID;
        }
        set 
        {
            if (_currentCurveKeyID >= 1)
            {
                _currentCurveKeyID = 0;
            }
            else 
            {
                _currentCurveKeyID = value;
            }
        }
    }

   [SerializeField] private int spawnedAmount; // spawn amount
    private Coroutine _spawnCou; // spawn couroutine

    //=====================================================================================

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //===================== SET =====================
       
        //loop all avalible entity
        for (int i =0; i < entities.Count; i++) 
        {
            //spawn each entity 20 times
            PoolManager.Setup(entities[i], amountEntityPerTypeOnScene);
        }

        //setup pool of blood particle prefab
        PoolManager.Setup(bloodParticlePrefab, amountBloodPrefabOnScene);

        //start spawning
        StartSpawning();
        //===================== SET =====================
    }

    /// <summary>
    /// function to start spawning wave of enemy
    /// </summary>
    public void StartSpawning() 
    {
        //calling spawn function
        _spawnCou = StartCoroutine(SpawnCou());
    }

    /// <summary>
    /// function to stop spawning wave of enemy
    /// </summary>
    public void StopSpawning() 
    { 
        //calling stop function
        StopCoroutine(SpawnCou());
    }

    IEnumerator SpawnCou() 
    {
        //while the spawner not reach maximum amount of entity in the wave
        while (spawnedAmount < maximumEntityPerWave) 
        {
            Debug.Log($"CURVE: {(int)(spawnCurve[0].Evaluate(0.5f)*10)}");
            //keep spawning
            //get random point on a circle
            Vector3 _spawnPos = randomCirlceSpawnPoint();
            //get an entity from pool
            EntitiesCore _nextEntity = PoolManager.GetItem<EntitiesCore>(entityWaveSpawnTemplate[_TemplateID].entity[_CurrentEnemyTypeID]);
            //next enemy to spawn
            _CurrentEnemyTypeID++;

            //if next entity able to spawn
            if (_nextEntity.ableToSpawn == true) 
            {
                //set that entity position to be at that random spawn position
                _nextEntity.transform.position = _spawnPos;
                //activate that entity
                _nextEntity.gameObject.SetActive(true);
                //enable entity script
                _nextEntity.enabled = true;
                //reset entity
                _nextEntity.OnResetDefault();
            }


            //increase amount have spawned
            spawnedAmount++;

            

            //if spawned amount is more than maximum 
            if (spawnedAmount >= maximumEntityPerWave) 
            {
                yield return new WaitForSeconds(2f);
                //break out of loop
                OnEndWave();
                break;
            }

            //delay few second
            yield return new WaitForSeconds(delayBetween);  
        }

        

    }

    private void OnEndWave() 
    {
        _CurrentCurveKeyID += 0.1f;
        maximumEntityPerWave = MaximumAmountCalculation();
        GameManager.Instance.currentWave++;
        spawnedAmount = 0;
        StartSpawning();
    }

    private int MaximumAmountCalculation() 
    {
        
        int _oldKeyVal = (int)(spawnCurve[_CurrentCurveID].Evaluate(_CurrentCurveKeyID - 0.1f) * 10); //get old key value
        int _newKeyVal = (int)(spawnCurve[_CurrentCurveID].Evaluate(_CurrentCurveKeyID) * 10);        //get new key value
        int _finalVal;                                                                                //final result

        //if new key value is larger than new key value or equal to it
        if (_newKeyVal > _oldKeyVal || _newKeyVal == _oldKeyVal)
        {
            //add it maximum value that player can spawn
            _finalVal = maximumEntityPerWave + _newKeyVal;
        }
        else //it is less than new key value
        {
            //decrease it
            _finalVal = maximumEntityPerWave - _oldKeyVal;
        }

        //return as final result
        return _finalVal;
    }

    private Vector3 randomCirlceSpawnPoint() 
    {
        Vector3 _spawnPos;

        Vector3 randomPos = Random.insideUnitSphere * randSphereRadius;
        randomPos += transform.position;
        randomPos.y = 0f;

        Vector3 direction = randomPos - transform.position;
        direction.Normalize();

        float dotProduct = Vector3.Dot(transform.forward, direction);
        float dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);

        randomPos.x = Mathf.Cos(dotProductAngle) * randSphereRadius + transform.position.x;
        randomPos.z = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * randSphereRadius + transform.position.z;

        _spawnPos = randomPos;

        return _spawnPos;
    }
}
