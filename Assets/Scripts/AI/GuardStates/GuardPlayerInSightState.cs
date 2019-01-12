using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPlayerInSightState : GuardSensState {
    private bool isChasing = false;
    public GuardPlayerInSightState(GuardAI guardAI) : base(guardAI)
    {
    }

    public override GuardState DoAction()
    {
        GuardState state = base.DoAction();
        if (state.GetType() == typeof(GuardPlayerInSightState))
        {
            if (isChasing)
                guardAI.navMeshAgent.SetDestination(guardAI.lastSeenPlayerPosition);
            else
            {
                //guard is suprised for a duration
            }
            return this;
        }
        else
        {
            return state;
        }
    }
}
