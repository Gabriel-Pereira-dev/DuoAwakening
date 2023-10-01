using StateMachineNamespace;
using UnityEngine;
namespace Player.States
{
    public class Idle : State
    {
        private PlayerController controller;

        public Idle(PlayerController controller) : base("Idle")
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
            
            // Ignore if isInCutscene
            var bossBattleHandler = GameManager.Instance.bossBattleHandler;
            var isInCutscene = bossBattleHandler.IsInCutscene();
            if (isInCutscene)
            {
                return;
            }

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

            // Change to Walking
            if (!controller.movementVector.IsZero())
            {
                controller.stateMachine.ChangeState(controller.walkingState);
                return;
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

    }
}