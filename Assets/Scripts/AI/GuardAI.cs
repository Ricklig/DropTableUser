using BehaviourTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
enum AlertLevel { None, MinorAlert, Alert, SevereAlert }
public class GuardAI : MonoBehaviour {
    public SwapSightConeColor swapSightConeColor;
    public int VisionAngle;
    public int VisionRange;
    public INavNode InitialNavNode;
    public float SupriseDuration;
    public Vector3 lastSeenPlayerPosition;
    public Vector3 lastHeardPlayerPosition;
    public float AlertLevel2Coefficient = 1.25f;
    public float AlertLevel3Coefficient = 1.5f;
    public float RedAlertCoefficient = 1.75f;
    private float initialSpeed;
    private int initialVisionAngle;
    private int initialVisionRange;
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
    public AudioSource EffectAudioSource
    {
        get;
        set;
    }
    public AudioSource MouvementAudioSource
    {
        get;
        set;
    }
    public AudioSource WalkingAudioSource = new AudioSource();
    public AudioClip Level1WalkingClip;
    public AudioClip Level2WalkingClip;
    public AudioClip RunningClip;
    public AudioClip CurrentWalkingClip;

    public FieldOfView GraphicalConeOfView;
    // Use this for initialization
    void Start () {
        var aSources = GetComponents<AudioSource>();
        EffectAudioSource = aSources[0];
        MouvementAudioSource = aSources[1];

        guardState = new GuardPatrolState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentNavNode = InitialNavNode;
        GameManager.Instance.AlertManager.alertStatusChanged += alertChanged;

        initialSpeed = navMeshAgent.speed;
        initialVisionAngle = VisionAngle;
        initialVisionRange = VisionRange;
}

    private void alertChanged(AlertStatus alertStatus)
    {
        if (alertStatus == AlertStatus.level2)
        {
            CurrentWalkingClip = Level2WalkingClip;
            SupriseDuration = 0.75f;
            navMeshAgent.speed = AlertLevel2Coefficient * initialSpeed;
            VisionRange = (int)(AlertLevel2Coefficient * initialVisionRange);
            VisionAngle = (int)(AlertLevel2Coefficient * initialVisionAngle);
            //scale the guard
        }
        else if (alertStatus == AlertStatus.level3)
        {
            SupriseDuration = 0.5f;
            navMeshAgent.speed = AlertLevel3Coefficient * initialSpeed;
            VisionRange = (int)(AlertLevel3Coefficient * initialVisionRange);
            VisionAngle = (int)(AlertLevel3Coefficient * initialVisionAngle);
            //scale the guard
        }
        else if (alertStatus == AlertStatus.RedAlert)
        {
            navMeshAgent.speed = RedAlertCoefficient * initialSpeed;
            VisionRange = (int)(RedAlertCoefficient * initialVisionRange);
            VisionAngle = (int)(RedAlertCoefficient * initialVisionAngle);
            SupriseDuration = 0;
            //scale the guard
        }
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
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.KillPlayer();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.KillPlayer();
        }
    }
    public void SetFlashLightAlertMode()
    {
        swapSightConeColor.SetAlarmMaterial();
    }
    public void SetFlashLightPatrolMode()
    {
        swapSightConeColor.SetPatrolMaterial();
    }
    public void SetFlashLightSearchingMode()
    {
        swapSightConeColor.SetSearchingMaterial();
    }
}
