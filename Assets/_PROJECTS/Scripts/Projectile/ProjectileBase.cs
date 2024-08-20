using DG.Tweening;
using PrimeTween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public Action _callBackOnHit;               // call back function when arrow hit it target
    public ProjectileData _projectileData;      // projectile data

    /// <summary>
    /// shoot function to nemey
    /// </summary>
    /// <param name="_target"> target </param>
    /// <param name="_speed"> travel speed </param>
    public void Shoot(Vector3 _from, Transform _target,float _speed) 
    {
        //randomize target height
        float _randedY = UnityEngine.Random.Range(0.75f, 1.7f);
        //calculate correct target position
        Vector3 _targetPos = new Vector3(_target.position.x, _randedY, _target.position.z);
        //move target to desiered position
        this.transform.position = _from;
        //look at target
        this.transform.LookAt(_target.transform);

        //move arrow to target
        this.transform.DOMove(_targetPos, _speed).OnComplete(() =>
        {
            // when it finished to target

            //DOTween.Sequence()
            //.SetDelay(1f)
            //.Append(this.transform.DOScale(0, 0.3f));

            //deactivate objcet
            this.gameObject.SetActive(false);
            //call callback function
            _callBackOnHit();
        });
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
