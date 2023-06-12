using StateMachineNamespace;
using UnityEngine;
namespace Player.States
{
    public class Walking : State
    {
        private PlayerController controller;

        public Walking(PlayerController controller) : base("Walking")
        {
            this.controller = controller;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            // Change to Attack
            if (controller.AttemptToAttack())
            {
                return;
            }

            // Change to defense
            if (controller.hasDefenseInput)
            {
                controller.stateMachine.ChangeState(controller.defendState);
                return;
            }



            // Change to Jump
            if (controller.hasJumpInput)
            {
                controller.stateMachine.ChangeState(controller.jumpState);
                return;
            }

            // Change to Idle
            if (controller.movementVector.IsZero())
            {
                controller.stateMachine.ChangeState(controller.idleState);
                return;
            }

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            // Create movement Vector
            Vector3 walkVector = new Vector3(controller.movementVector.x, 0f, controller.movementVector.y);
            walkVector = controller.GetFoward() * walkVector;
            walkVector = Vector3.ProjectOnPlane(walkVector, controller.slopeNormal);
            walkVector *= controller.movementSpeed;
            // Apply input to character
            controller.thisRigidbody.AddForce(walkVector, ForceMode.Force);

            // Rotate Character
            controller.RotateBodyToFaceInput();
        }

        public override void LateUpdate()
        {
            base.LateUpdate();

        }
    }
}