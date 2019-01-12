using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GuardState {
    public enum GuardStatesEnum { Patrol, Alert }
    protected GuardAI guardAI;
    public GuardState(GuardAI guardAI)
    {
        this.guardAI = guardAI;
    }
    public virtual GuardState DoAction()
    {
        return null;
    }
}
