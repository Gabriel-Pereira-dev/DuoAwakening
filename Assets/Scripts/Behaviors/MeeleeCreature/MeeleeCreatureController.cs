using System.Collections;
using System.Collections.Generic;
using Behaviors.MeeleeCreature.States;
using EventArgs;
using StateMachineNamespace;
using UnityEngine;
using UnityEngine.AI;

public class MeeleeCreatureController : MonoBehaviour
{
    [HideInInspector] public MeeleeCreatureHelper helper;
    [HideInInspector] public NavMeshAgent thisAgent;
    [HideInInspector] public LifeScript thisLife;
    [HideInInspector] public Animator thisAnimator;
    [HideInInspector] public Collider thisCollider;
    [HideInInspector] public Rigidbody thisRigidbody;
    public string currentState;


    // StateMachine
    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Attack attackState;
    [HideInInspector] public Follow followState;
    [HideInInspector] public Hurt hurtState;
    [HideInInspector] public Dead deadState;

    // Controller Attr

    [Header("General")]
    public float targetSearchInteval = 1f;
    [Header("Idle")]
    public float searchRadius = 5f;
    [Header("Follow")]
    public float ceaseFollowInterval = 4f;
    [Header("Attack")]
    public float distanceToAttack = 1f;
    public float attackRadius = 1.5f;
    public float attackSphereRadius = 1.5f;
    public float damageDelay = 0f;
    public float attackDurantion = 1f;
    public int attackDamage = 1;

    [Header("Hurt")]
    public float hurtDuration = 1f;
    [Header("Dead")]
    public float destroyIfFar = 1f;

    [Header("Effect")] public GameObject knockOutEffect;


    void Awake()
    {
        // Get Components
        thisAgent = GetComponent<NavMeshAgent>();
        thisLife = GetComponent<LifeScript>();
        thisAnimator = GetComponent<Animator>();
        thisCollider = GetComponent<Collider>();
        thisRigidbody = GetComponent<Rigidbody>();
        // Helper
        helper = new MeeleeCreatureHelper(this);
    }

    void Start()
    {

        //StateMachine
        stateMachine = new StateMachine();
        idleState = new Idle(this);
        attackState = new Attack(this);
        followState = new Follow(this);
        hurtState = new Hurt(this);
        deadState = new Dead(this);
        stateMachine.ChangeState(idleState);

        //Register listeners
        thisLife.OnDamage += OnDamage;
    }
    private void OnDamage(object sender, DamageEventArgs args)
    {
        stateMachine.ChangeState(hurtState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        currentState = stateMachine.currentStateName;

        //Update anim velocity
        var velocityRate = thisAgent.velocity.magnitude / thisAgent.speed;
        thisAnimator.SetFloat("fVelocity", velocityRate);
    }

    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    void LateUpdate()
    {
        stateMachine.LateUpdate();
    }
}
