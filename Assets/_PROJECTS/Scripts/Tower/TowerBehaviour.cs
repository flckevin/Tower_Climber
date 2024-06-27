using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    [Header("GENERAL INFO")]
    public float checkRadius; //radius of tower to check
    public LayerMask mask; //mask check
    private List<GameObject> enemy = new List<GameObject>(); // list to store all enmey
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
       Collider[] hitColliders = Physics.OverlapSphere(this.gameObject.transform.position, 
                                                        checkRadius,
                                                        mask);
        
        if(hitColliders.Length > 0)
        {
            
        }
    }
}
