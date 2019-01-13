﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPlayerInSightState : GuardSensState {
    private bool isChasing = false;
    private bool suprisedClipPlayed = false;
    float timer = 0;
    public GuardPlayerInSightState(GuardAI guardAI) : base(guardAI)
    {
    }

    public override GuardState DoAction()
    {
        guardAI.SetFlashLightAlertMode();
        Debug.Log("GuardPlayerInSightState");
        GuardState state = base.DoAction();
        if (state.GetType() == typeof(GuardPlayerInSightState))
        {
            if (!isChasing)
            {
                if (guardAI.MouvementAudioSource.isPlaying)
                {
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
                    isChasing = true;
                }
                timer += Time.deltaTime;
            }        
            //guard is suprised for a duration
            else
            {
                if (!guardAI.MouvementAudioSource.isPlaying)
                {
                    guardAI.MouvementAudioSource.clip = guardAI.RunningClip;
                    guardAI.MouvementAudioSource.Play();
                }
                timer = 0;
                guardAI.navMeshAgent.SetDestination(guardAI.lastSeenPlayerPosition);
            }
            return this;
        }
        else
        {
            return new GuardStateSearchPlayer(guardAI);
        }
    }
}
