using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSensState : GuardState {
    public GuardSensState(GuardAI guardAI) : base(guardAI)
    {
    }

    public override GuardState DoAction()
    {
        /*check sens here*/
        return base.DoAction();
    }
}
