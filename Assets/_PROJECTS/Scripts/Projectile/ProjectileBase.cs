using PrimeTween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    private Action<EntitiesCore> _callBackOnHit; // call back function when arrow hit it target
    public ProjectileData _projectileData; // projectile data
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// shoot function to nemey
    /// </summary>
    /// <param name="_target"> target </param>
    /// <param name="_speed"> travel speed </param>
    public void Shoot(Vector3 _from, Transform _target,float _speed) 
    {
        //move target to desiered position
        this.transform.position = _from;
        //look at target
        this.transform.LookAt(_target.transform);
        //move arrow position to enemy
        Tween.Position(this.transform, _target.transform.position, duration: _speed).OnComplete(() => 
        {
            //wait for 1 sec
            Tween.Delay(1f);
            //scale down
            Tween.Scale(this.transform, endValue: 0, duration: 0.3f);
            //wait for few sec
            Tween.Delay(0.4f);
            //deactivate this arrow
            this.gameObject.SetActive(false);
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        //if other has enemy tag
        if (other.CompareTag("Enemy")) 
        {
            //get enetity compoenent
            EntitiesCore _entity = other.GetComponent<EntitiesCore>();
            //deal damage to it
            DealDamage(_entity);
            
        }
    }

    /// <summary>
    /// base function to deal damage to enemy
    /// </summary>
    /// <param name="_enetity"> target </param>
    protected virtual void DealDamage(EntitiesCore _enetity){ Debug.Log("HIT TO ENEMY : " + _enetity.name); }

    /// <summary>
    /// function to reset all arrow value back to default
    /// </summary>
    public virtual void ResetDefault() 
    { 
        //reset back to 1 value in vector 3
        this.transform.localScale = Vector3.one;
    }
}
