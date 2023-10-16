using System;
using System.Collections;
using System.Collections.Generic;
using EventArgs;
using Player.States;
using StateMachineNamespace;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // State Machine
    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Walking walkingState;
    [HideInInspector] public Jump jumpState;
    [HideInInspector] public Attack attackState;
    [HideInInspector] public Defend defendState;
    [HideInInspector] public Dead deadState;
    [HideInInspector] public Hurt hurtState;

    // Components

    [HideInInspector] public bool isGrounded;
    [HideInInspector] public Rigidbody thisRigidbody;
    [HideInInspector] public Collider thisCollider;
    [HideInInspector] public AudioSource thisAudioSource;
    [HideInInspector] public Animator thisAnimator;
    [HideInInspector] public LifeScript thisLife;

    // Movement
    [Header("Movement")]
    public float movementSpeed = 10f;
    public float maxSpeed = 10f;
    [HideInInspector] public Vector2 movementVector;

    // Jump
    [Header("Jump")]
    public float jumpPower = 10f;
    [Range(0f, 1f)] public float jumpMovementFactor = 1f;
    [HideInInspector] public bool hasJumpInput;
    public AudioClip jumpSound;

    // Slope
    [Header("Slope")]
    public float maxSlopeAngle = 45f;
    [HideInInspector] public bool isOnSlope;
    [HideInInspector] public Vector3 slopeNormal;
    // Attack
    [Header("Attack")]
    public int attackStages;
    public List<float> attackStageDurations = new List<float>();
    public List<float> attackStageMaxInterval = new List<float>();
    public List<float> attackStageImpulse = new List<float>();
    public GameObject swordHitbox;
    public float swordKnockbackImpulse = 10f;
    public List<int> attackDamageByStage;

    [Header("Defend")]
    public GameObject shieldHitbox;
    public float shieldKnockbackImpulse = 10f;
    public bool hasDefenseInput;

    [Header("Effects")] public GameObject hitEffect;

    [Header("Hurt")] public float hurtDuration = 0.5f;

    void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        thisCollider = GetComponent<Collider>();
        thisAnimator = GetComponent<Animator>();
        thisAudioSource = GetComponent<AudioSource>();
        thisLife = GetComponent<LifeScript>();
        if (thisLife != null)
        {
            thisLife.OnDamage += OnDamage;
            thisLife.OnHeal += OnHeal;
            thisLife.canInflictDamageDelegate += CanInflictDamage;
        }
    }

   void Start()
    {
        // State Machie and its states
        stateMachine = new StateMachine();
        idleState = new Idle(this);
        walkingState = new Walking(this);
        jumpState = new Jump(this);
        attackState = new Attack(this);
        defendState = new Defend(this);
        deadState = new Dead(this);
        hurtState = new Hurt(this);
        stateMachine.ChangeState(idleState);

        // Toggle hitbox
        swordHitbox.SetActive(false);
        shieldHitbox.SetActive(false);
        
        // Set max health in UI
        var gameplayUI = GameManager.Instance.gameplayUI;
        gameplayUI.playerHealthBar.SetMaxHealth(thisLife.maxHealth);
    }

    void Update()
    {
        // Create input Vector
        bool isUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool isDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool isLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool isRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        float inputX = isRight ? 1 : isLeft ? -1 : 0;
        float inputY = isUp ? 1 : isDown ? -1 : 0;
        movementVector = new Vector3(inputX, inputY);
        hasJumpInput = Input.GetKey(KeyCode.Space);

        // Check defense input
        hasDefenseInput = Input.GetKey(KeyCode.X);

        //P Passar a velocidade de 0 a 1 pro Animator Controller
        float velocity = thisRigidbody.velocity.magnitude;
        float velocityRate = velocity / maxSpeed;
        thisAnimator.SetFloat("fVelocity", velocityRate);
        // Detect Ground
        DetectGround();
        DetectSlope();

        var bossBattleHandler = GameManager.Instance.bossBattleHandler;
        var isInCutscene = bossBattleHandler.IsInCutscene();
        if (isInCutscene && stateMachine.currentStateName != idleState.name)
        {
            stateMachine.ChangeState(idleState);
        }

        stateMachine.Update();
    }


    void LateUpdate()
    {
        stateMachine.LateUpdate();
    }

    void FixedUpdate()
    {
        // Apply Gravity
        Vector3 gravityForce = Physics.gravity * (isOnSlope ? 0.25f : 1);
        thisRigidbody.AddForce(gravityForce, ForceMode.Acceleration);
        // Limit Speed
        LimitSpeed();

        // State
        stateMachine.FixedUpdate();
    }
    
    private void OnDamage(object sender, DamageEventArgs args)
    {
        var gameplayUI = GameManager.Instance.gameplayUI;
        gameplayUI.playerHealthBar.SetHealth(thisLife.health);
        stateMachine.ChangeState(hurtState);
    }
    
    private void OnHeal(object sender, HealEventArgs args)
    {
        var gameplayUI = GameManager.Instance.gameplayUI;
        gameplayUI.playerHealthBar.SetHealth(thisLife.health);
    }

    public void OnSwordCollisionEnter(Collider otherCollider)
    {
        var otherObject = otherCollider.gameObject;
        var otherRigidbody = otherObject.GetComponent<Rigidbody>();
        var otherLife = otherObject.GetComponent<LifeScript>();

        int bit = 1 << otherObject.layer;
        int mask = LayerMask.GetMask("Target", "Creatures");
        bool isTargetOrCreature = (bit & mask) == bit;

        if (isTargetOrCreature)
        {
            // Life

            if (otherLife != null)
            {
                var wasVulnerable = otherLife.isVulnerable;
                var damage = attackDamageByStage[attackState.stage - 1];
                otherLife.InflictDamage(gameObject, damage);

                if (wasVulnerable)
                {
                    Debug.Log(otherObject.name+" - Ataque Espada Efeito Vulneravel");
                    var hitPosition = otherCollider.ClosestPointOnBounds(swordHitbox.transform.position);
                    var hitRotation = hitEffect.transform.rotation;
                    Instantiate(hitEffect, hitPosition, hitRotation);
                }
            }else if (hitEffect != null)
            {
                Debug.Log(otherObject.name+" - Ataque Espada Efeito");
                var hitPosition = otherCollider.ClosestPointOnBounds(swordHitbox.transform.position);
                var hitRotation = hitEffect.transform.rotation;
                Instantiate(hitEffect, hitPosition, hitRotation);
            }
            
            //Knockback
            if (otherRigidbody != null)
            {
                var positionDif = otherObject.transform.position - gameObject.transform.position;
                var impulseVector = new Vector3(positionDif.normalized.x, 0, positionDif.normalized.z);
                impulseVector *= swordKnockbackImpulse;
                otherRigidbody.AddForce(impulseVector, ForceMode.Impulse);
            }

            
        }
    }

    public void OnShieldCollisionEnter(Collider other)
    {
        var otherObject = other.gameObject;
        var otherRigidbody = otherObject.GetComponent<Rigidbody>();
        var isTarget = true;
        if (isTarget && otherRigidbody != null)
        {
            var positionDif = otherObject.transform.position - gameObject.transform.position;
            var impulseVector = new Vector3(positionDif.normalized.x, 0, positionDif.normalized.z);
            impulseVector *= shieldKnockbackImpulse;
            otherRigidbody.AddForce(impulseVector, ForceMode.Impulse);
        }
    }

    private bool CanInflictDamage(GameObject attacker, int damage)
    {
        bool isDefending = stateMachine.currentStateName == defendState.name;
        if (isDefending)
        {
            Vector3 playerDirection = transform.TransformDirection(Vector3.forward);
            Vector3 attackDirection = (transform.position - attacker.transform.position).normalized;
            float dot = Vector3.Dot(playerDirection, attackDirection);
            Debug.Log("Dot - " + dot);
            if (dot < - -0.25f)
            {
                return false;
            }
            ;
        }

        return true;
    }

    public Quaternion GetFoward()
    {
        Camera camera = Camera.main;
        float eulerY = camera.transform.eulerAngles.y;
        return Quaternion.Euler(0, eulerY, 0);
    }

    public void RotateBodyToFaceInput(float alpha = 0.225f)
    {
        if (movementVector.IsZero()) return;
        // Calculate Rotation
        Camera camera = Camera.main;
        Vector3 inputVector = new Vector3(movementVector.x, 0, movementVector.y);
        Quaternion q1 = Quaternion.LookRotation(inputVector, Vector3.up);
        Quaternion q2 = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
        Quaternion toRotation = q1 * q2;
        Quaternion newRotation = Quaternion.LerpUnclamped(transform.rotation, toRotation, alpha);

        // Apply rotation
        thisRigidbody.MoveRotation(newRotation);
    }

    public bool AttemptToAttack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            bool isAttacking = stateMachine.currentStateName == attackState.name;
            bool canAttack = !isAttacking || attackState.CanSwitchStages();
            if (canAttack)
            {
                var attackStage = isAttacking ? attackState.stage + 1 : 1;
                attackState.stage = attackStage;
                stateMachine.ChangeState(attackState);
                return true;
            }
        }
        return false;
    }

    private void DetectGround()
    {
        // Reset flag
        isGrounded = false;

        // Detect ground
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;
        float maxDistance = 0.1f;
        LayerMask groundLayer = GameManager.Instance.groundLayer;
        if (Physics.Raycast(origin, direction, maxDistance, groundLayer))
        {
            isGrounded = true;
        }
    }

    private void DetectSlope()
    {
        //ResetSlope
        isOnSlope = false;
        slopeNormal = Vector3.zero;

        // Detect on slope
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;
        float maxDistance = 0.2f;
        if (Physics.Raycast(origin, direction, out var slopeHitInfo, maxDistance))
        {

            float angle = Vector3.Angle(Vector3.up, slopeHitInfo.normal);
            isOnSlope = angle < maxSlopeAngle && angle != 0;
            slopeNormal = isOnSlope ? slopeHitInfo.normal : Vector3.zero;

        }
    }

    private void LimitSpeed()
    {
        Vector3 flatVelocity = new Vector3(thisRigidbody.velocity.x, 0, thisRigidbody.velocity.z);
        if (flatVelocity.magnitude > maxSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * maxSpeed;
            thisRigidbody.velocity = new Vector3(limitedVelocity.x, thisRigidbody.velocity.y, limitedVelocity.z);
        }
    }
    void OnDrawGizmos()
    {
        if (thisCollider != null)
        {
            // Get values
            Vector3 origin = transform.position;
            Vector3 direction = Vector3.down;
            Bounds bounds = thisCollider.bounds;
            float radius = bounds.size.x * 0.33f;
            float maxDistance = bounds.size.y * 0.25f;

            //Draw Ray
            Gizmos.DrawRay(new Ray(origin, direction * maxDistance));

            // Draw Sphere
            Vector3 spherePosition = direction * maxDistance + origin;
            Gizmos.color = isGrounded ? Color.magenta : Color.cyan;
            Gizmos.DrawSphere(spherePosition, radius);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BossRoomSensor"))
        {
            GlobalEvents.Instance.InvokeOnBossRoomEnter(this,new BossRoomEnterArgs());
            Destroy(other.gameObject);
        }
    }

    // void OnGUI()
    // {
    //     Rect rect = new Rect(10, 10, 100, 50);
    //     string text = stateMachine.currentStateName;
    //     GUIStyle style = new GUIStyle
    //     {
    //         fontSize = (int)(50 * (Screen.width / 1920f))
    //     };
    //     GUI.color = Color.blue;
    //     GUI.Label(rect, text, style);
    //     GUI.Label(new Rect(20, 20, 100, 50), isGrounded.ToString(), style);
    // }

}
