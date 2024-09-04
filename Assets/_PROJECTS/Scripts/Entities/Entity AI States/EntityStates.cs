using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Enetity.Common 
{
    public class ApproachTarget_Common : IEntityStates
    {
        public void Execute(EntitiesCore _entity)
        {
            //calculate distance between enemy and next path
            float _distance = Vector3.Distance(_entity.transform.position, _entity.path.vectorPath[_entity.currentPathIndex]);
            float _towerDistance = Vector3.Distance(_entity.transform.position, GameManager.Instance.tower.transform.position);
            //if distance is less than 0.2
            if (_distance <= 0.2f)
            {
                //if enetity next path wont exceed our path length
                if (_entity.currentPathIndex + 1 < _entity.path.vectorPath.Count)
                {
                    //increase it to move onto next part of the path
                    _entity.currentPathIndex++;
                }
               
            }
            else if(_towerDistance <= _entity.entitiesData.attackRange)
            {
                //create new attack behaviour
                AttackTarget_Common _attackTarget = new AttackTarget_Common();
                //pause walk sequence
                _entity._walkSequence.Pause();
                //change state to attack
                _entity.StateChange(_attackTarget);
            }

            //calculatee direction between path and entity to normalize our direction data
            //we normalize to reduce our data weight and reduce error
            Vector3 _dir = (_entity.path.vectorPath[_entity.currentPathIndex] - _entity.transform.position).normalized;

            //moving to target using lerp
            _entity.transform.position = Vector3.Lerp(_entity.transform.position, _dir, (_entity.entitiesData.speed*Time.deltaTime));

            //make AI look at it heading direction
            _entity._entityMesh.transform.LookAt(_dir);
        }
    }
    
    public class AttackTarget_Common : IEntityStates
    {
        private float lastShootTime = 0;

        public void Execute(EntitiesCore _entity)
        {
            Debug.Log("ATTACK STATE");
            //if (Time.time > lastShootTime + _entity.entitiesData.attackRate) 
            //{
            //    //========================================== ATTACk ================================================

            //    //========================================== ATTACk ================================================


            //    lastShootTime = Time.time;
            //}
            _entity.ChangeAnimation(_entity._attackSequence);
            _entity.StateChange(null);
        }
    }
}
