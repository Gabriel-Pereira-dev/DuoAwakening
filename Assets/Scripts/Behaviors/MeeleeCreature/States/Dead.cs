using StateMachineNamespace;
using Object = UnityEngine.Object;

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
            
            // Update Animation
            controller.thisAnimator.SetTrigger("tDead");
            
            // Disable Collider
            controller.thisCollider.enabled = false;
            
            // Create effect
            var knockOutEffect = controller.knockOutEffect;
            var position = controller.transform.position;
            var rotation = knockOutEffect.transform.rotation;
            Object.Instantiate(knockOutEffect, position, rotation);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            
            // Destroy if far away
            var distanceToPlayer = helper.GetDistanceToPlayer();
            if (distanceToPlayer >= controller.destroyIfFar)
            {
                Object.Destroy(controller.gameObject);
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

