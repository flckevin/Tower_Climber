using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enetity.Common;
public class Common_CloseRange : EntitiesCore
{
    protected override void Setup()
    {
        base.Setup();
        //create new common approach state
        ApproachTarget_Common _commonApproach = new ApproachTarget_Common();
        //change to that state
        StateChange(_commonApproach);
    }
}
