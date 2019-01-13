using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSuprisedState : GuardSensState {
    private bool suprisedClipPlayed = false;
    float timer = 0;
    private Animator anim;
    public GuardSuprisedState(GuardAI guardAI) : base(guardAI)
    {
        anim = guardAI.gameObject.GetComponentInChildren<Animator>();
        anim.SetBool("PlayerSeen", false);
        anim.SetBool("PlayerLost", false);
        anim.SetBool("MissingArt", false);
        anim.SetBool("Walking", true);
    }

    public override GuardState DoAction(GuardState previousState)
    {
        guardAI.SetFlashLightSearchingMode();
        base.DoAction(previousState);
        if (guardAI.MouvementAudioSource.isPlaying)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("PlayerSeen", false);
            anim.SetBool("PlayerLost", true);
            anim.SetBool("MissingArt", false);
            guardAI.MouvementAudioSource.Stop();
        }
        guardAI.navMeshAgent.SetDestination(guardAI.transform.position);
        if (!suprisedClipPlayed)
        {
            guardAI.EffectAudioSource.PlayOneShot(guardAI.EffectAudioSource.clip);
            suprisedClipPlayed = true;
        }

        //put exclamation mark
        if (timer >= guardAI.SupriseDuration)
        {
            timer = 0;
            return new GuardPlayerInSightState(guardAI);
        }
        timer += Time.deltaTime;
        return this;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
