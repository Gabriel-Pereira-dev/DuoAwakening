using StateMachineNamespace;
using UnityEngine;

namespace Behaviors.MeeleeCreature.States
{
    public class Hurt : State
    {
        private MeeleeCreatureController controller;
        private MeeleeCreatureHelper helper;

        private float timePassed;
        public Hurt(MeeleeCreatureController controller) : base("Hurt")
        {
            this.controller = controller;
            helper = this.controller.helper;
        }

        public override void Enter()
        {
            base.Enter();
            //Reset
            timePassed = 0f;

            // Pause Damage
            controller.thisLife.isVulnerable = false;
            controller.thisAnimator.SetTrigger("tHurt");
            
            // Shift Object control Navmesh to Physics
            controller.thisAgent.enabled = false;
            controller.thisRigidbody.isKinematic = false;
        }

        public override void Exit()
        {
            base.Exit();
            // Exit
            controller.thisLife.isVulnerable = true;
            
            // Shift Object control Physics back to Navmesh
            controller.thisAgent.enabled = true;
            controller.thisRigidbody.isKinematic = true;
        }

        public override void Update()
        {
            base.Update();
            // update Time
            timePassed += Time.deltaTime;

            // Switch state
            if (timePassed >= controller.hurtDuration)
            {
                if (controller.thisLife.IsDead())
                {
                    controller.stateMachine.ChangeState(controller.deadState);
                }
                else
                {
                    controller.stateMachine.ChangeState(controller.idleState);
                }


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

