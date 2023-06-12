using StateMachineNamespace;
using UnityEngine;

namespace Behaviors.MeeleeCreature.States
{
    public class Follow : State
    {
        private MeeleeCreatureController controller;
        private MeeleeCreatureHelper helper;

        private readonly float updateInterval = 1f;
        private float updateCooldown = 0f;
        private float ceaseFollowCooldown;
        public Follow(MeeleeCreatureController controller) : base("Follow")
        {
            this.controller = controller;
            helper = this.controller.helper;
        }

        public override void Enter()
        {
            base.Enter();
            //Reset
            updateCooldown = 0f;
            ceaseFollowCooldown = controller.ceaseFollowInterval;

        }

        public override void Exit()
        {
            base.Exit();
            // Stop Following
            controller.thisAgent.ResetPath();
        }

        public override void Update()
        {
            base.Update();

            // Follow

            if ((updateCooldown -= Time.deltaTime) <= 0)
            {
                updateCooldown = updateInterval;
                var player = GameManager.Instance.player;
                var playerPosition = player.transform.position;
                controller.thisAgent.SetDestination(playerPosition);
            }

            if ((ceaseFollowCooldown -= Time.deltaTime) <= 0)
            {
                if (!helper.IsPlayerOnSight())
                {
                    controller.stateMachine.ChangeState(controller.idleState);
                }
            }

            // Attempt to attack
            if (helper.GetDistanceToPlayer() <= controller.distanceToAttack)
            {
                controller.stateMachine.ChangeState(controller.attackState);
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

