using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    [Header("GENERAL INFO")]
    public float checkRadius; //radius of tower to check
    public LayerMask mask; //mask check

    [Header("PROJECTILE")]
    public ProjectileBase projectile;//projectile of tower

    private SphereCollider _sphereCol;//get sphere collider
    private List<EntitiesCore> enemy = new List<EntitiesCore>(); // list to store all enmey
    private EntitiesCore focusTarget; // target to focus to kill


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
        TowerShootBehaviour();
    }

    /// <summary>
    /// function of tower behaviour
    /// </summary>
    private void TowerShootBehaviour() 
    {
        //if enemy count is less than 0 then dont execute
        if (enemy.Count <= 0) return;

        //if focus target not exist
        if (focusTarget == null)
        {
            //get focus target
            focusTarget = enemy[0];
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        //check if object enter to collider has enemy tag
        if (other.CompareTag("Enemy")) 
        {
            //add that to enemy list
            enemy.Add(other.GetComponent<EntitiesCore>());
        }
    }

}
