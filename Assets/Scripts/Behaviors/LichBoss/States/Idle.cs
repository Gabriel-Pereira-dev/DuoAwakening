using StateMachineNamespace;
using UnityEngine;

namespace Behaviors.LichBoss.States
{
    public class Idle : State
    {
        private LichBossController controller;
        private LichBossHelper helper;
        private float stateTime;

        public Idle(LichBossController controller) : base("Idle")
        {
            this.controller = controller;
            helper = this.controller.helper;
        }

        public override void Enter()
        {
            base.Enter();

            stateTime = 0f;

        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            stateTime += Time.deltaTime;
            
            // Ignore if gameover
            if (GameManager.Instance.isGameOver) return;

            //Switch to follow
            if (stateTime >= controller.idleDuration)
            {
                controller.stateMachine.ChangeState(controller.followState);
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

