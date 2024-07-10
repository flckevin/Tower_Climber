using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesSpawner : MonoBehaviour
{
    [Header("SpawnerInfo"), Space(10)]
    public int amountPerEnemy; // amount per enemy that going to be in scene
    public int amountBloodParticlePrefab; //amount of blood particle that going to be on scene
    public float randSphereRadius; // sphere radius

    [Header("Entity"),Space(10)]
    public int maxmimumEnemyAmount; // maximum amount of enemy that can spawn during current wave
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
            PoolManager.Setup(entities[i], amountPerEnemy);
        }

        //setup pool of blood particle prefab
        PoolManager.Setup(bloodParticlePrefab, amountBloodParticlePrefab);

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

        while (spawnedAmount < maxmimumEnemyAmount) 
        {
            Vector3 _spawnPos = randomSpawnPoint();

            yield return new WaitForSeconds(delayBetween);  
        }

    }

    private Vector3 randomSpawnPoint() 
    {
        Vector3 _spawnPos;

        Vector3 _spawnDir = Random.onUnitSphere*randSphereRadius; // could be anywhere on a sphere
        _spawnDir.y = 0;
        _spawnPos = _spawnDir;
        _spawnPos.Normalize();
        return _spawnPos;
    }
}
