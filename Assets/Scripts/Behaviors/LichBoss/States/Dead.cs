using EventArgs;
using StateMachineNamespace;

namespace Behaviors.LichBoss.States
{
    public class Dead : State
    {
        private LichBossController controller;
        private LichBossHelper helper;
        public Dead(LichBossController controller) : base("Dead")
        {
            this.controller = controller;
            helper = this.controller.helper;
        }

        public override void Enter()
        {
            base.Enter();
            // Pause Damage
            controller.thisLife.isVulnerable = false;
            controller.thisAnimator.SetTrigger("tDead");
            
            // Game won
            GlobalEvents.Instance.InvokeOnGameWon(this,new GameWonArgs());
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

