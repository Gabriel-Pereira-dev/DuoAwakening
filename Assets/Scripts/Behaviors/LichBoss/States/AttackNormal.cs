using System.Collections;
using StateMachineNamespace;
using UnityEngine;

namespace Behaviors.LichBoss.States
{
    public class AttackNormal : State
    {
        private LichBossController controller;
        private LichBossHelper helper;

        private float endAttackNormalCooldown;
        private IEnumerator attackNormalCoroutine;
        public AttackNormal(LichBossController controller) : base("AttackNormal")
        {
            this.controller = controller;
            helper = this.controller.helper;
        }

        public override void Enter()
        {
            base.Enter();

            // Set variables
            endAttackNormalCooldown = controller.attackNormalDuration;
            controller.thisAnimator.SetTrigger("tAttackNormal");
            // Schedule AttackNormal
            attackNormalCoroutine = ScheduleAttackNormal();
            controller.StartCoroutine(ScheduleAttackNormal());
        }

        public override void Exit()
        {
            base.Exit();

            // Cancel attack
            if (attackNormalCoroutine != null)
            {
                controller.StopCoroutine(attackNormalCoroutine);
            }
        }

        public override void Update()
        {
            base.Update();

            if ((endAttackNormalCooldown -= Time.deltaTime) <= 0)
            {
                controller.stateMachine.ChangeState(controller.idleState);
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

        private IEnumerator ScheduleAttackNormal()
        {
            yield return new WaitForSeconds(controller.attackNormalDelay);
            PerformAttackNormal();
        }
        private void PerformAttackNormal()
        {
            var origin = controller.transform.position;
            var playerPosition = GameManager.Instance.player.transform.position;
            var direction = controller.transform.rotation * Vector3.forward;
            // var radius = controller.attackRadius;
            // var maxDistance = controller.attackSphereRadius;

            // var attackPosition = origin + direction * radius;
            // var layerMask = LayerMask.GetMask("Player");
            // // attack radius sphere cast
            // Collider[] colliders = Physics.OverlapSphere(attackPosition, maxDistance, layerMask);

            // foreach (var collider in colliders)
            // {
            //     var hitObject = collider.gameObject;

            //     var hitLifeScript = hitObject.GetComponent<LifeScript>();

            //     if (hitLifeScript != null)
            //     {
            //         var attacker = controller.gameObject;
            //         var attackDamage = controller.attackDamage;
            //         hitLifeScript.InflictDamage(attacker, attackDamage);
            //     }

            // }

        }
    }
}

