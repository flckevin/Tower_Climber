using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuocAnh.EntityAnimsData 
{
    public static class CommonEntityAnimation
    {
       public static Sequence commonWalk(Transform _target,int _loop,LoopType _loopType,float _animSpeed) => 
        DOTween.Sequence()
        .Append(_target.transform.DOMoveY(_target.transform.position.y + 0.5f, _animSpeed))
        .Append(_target.transform.DOMoveY(0, _animSpeed))
        .SetLoops(_loop, _loopType);

        public static Sequence commonAttack(Transform _target, int _loop, LoopType _loopType, float _animSpeed, float _delay) =>
            DOTween.Sequence()
                .Append(_target.DORotate(new Vector3(_target.localEulerAngles.x - 15f,
                                                    _target.localEulerAngles.y,
                                                    _target.localEulerAngles.z), _animSpeed))

                .Append(_target.DORotate(new Vector3(_target.localEulerAngles.x + 35f,
                                                    _target.localEulerAngles.y,
                                                    _target.localEulerAngles.z), _animSpeed))

                .Append(_target.DORotate(new Vector3(0,
                                                    _target.localEulerAngles.y,
                                                    _target.localEulerAngles.z), _animSpeed))
                .SetLoops(_loop, _loopType)

                .SetDelay(_delay);
    }

}

