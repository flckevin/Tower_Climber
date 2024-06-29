using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    [Header("GENERAL INFO")]
    public float checkRadius; //radius of tower to check
    public LayerMask mask; //mask check

    private List<EntitiesCore> enemy = new List<EntitiesCore>(); // list to store all enmey
    private EntitiesCore focusTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        TowerShootBehaviour();
    }

    private void TowerShootBehaviour() 
    {
        //if enemy count is less than 0 then dont execute
        if (enemy.Count < 0) return;

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
