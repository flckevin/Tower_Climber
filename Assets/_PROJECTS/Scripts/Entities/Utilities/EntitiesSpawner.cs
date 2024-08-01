using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesSpawner : MonoBehaviour
{
    [Header("SpawnerInfo"), Space(10)]
    public int amountEntityPerTypeOnScene; // amount per enemy that going to be in scene
    public int amountBloodPrefabOnScene; //amount of blood particle that going to be on scene
    public float randSphereRadius; // sphere radius

    [Header("Entity"),Space(10)]
    public int maximumEntityPerWave; // maximum amount of enemy that can spawn during current wave
    public float delayBetween; // delay between waves
    public List<EntitiesCore> entities;//list of all avalible entity in game

    [Header("Entity Ultility"), Space(10)]
    public ParticleSystem bloodParticlePrefab;//blood particle prefab

    private int spawnedAmount; // spawn amount
    private Coroutine _spawnCou; // spawn couroutine
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
            //keep spawning
            //get random point on a circle
            Vector3 _spawnPos = randomCirlceSpawnPoint();
            //get an entity from pool
            EntitiesCore _nextEntity = PoolManager.GetItem<EntitiesCore>(entities[0].name);

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
            //spawnedAmount++;

            //if spawned amount is more than maximum 
            if (spawnedAmount >= maximumEntityPerWave) 
            {
                //break out of loop
                break;
            }

            //delay few second
            yield return new WaitForSeconds(delayBetween);  
        }

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
