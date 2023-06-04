using UnityEngine;

public class Jump : State
{
    private PlayerController controller;
    private bool hasJumped;
    private float cooldown;

    public Jump(PlayerController controller) : base("Jump")
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        base.Enter();

        // Reset vars
        hasJumped = false;
        cooldown = 0.5f;

        // Handle animator
        controller.thisAnimator.SetBool("bJumping", true);
        controller.thisAudioSource.PlayOneShot(controller.jumpSound);
    }

    public override void Exit()
    {
        base.Exit();

        // Handle animator
        controller.thisAnimator.SetBool("bJumping", false);
    }

    public override void Update()
    {
        base.Update();

        cooldown -= Time.deltaTime;

        if (hasJumped && controller.isGrounded && cooldown <= 0)
        {
            controller.stateMachine.ChangeState(controller.idleState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        if (!hasJumped)
        {
            hasJumped = true;
            ApplyImpulse();
        }

        // Create movement Vector
        Vector3 walkVector = new Vector3(controller.movementVector.x, 0f, controller.movementVector.y);
        walkVector = controller.GetFoward() * walkVector;
        walkVector *= controller.movementSpeed * controller.jumpMovementFactor;

        // Apply input to character
        controller.thisRigidbody.AddForce(walkVector, ForceMode.Force);

        // Rotate Character
        controller.RotateBodyToFaceInput();
    }

    private void ApplyImpulse()
    {
        // Apply Impulse
        Vector3 forceVector = Vector3.up * controller.jumpPower;
        controller.thisRigidbody.AddForce(forceVector, ForceMode.Impulse);
    }

}
