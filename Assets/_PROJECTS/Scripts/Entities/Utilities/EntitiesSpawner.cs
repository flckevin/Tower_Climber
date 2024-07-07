using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesSpawner : MonoBehaviour
{
    public int maxmimumEnemyAmount; // maximum amount of enemy that can spawn during current wave
    public float delayBetween; // delay between waves
    public List<EntitiesCore> entities;//list of all avalible entity in game
   
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
            PoolManager.Setup(entities[i],20);
        }
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
            
            yield return new WaitForSeconds(delayBetween);  
        }

    }
}
