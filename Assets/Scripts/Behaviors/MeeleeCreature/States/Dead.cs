using StateMachineNamespace;

namespace Behaviors.MeeleeCreature.States
{
    public class Dead : State
    {
        private MeeleeCreatureController controller;
        private MeeleeCreatureHelper helper;
        public Dead(MeeleeCreatureController controller) : base("Dead")
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

