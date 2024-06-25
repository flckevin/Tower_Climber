using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbuilderScaleTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.localScale = new Vector3(this.transform.localScale.x-2,
                                                            this.transform.localScale.y,
                                                            this.transform.localScale.z-2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
