using System.Collections.Generic;
using StateMachineNamespace;
using UnityEngine;

namespace Behaviors.LichBoss.States
{
    public class Hurt : State
    {
        private LichBossController controller;
        private LichBossHelper helper;

        private float timePassed;
        public Hurt(LichBossController controller) : base("Hurt")
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
        }

        public override void Exit()
        {
            base.Exit();
            // Resume Damage
            controller.thisLife.isVulnerable = true;
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
                    State attackState = helper.HasLowHealth() ? controller.attackSuperState : controller.attackNormalState;
                    List<State> states = new List<State>(){controller.attackRitualState,attackState};
                    var state = states[Random.Range(0, states.Count)];
                    controller.stateMachine.ChangeState(state);
                    // controller.stateMachine.ChangeState(controller.idleState);
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

