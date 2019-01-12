using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPlayerInSightState : GuardSensState {
    private bool isChasing = false;
    float timer = 0;
    public GuardPlayerInSightState(GuardAI guardAI) : base(guardAI)
    {
    }

    public override GuardState DoAction()
    {
        GuardState state = base.DoAction();
        if (state.GetType() == typeof(GuardPlayerInSightState))
        {
            if (!isChasing)
            {
                //put exclamation mark
                if (timer >= guardAI.SupriseDuration)
                {
                    isChasing = true;
                }
                timer += Time.deltaTime;
            }        
            //guard is suprised for a duration
            else
            {
                timer = 0;
                guardAI.navMeshAgent.SetDestination(guardAI.lastSeenPlayerPosition);
            }
            return this;
        }
        else
        {
            return state;
        }
    }
}
