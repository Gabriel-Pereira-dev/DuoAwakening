using System.Collections;
using System.Collections.Generic;
using Behaviors.LichBoss.States;
using EventArgs;
using StateMachineNamespace;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviors.LichBoss
{
    public class LichBossController : MonoBehaviour
    {
        //Helper
        [HideInInspector] public LichBossHelper helper;
        //Components
        [HideInInspector] public NavMeshAgent thisAgent;
        [HideInInspector] public LifeScript thisLife;
        [HideInInspector] public Animator thisAnimator;
        public string currentState;


        // StateMachine
        [HideInInspector] public StateMachine stateMachine;
        [HideInInspector] public Idle idleState;
        [HideInInspector] public AttackNormal attackNormalState;
        [HideInInspector] public AttackSuper attackSuperState;
        [HideInInspector] public AttackRitual attackRitualState;
        [HideInInspector] public Follow followState;
        [HideInInspector] public Hurt hurtState;
        [HideInInspector] public Dead deadState;

        // Controller Attr

        [Header("General")]
        public float lowHealthThreshold = 0.4f;
        [Header("Idle")]
        public float idleDuration = 0.3f;
        [Header("Follow")]
        public float ceaseFollowInterval = 4f;
        [Header("Attack Normal")]
        public float attackNormalDelay = 0f;
        public float attackNormalDuration = 0f;

        [Header("Attack Ritual")]
        public float distanceToRitual = 2.5f;
        public float attackRitualDelay = 0f;
        public float attackRitualDuration = 0f;
        [Header("Attack Super")]

        public float attackSuperDelay = 0f;
        public float attackSuperDuration = 1f;
        public int attackSuperCount = 5;


        public int attackDamage = 1;

        [Header("Hurt")]
        public float hurtDuration = 0.5f;


        void Awake()
        {
            // Get Components
            thisAgent = GetComponent<NavMeshAgent>();
            thisLife = GetComponent<LifeScript>();
            thisAnimator = GetComponent<Animator>();
            // Helper
            helper = new LichBossHelper(this);
        }

        void Start()
        {

            //StateMachine
            stateMachine = new StateMachine();
            idleState = new Idle(this);
            attackNormalState = new AttackNormal(this);
            attackSuperState = new AttackSuper(this);
            attackRitualState = new AttackRitual(this);
            followState = new Follow(this);
            hurtState = new Hurt(this);
            deadState = new Dead(this);
            stateMachine.ChangeState(idleState);

            //Register listeners
            thisLife.OnDamage += OnDamage;
        }

        private void OnDamage(object sender, DamageEventArgs args)
        {
            Debug.Log("Boss recebeu dano");
            stateMachine.ChangeState(hurtState);
        }

        // Update is called once per frame
        void Update()
        {
            stateMachine.Update();
            currentState = stateMachine.currentStateName;

            //Update anim velocity
            // var velocityRate = thisAgent.velocity.magnitude / thisAgent.speed;
            // thisAnimator.SetFloat("fVelocity", velocityRate);
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
}