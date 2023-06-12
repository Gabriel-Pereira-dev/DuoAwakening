using StateMachineNamespace;
using UnityEngine;

namespace Player.States
{

    public class Attack : State
    {
        private PlayerController controller;
        public int stage = 1;
        private float stateTime;
        private bool firstFixedUpdate;

        public Attack(PlayerController controller) : base("Attack")
        {
            this.controller = controller;
        }

        public override void Enter()
        {
            base.Enter();

            // Error: invalid stage
            if (stage <= 0 || stage > controller.attackStages)
            {
                controller.stateMachine.ChangeState(controller.idleState);
                return;
            }
            // Reset variables
            stateTime = 0f;
            firstFixedUpdate = true;

            // Set Animator trigger
            controller.thisAnimator.SetTrigger("tAttack" + stage);

            // Toggle hitbox
            controller.swordHitbox.SetActive(true);
        }

        public override void Exit()
        {
            base.Exit();
            // Toggle hitbox
            controller.swordHitbox.SetActive(false);

        }

        public override void Update()
        {
            base.Update();

            // Change to Attack
            if (controller.AttemptToAttack())
            {
                return;
            }

            // Update state time
            stateTime += Time.deltaTime;

            // Exit after time
            if (IsStageExpired())
            {
                controller.stateMachine.ChangeState(controller.idleState);
                return;
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (firstFixedUpdate)
            {
                firstFixedUpdate = false;

                //Look Rotation
                controller.RotateBodyToFaceInput(1f);

                // Impulse
                var impulseValue = controller.attackStageImpulse[stage - 1];
                var impulseVector = controller.thisRigidbody.rotation * Vector3.forward;
                impulseVector *= impulseValue;
                controller.thisRigidbody.AddForce(impulseVector, ForceMode.Impulse);
            }
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public bool CanSwitchStages()
        {
            // Get Attack Variables
            var isLastState = stage == controller.attackStages;
            var stageDuration = controller.attackStageDurations[stage - 1];
            var stageMaxInterval = isLastState ? 0 : controller.attackStageMaxInterval[stage - 1];
            var maxStageTimeDuration = stageDuration + stageMaxInterval;
            // Reply
            return !isLastState && stateTime >= stageDuration && stateTime <= maxStageTimeDuration;
        }

        public bool IsStageExpired()
        {
            // Get Attack Variables
            var isLastState = stage == controller.attackStages;
            var stageDuration = controller.attackStageDurations[stage - 1];
            var stageMaxInterval = isLastState ? 0 : controller.attackStageMaxInterval[stage - 1];
            var maxStageTimeDuration = stageDuration + stageMaxInterval;
            // Reply
            return stateTime > maxStageTimeDuration;
        }

    }
}