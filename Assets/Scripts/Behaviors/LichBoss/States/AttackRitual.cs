using System.Collections;
using StateMachineNamespace;
using UnityEngine;

namespace Behaviors.LichBoss.States
{
    public class AttackRitual : State
    {
        private LichBossController controller;
        private LichBossHelper helper;

        private float endAttackRitualCooldown;
        public AttackRitual(LichBossController controller) : base("AttackRitual")
        {
            this.controller = controller;
            helper = this.controller.helper;
        }

        public override void Enter()
        {
            base.Enter();

            // Set variables
            endAttackRitualCooldown = controller.attackRitualDuration;
            controller.thisAnimator.SetTrigger("tAttackRitual");
            // Schedule AttackRitual
            helper.StartStateCoroutine(ScheduleAttackRitual(controller.attackRitualDelay));
        }

        public override void Exit()
        {
            base.Exit();
            helper.ClearStateCoroutines();
        }

        public override void Update()
        {
            base.Update();

            if ((endAttackRitualCooldown -= Time.deltaTime) <= 0)
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

        private IEnumerator ScheduleAttackRitual(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            Debug.Log("Atacou RITUAL");

            var ritual = Object.Instantiate(controller.ritualPrefab, controller.staffBottom.position,
                controller.ritualPrefab.transform.rotation);

            if (helper.GetDistanceToPlayer() <= controller.distanceToRitual)
            {
                var playerLife = GameManager.Instance.player.GetComponent<LifeScript>();
                playerLife.InflictDamage(controller.gameObject,controller.attackDamage);
            }
            
            Object.Destroy(ritual,10f);
        }
    }
}

