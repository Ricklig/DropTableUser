using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSensState : GuardState {
    public GuardSensState(GuardAI guardAI) : base(guardAI)
    {
    }

    public override GuardState DoAction(GuardState previousState)
    {
        Debug.Log("SensCheck");
        /*check sens here*/
        if (CheckVision())
        {
            if (previousState.GetType() == typeof(GuardPlayerInSightState) || previousState.GetType() == typeof(GuardStateSearchPlayer))
                return new GuardPlayerInSightState(guardAI);
            else
            {
                return new GuardSuprisedState(guardAI);
            }
        }
        return new GuardPatrolState(guardAI);

    }
    
    private bool CheckVision()
    {
        Vector3 spherePos = guardAI.transform.position + guardAI.transform.forward * 30;
        foreach (var collider in Physics.OverlapSphere(spherePos, 30))
        {
            if (collider.gameObject.tag == "Player")
            {
                Vector3 playerPos = new Vector3(collider.gameObject.transform.position.x, guardAI.transform.position.y, collider.gameObject.transform.position.z);
                //test if the player is hidden behind a wall or something
                Ray ray = new Ray(guardAI.transform.position, (playerPos - guardAI.transform.position).normalized);
                RaycastHit hitInfo;
                Physics.Raycast(ray, out hitInfo, guardAI.VisionRange + 1);
                Debug.DrawRay(guardAI.transform.position, (playerPos - guardAI.transform.position).normalized * (guardAI.VisionRange + 1), Color.red, 0.0f);
                if (hitInfo.collider && hitInfo.collider.gameObject.gameObject.tag != "Player")

                    return false;       
                if (IsInConeOfSight(collider.gameObject.transform.position))
                {
                    guardAI.lastSeenPlayerPosition = collider.gameObject.transform.position;
                    return true;
                }
            }
        }
        return false;
    }
    private bool IsInConeOfSight(Vector3 pos)
    {
        Vector3 objectPos = new Vector3(pos.x, 0, pos.z);
        Vector3 aiPos = new Vector3(guardAI.transform.position.x, 0, guardAI.transform.position.z);
        Vector3 directionVector = (objectPos - aiPos).normalized;
        //we get the axe of vision
        Vector3 forward = guardAI.transform.forward;
        //float testBuff = Mathf.Cos((guardAI.VisionAngle / 2) * Mathf.Deg2Rad);
        Quaternion QuaternionRotation = Quaternion.AngleAxis(guardAI.VisionAngle / 2, Vector3.up);
        Vector3 visionAxe = QuaternionRotation * guardAI.transform.forward;
        Vector3 visionAxe2 = Quaternion.AngleAxis(-guardAI.VisionAngle/2, Vector3.up) * guardAI.transform.forward;
        //Vector3 visionAxe = (forward / testBuff).normalized;
        Debug.DrawRay(aiPos, visionAxe * guardAI.VisionRange, Color.white, 0.0f);
        Debug.DrawRay(aiPos, visionAxe2 * guardAI.VisionRange, Color.white, 0.0f);
        if (Mathf.Abs((objectPos - aiPos).magnitude) > guardAI.VisionRange)
        {
            return false;
        }
        float angleTest = Mathf.Cos(guardAI.VisionAngle * Mathf.Deg2Rad);
        float buff1 = Vector3.Dot(directionVector, visionAxe);
        float buff2 = Vector3.Dot(directionVector, visionAxe2);
        if (buff1 > angleTest && buff2 > angleTest)
        {
            //Debug.Log("Inside vision Cone");
            return true;
        }
        return false;
    }
}
