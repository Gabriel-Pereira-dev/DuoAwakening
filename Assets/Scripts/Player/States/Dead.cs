using EventArgs;
using StateMachineNamespace;
using UnityEngine;
namespace Player.States
{
    public class Dead : State
    {
        private PlayerController controller;

        public Dead(PlayerController controller) : base("Dead")
        {
            this.controller = controller;
        }

        public override void Enter()
        {
            base.Enter();
            controller.thisAnimator.SetTrigger("tGameOver");
            
            // Make player invulnerable
            controller.thisLife.isVulnerable = false;
            
            // Game over
            GlobalEvents.Instance.InvokeOnGameOver(this,new GameOverArgs());
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
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