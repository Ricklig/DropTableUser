using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPatrolState : GuardSensState {
    private Animator anim;
    public GuardPatrolState(GuardAI guardAI) : base(guardAI)
    {
        anim = guardAI.gameObject.GetComponentInChildren<Animator>();
        anim.SetBool("PlayerSeen", false);
        anim.SetBool("PlayerLost", false);
        anim.SetBool("MissingArt", false);
    }

    public override GuardState DoAction()
    {
        anim.SetBool("Walking", true);
        guardAI.SetFlashLightPatrolMode();
        if (!guardAI.MouvementAudioSource.isPlaying || guardAI.MouvementAudioSource.clip != guardAI.CurrentWalkingClip)
        {
            guardAI.MouvementAudioSource.clip = guardAI.CurrentWalkingClip;
            guardAI.MouvementAudioSource.Play();
        }
        //add sense here (vision detection and such)
        //we start patroling
        GuardState state = base.DoAction();
        if (state.GetType() != this.GetType())
        {
            return state;
        }
        Patrol();
        return this;
    }
    private void Patrol()
    {

        if (guardAI.currentNavNode == null)
        {
            return;
        }
        guardAI.navMeshAgent.SetDestination(guardAI.currentNavNode.transform.position);
        if (guardAI.navMeshAgent.remainingDistance <= 0.5)
        {
            INavNode buff = guardAI.currentNavNode;
            guardAI.currentNavNode = guardAI.currentNavNode.NextNavNode(guardAI.previousNavNode);
            guardAI.previousNavNode = buff;
            guardAI.navMeshAgent.SetDestination(guardAI.currentNavNode.transform.position);
        }
    }
    
    private INavNode FindClosestNavNode()
    {
        return null;
    }
}
