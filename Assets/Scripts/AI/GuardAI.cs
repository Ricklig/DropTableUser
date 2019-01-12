using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
enum AlertLevel { None, MinorAlert, Alert, SevereAlert }
public class GuardAI : MonoBehaviour {
    public int VisionAngle
    {
        get;
        set;
    }
    public int VisionRange
    {
        get;
        set;
    }
    public INavNode InitialNavNode;
    private GuardState guardState;
    private Task behaviourTree;
    public INavNode currentNavNode
    {
        get;
        set;
    }
    public INavNode previousNavNode
    {
        get;
        set;
    }
    public NavMeshAgent navMeshAgent
    {
        get;
        set;
    }
    public Vector3 lastSeenPlayerPosition;
    public Vector3 lastHeardPlayerPosition;
    // Use this for initialization
    void Start () {
        guardState = new GuardPatrolState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentNavNode = InitialNavNode;
        //navMeshAgent.SetDestination(currentNavNode.transform.position);
        VisionAngle = 30;
        VisionRange = 5;

    }
	
	// Update is called once per frame
	void Update () {
        guardState = guardState.DoAction();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PedestalCheck" && guardState.GetType() == typeof(GuardPatrolState))
        {
            guardState = new GuardPedestalCheckState(this, other.GetComponent<PedestalNavNode>().PedestalTransform.gameObject);
            // do some logic to check the artefact if not in the alert level
        }
    } 

}
