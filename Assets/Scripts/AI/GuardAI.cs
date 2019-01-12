using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
enum AlertLevel { None, MinorAlert, Alert, SevereAlert }
public class GuardAI : MonoBehaviour {
    public INavNode InitialNavNode;

    private GuardState guardState;
    private Task behaviourTree;
    private BlackBoard guardBlackBoard;
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
    // Use this for initialization
    void Start () {
        guardState = new GuardPatrolState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentNavNode = InitialNavNode;
        //navMeshAgent.SetDestination(currentNavNode.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        guardState.DoAction();
    }
    private void OnTriggerEnter(Collider other)
    {
        /*if (other.tag == "PedestalCheck" && GuardState)
        {
            Quaternion toRotation = Quaternion.FromToRotation(transform.position, other.gameObject.GetComponent<PedestalNavNode>().PedestalTransform.position);
            // do some logic to check the artefact if not in the alert level
        }*/
    } 

}
