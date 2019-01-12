using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPedestalCheckState : GuardSensState {
    private enum PedestalCheckState { TurningTowardArtefact, TurningTowardOldRotation, Finished }
    private PedestalCheckState pedestalCheckState = PedestalCheckState.TurningTowardArtefact;

    public GuardPedestalCheckState(GuardAI guardAI) : base(guardAI)
    {

    }



    // Update is called once per frame
    public override GuardState DoAction()
    {
        base.DoAction();
        if (pedestalCheckState == PedestalCheckState.TurningTowardArtefact)
        {
            return this;
        }
        else if (pedestalCheckState == PedestalCheckState.TurningTowardOldRotation)
        {
            return this;
        }
        else
        {
            return new GuardPatrolState(guardAI);
        }
    }


}
