using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationController : MonoBehaviour
{

    
    public Transform cineCam;
    public Transform target;

    public float offsetZ;

    private Vector3 _previousPosition;
    private Camera _cam;

    void Start()
    {
        _cam = Camera.main;
        cineCam.transform.position = target.transform.position;
        cineCam.transform.Translate(new Vector3(0,0,offsetZ));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch _touch = Input.GetTouch(0);

            if(_touch.phase == TouchPhase.Began)
            {
                _previousPosition = _cam.ScreenToViewportPoint(_touch.position);
            }

            if(_touch.phase == TouchPhase.Moved)
            {
                //Debug.Log("MOVING");
                Vector3 _direction = _previousPosition - _cam.ScreenToViewportPoint(_touch.position);
                
                cineCam.transform.position = target.transform.position;

                //cineCam.transform.Rotate(new Vector3(1,0,0),_direction.y*180);
                cineCam.transform.Rotate(new Vector3(0,1,0),-_direction.x*180,Space.World);
                // cineCam.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                //                                                 Quaternion.Euler(this.transform.position.x,_touch.position.x,offsetZ),
                //                                                 1.5f);
                cineCam.transform.Translate(new Vector3(0,0,offsetZ));
                _previousPosition = _cam.ScreenToViewportPoint(_touch.position);
            }
        }
    }
}
