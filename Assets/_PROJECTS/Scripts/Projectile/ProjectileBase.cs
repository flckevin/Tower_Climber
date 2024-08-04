using DG.Tweening;
using PrimeTween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public Action _callBackOnHit; // call back function when arrow hit it target
    public ProjectileData _projectileData; // projectile data
    // Start is called before the first frame update
   

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
        //move arrow position to enemy
        //Tween.Position(this.transform, _targetPos, duration: _speed).OnComplete(() => 
        //{
        //    //wait for 1 sec
        //    Tween.Delay(1f);
        //    //scale down
        //    Tween.Scale(this.transform, endValue: 0, duration: 0.3f);
        //    //wait for few sec
        //    Tween.Delay(0.4f);
        //    //deactivate this arrow
        //    this.gameObject.SetActive(false);
        //    //call back on hit
        //    _callBackOnHit();
        //});

        this.transform.DOMove(_targetPos, _speed).OnComplete(() =>
        {
            DOTween.Sequence()
            .SetDelay(1f)
            .Append(this.transform.DOScale(0, 0.3f));
            this.gameObject.SetActive(false);
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
