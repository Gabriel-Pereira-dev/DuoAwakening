using StateMachineNamespace;
using UnityEngine;
namespace Player.States
{
    public class Hurt : State
    {
        private PlayerController controller;
        
        private float timePassed;
        
        

        public Hurt(PlayerController controller) : base("Hurt")
        {
            this.controller = controller;
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
            
            // Ignore if isInCutscene
            var bossBattleHandler = GameManager.Instance.bossBattleHandler;
            var isInCutscene = bossBattleHandler.IsInCutscene();
            if (isInCutscene)
            {
                return;
            }
            
            // update Time
            timePassed += Time.deltaTime;
            
            // Switch to dead state
            if (controller.thisLife.IsDead())
            {
                controller.stateMachine.ChangeState(controller.deadState);
                return;
            }
            
            // Switch to idle state
            if (timePassed >= controller.hurtDuration)
            {
                controller.stateMachine.ChangeState(controller.idleState);
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