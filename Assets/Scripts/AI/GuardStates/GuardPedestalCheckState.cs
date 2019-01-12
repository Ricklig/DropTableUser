using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPedestalCheckState : GuardSensState {
    private enum PedestalCheckState { TurningTowardArtefact, PedestalCheck, TurningTowardOldRotation, Finished }
    private PedestalCheckState pedestalCheckState = PedestalCheckState.TurningTowardArtefact;
    private GameObject artefact;
    public GuardPedestalCheckState(GuardAI guardAI, GameObject artefact) : base(guardAI)
    {
        this.artefact = artefact;
    }



    // Update is called once per frame
    public override GuardState DoAction()
    {
        base.DoAction();
        Debug.Log("Entering Pedestal State");
        if (pedestalCheckState == PedestalCheckState.TurningTowardArtefact)
        {
            return this;
        }
        else if (pedestalCheckState == PedestalCheckState.PedestalCheck)
        {
            if (artefact.tag == "stolen")
            {
                GameManager.Instance.IncrementSeenStolenItem();
                //augment alert mode
            }
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
