using System.Collections;
using StateMachineNamespace;
using UnityEngine;

namespace Behaviors.LichBoss.States
{
    public class AttackSuper : State
    {
        private LichBossController controller;
        private LichBossHelper helper;

        private float endAttackCooldown;
        private IEnumerator attackSuperCoroutine;
        public AttackSuper(LichBossController controller) : base("AttackSuper")
        {
            this.controller = controller;
            helper = this.controller.helper;
        }

        public override void Enter()
        {
            base.Enter();

            // Set variables
            endAttackCooldown = controller.attackSuperDuration;
            controller.thisAnimator.SetTrigger("tAttack");
            // Schedule AttackSuper
            attackSuperCoroutine = ScheduleAttackSuper();
            controller.StartCoroutine(ScheduleAttackSuper());
        }

        public override void Exit()
        {
            base.Exit();

            // Cancel attack
            if (attackSuperCoroutine != null)
            {
                controller.StopCoroutine(attackSuperCoroutine);
            }
        }

        public override void Update()
        {
            base.Update();

            if ((endAttackCooldown -= Time.deltaTime) <= 0)
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

        private IEnumerator ScheduleAttackSuper()
        {
            yield return new WaitForSeconds(controller.attackSuperDelay);
            PerformAttack();
        }
        private void PerformAttack()
        {
            var origin = controller.transform.position;
            var playerPosition = GameManager.Instance.player.transform.position;
            var direction = controller.transform.rotation * Vector3.forward;
            //     var radius = controller.attackRadius;
            //     var maxDistance = controller.attackSphereRadius;

            //     var attackPosition = origin + direction * radius;
            //     var layerMask = LayerMask.GetMask("Player");
            //     // attack radius sphere cast
            //     Collider[] colliders = Physics.OverlapSphere(attackPosition, maxDistance, layerMask);

            //     foreach (var collider in colliders)
            //     {
            //         var hitObject = collider.gameObject;

            //         var hitLifeScript = hitObject.GetComponent<LifeScript>();

            //         if (hitLifeScript != null)
            //         {
            //             var attacker = controller.gameObject;
            //             var attackDamage = controller.attackDamage;
            //             hitLifeScript.InflictDamage(attacker, attackDamage);
            //         }

            //     }

        }
    }
}

