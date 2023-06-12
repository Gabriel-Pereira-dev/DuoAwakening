using StateMachineNamespace;
using UnityEngine;

namespace Behaviors.MeeleeCreature.States
{
    public class Idle : State
    {
        private MeeleeCreatureController controller;
        private MeeleeCreatureHelper helper;

        private float searchCooldown;
        public Idle(MeeleeCreatureController controller) : base("Idle")
        {
            this.controller = controller;
            helper = this.controller.helper;
        }

        public override void Enter()
        {
            base.Enter();

            //Reset cooldown
            searchCooldown = controller.targetSearchInteval;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            // Update cooldown
            searchCooldown -= Time.deltaTime;
            if (searchCooldown <= 0)
            {
                searchCooldown = controller.targetSearchInteval;
                // Search Player
                if (helper.IsPlayerOnSight())
                {
                    controller.stateMachine.ChangeState(controller.followState);
                    return;
                }
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

