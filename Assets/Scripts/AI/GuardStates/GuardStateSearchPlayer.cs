using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardStateSearchPlayer : GuardSensState
{
    enum SearchPlayerStates { MovingToLastKnownLocation, RoomSwipe, RoomSecondSwipe, MovingToRandomNearbyNode, Finished}
    SearchPlayerStates searchPlayerState = SearchPlayerStates.MovingToLastKnownLocation;
    Quaternion lerpFinalRotation;
    Quaternion lerpInitialRotation;
    float lerpSpeed = 0.1f;
    bool isSwipping = false;
    float timeToSwipe = 0;
    float swipeTime = 0;
    public GuardStateSearchPlayer(GuardAI guardAI) : base(guardAI)
    {
    }
    

    public override GuardState DoAction()
    {
        Debug.Log("GuardStateSearchPlayer");
        GuardState state = base.DoAction();
        if (state.GetType() != typeof(GuardPlayerInSightState))
        {
            if (searchPlayerState == SearchPlayerStates.MovingToLastKnownLocation)
            {
                guardAI.navMeshAgent.SetDestination(guardAI.lastSeenPlayerPosition);
                if (guardAI.navMeshAgent.remainingDistance < 0.5f)
                {
                    searchPlayerState = SearchPlayerStates.RoomSwipe;
                }
                return this;
            }
            else if(searchPlayerState == SearchPlayerStates.RoomSwipe)
            {
                if (!isSwipping)
                {
                    swipeTime = 0;
                    lerpFinalRotation = Quaternion.AngleAxis(90, Vector3.up) * guardAI.transform.rotation;
                    lerpInitialRotation = guardAI.transform.rotation;
                    isSwipping = true;
                    timeToSwipe = 90 / guardAI.navMeshAgent.angularSpeed;
                }
                swipeTime += Time.deltaTime;
                guardAI.transform.rotation = Quaternion.Lerp(lerpInitialRotation, lerpFinalRotation, swipeTime / timeToSwipe);
                if (swipeTime > timeToSwipe)
                {
                    searchPlayerState = SearchPlayerStates.RoomSecondSwipe;
                    isSwipping = false;
                }
                return this;
            }
            else if (searchPlayerState == SearchPlayerStates.RoomSecondSwipe)
            {
                if (!isSwipping)
                {
                    swipeTime = 0;
                    lerpFinalRotation = Quaternion.AngleAxis(-180, Vector3.up) * guardAI.transform.rotation;
                    lerpInitialRotation = guardAI.transform.rotation;
                    isSwipping = true;
                    timeToSwipe = 180 / guardAI.navMeshAgent.angularSpeed;
                }
                swipeTime += Time.deltaTime;
                guardAI.transform.rotation = Quaternion.Lerp(lerpInitialRotation, lerpFinalRotation, swipeTime / timeToSwipe);
                if (swipeTime > timeToSwipe)
                {
                    searchPlayerState = SearchPlayerStates.RoomSecondSwipe;
                    isSwipping = false;
                }
                return this;
            }
            else if (searchPlayerState == SearchPlayerStates.MovingToRandomNearbyNode)
            {
                var colliders = Physics.OverlapSphere(guardAI.transform.position, 20);
                foreach (var collider in colliders)
                {
                    if (collider.tag == "NavNode")
                    {
                        guardAI.currentNavNode = collider.gameObject.GetComponent<INavNode>();
                        return new GuardPatrolState(guardAI);
                    }
                }
                guardAI.currentNavNode = guardAI.InitialNavNode;
                return new GuardPatrolState(guardAI);
            }
            else
            {
                return new GuardPatrolState(guardAI);
            }
        }
        else
        {
            return state;
        }
    }
}
