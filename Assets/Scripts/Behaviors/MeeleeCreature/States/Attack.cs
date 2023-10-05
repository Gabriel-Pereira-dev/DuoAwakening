using System.Collections;
using StateMachineNamespace;
using UnityEngine;

namespace Behaviors.MeeleeCreature.States
{
    public class Attack : State
    {
        private MeeleeCreatureController controller;
        private MeeleeCreatureHelper helper;

        private float endAttackCooldown;
        private IEnumerator attackCoroutine;
        public Attack(MeeleeCreatureController controller) : base("Attack")
        {
            this.controller = controller;
            helper = this.controller.helper;
        }

        public override void Enter()
        {
            base.Enter();
            // Set variables
            endAttackCooldown = controller.attackDurantion;
            controller.thisAnimator.SetTrigger("tAttack");
            // Schedule Attack
            attackCoroutine = ScheduleAttack();
            controller.StartCoroutine(ScheduleAttack());
        }

        public override void Exit()
        {
            base.Exit();

            // Cancel attack
            if (attackCoroutine != null)
            {
                controller.StopCoroutine(attackCoroutine);
            }
        }

        public override void Update()
        {
            base.Update();
            
            // Face player 
            helper.FacePlayer();
            
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

        private IEnumerator ScheduleAttack()
        {
            yield return new WaitForSeconds(controller.damageDelay);
            PerformAttack();
        }
        private void PerformAttack()
        {
            var origin = controller.transform.position;
            var playerPosition = GameManager.Instance.player.transform.position;
            var direction = controller.transform.rotation * Vector3.forward;
            var radius = controller.attackRadius;
            var maxDistance = controller.attackSphereRadius;

            var attackPosition = origin + direction * radius;
            var layerMask = LayerMask.GetMask("Player");
            // attack radius sphere cast
            Collider[] colliders = Physics.OverlapSphere(attackPosition, maxDistance, layerMask);

            foreach (var collider in colliders)
            {
                var hitObject = collider.gameObject;

                var hitLifeScript = hitObject.GetComponent<LifeScript>();

                if (hitLifeScript != null)
                {
                    var attacker = controller.gameObject;
                    var attackDamage = controller.attackDamage;
                    hitLifeScript.InflictDamage(attacker, attackDamage);
                }

            }



            //attack damage
        }
    }
}

