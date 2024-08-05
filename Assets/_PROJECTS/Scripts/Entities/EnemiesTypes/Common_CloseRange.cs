using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enetity.Common;
using DG.Tweening;
using QuocAnh.EntityAnimsData;
public class Common_CloseRange : EntitiesCore
{
    //create new common approach state
    private ApproachTarget_Common _commonApproach = new ApproachTarget_Common();
    protected override void Setup()
    {
        base.Setup();
        //change to that state
        StateChange(_commonApproach);
        //setup Walk Sequence
        _walkSequence = CommonEntityAnimation.commonWalk(_entityMesh.transform, -1, LoopType.Restart,0.1f);
        //setup attack sequence
        _attackSequence = CommonEntityAnimation.commonAttack(_entityMesh.transform, -1, LoopType.Restart,0.3f,entitiesData.attackRate);

        _walkSequence.Pause();
        _attackSequence.Pause();
        //play Walk sequence
        ChangeAnimation(_walkSequence);
    }

    public override void OnResetDefault()
    {
        //change to that state
        StateChange(_commonApproach);
        base.OnResetDefault();
    }
}
