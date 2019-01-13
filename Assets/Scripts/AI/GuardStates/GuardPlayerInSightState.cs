using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPlayerInSightState : GuardSensState {
    private bool suprisedClipPlayed = false;
    float timer = 0;
	private Animator anim;
    public GuardPlayerInSightState(GuardAI guardAI) : base(guardAI)
    {
		anim = guardAI.gameObject.GetComponentInChildren<Animator>();
	    anim.SetBool("PlayerSeen", false);
	    anim.SetBool("PlayerLost", false);
	    anim.SetBool("MissingArt", false);

	}
	
	public override GuardState DoAction(GuardState previousState)
    {
        guardAI.SetFlashLightAlertMode();

        Debug.Log("GuardPlayerInSightState");
        GuardState state = base.DoAction(previousState);
        if (state.GetType() == typeof(GuardPlayerInSightState))
        {   
            if (!guardAI.MouvementAudioSource.isPlaying)
            {
                guardAI.MouvementAudioSource.clip = guardAI.RunningClip;
                guardAI.MouvementAudioSource.Play();
            }
            anim.SetBool("PlayerSeen", true);
            anim.SetBool("Walking", false);
            timer = 0;
            guardAI.navMeshAgent.SetDestination(guardAI.lastSeenPlayerPosition);
            return this;
        }
        else
        {
            return new GuardStateSearchPlayer(guardAI);
        }
    }
}
