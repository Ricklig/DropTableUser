using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPedestalCheckState : GuardSensState {
    private enum PedestalCheckState { TurningTowardArtefact, PedestalCheck, TurningTowardOldRotation, Finished }
    private PedestalCheckState pedestalCheckState = PedestalCheckState.PedestalCheck;
    private GameObject artefact = null;
    private GameObject artefact2 = null;
    public GuardPedestalCheckState(GuardAI guardAI, GameObject artefact, GameObject arfact2) : base(guardAI)
    {
        this.artefact = artefact;
        this.artefact2 = arfact2;
    }



    // Update is called once per frame
    public override GuardState DoAction(GuardState previousState)
    {
        guardAI.SetFlashLightPatrolMode();
        Debug.Log("GuardPedestalCheckState");
        base.DoAction(previousState);
        Debug.Log("Entering Pedestal State");
        if (pedestalCheckState == PedestalCheckState.TurningTowardArtefact)
        {
            return this;
        }
        else if (pedestalCheckState == PedestalCheckState.PedestalCheck)
        {
            if (artefact != null && artefact.tag == "Stolen")
            {
                GameManager.Instance.IncrementSeenStolenItem();
                artefact.tag = "SeenStolen";
                //augment alert mode
            }
            if (artefact2 != null && artefact2.tag == "Stolen")
            {
                GameManager.Instance.IncrementSeenStolenItem();
                artefact2.tag = "SeenStolen";
                //augment alert mode
            }
            // temp
            return new GuardPatrolState(guardAI);
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
