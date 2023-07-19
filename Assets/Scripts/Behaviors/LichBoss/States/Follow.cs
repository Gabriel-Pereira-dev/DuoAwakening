using StateMachineNamespace;
using UnityEngine;

namespace Behaviors.LichBoss.States
{
    public class Follow : State
    {
        private LichBossController controller;
        private LichBossHelper helper;
        private readonly float attackAttemptInterval = 0.5f;
        private float attackAttemptCooldown = 0f;

        private readonly float targetUpdateInterval = 0.5f;
        private float targetUpdateCooldown = 0f;
        private float ceaseFollowCooldown = 0f;

        public Follow(LichBossController controller) : base("Follow")
        {
            this.controller = controller;
            helper = this.controller.helper;
        }

        public override void Enter()
        {
            base.Enter();
            //Reset
            attackAttemptCooldown = attackAttemptInterval;
            targetUpdateCooldown = 0f;
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
            if ((targetUpdateCooldown -= Time.deltaTime) <= 0)
            {
                targetUpdateCooldown = targetUpdateInterval;
                var player = GameManager.Instance.player;
                var playerPosition = player.transform.position;
                controller.thisAgent.SetDestination(playerPosition);
            }


            // Attempt to attack with Ritual
            if ((attackAttemptCooldown -= attackAttemptInterval) <= 0)
            {
                attackAttemptCooldown = attackAttemptInterval;

                // Ritual
                var distanceToPlayer = helper.GetDistanceToPlayer();
                var isCloseEnoughToRitual = distanceToPlayer <= controller.distanceToRitual;
                if (isCloseEnoughToRitual)
                {
                    controller.stateMachine.ChangeState(controller.attackRitualState);
                    return;
                }

            }

            // Attempt to cease follow(Normal and Super)
            if ((ceaseFollowCooldown -= Time.deltaTime) <= 0)
            {
                State newState = helper.HasLowHealth() ? controller.attackSuperState : controller.attackNormalState;
                controller.stateMachine.ChangeState(newState);
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

