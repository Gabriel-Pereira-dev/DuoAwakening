using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StateMachineNamespace;
using UnityEngine;

namespace Behaviors.LichBoss.States
{
    public class Teleport : State
    {
        private LichBossController controller;
        private LichBossHelper helper;

        private float timePassed;
        // 4 == middle
        private int lastTeleportPosition =  4;
        public Teleport(LichBossController controller) : base("Teleport")
        {
            this.controller = controller;
            helper = this.controller.helper;
        }

        public override void Enter()
        {
            base.Enter();
            //Reset
            timePassed = 0f;

            // Pause Damage
            controller.thisLife.isVulnerable = false;
            //controller.thisAnimator.SetTrigger("tTeleport");
            
            // Shift Object control Navmesh to Physics
            controller.thisAgent.enabled = false;
            controller.thisRigidbody.isKinematic = false;
            
            
            // Schedule Teleport
            controller.StartCoroutine(ScheduleTeleport(controller.teleportDelay));
        }

        public override void Exit()
        {
            base.Exit();
            // Exit
            controller.thisLife.isVulnerable = true;
            
            // Shift Object control Physics back to Navmesh
            controller.thisAgent.enabled = true;
            controller.thisRigidbody.isKinematic = true;
        }

        public override void Update()
        {
            base.Update();
            // update Time
            timePassed += Time.deltaTime;

            // Switch state
            if (timePassed >= controller.teleportDuration)
            {
                State attackState = helper.HasLowHealth() ? controller.attackSuperState : controller.attackNormalState;
                controller.stateMachine.ChangeState(attackState);
                
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
        
        private IEnumerator ScheduleTeleport(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            // Create Teleport Effect
            var teleport = Object.Instantiate(controller.teleportPrefab, controller.gameObject.transform.position,
                controller.teleportPrefab.transform.rotation);
            
            // Get teleport position
            var numArray =  Enumerable.Range(0, controller.teleportSpawnPoints.Count).ToList();
            numArray.RemoveAt(lastTeleportPosition);
            var randomTeleportPosition = numArray[Random.Range(0, numArray.Count)];
            var teleportPosition = controller.teleportSpawnPoints[randomTeleportPosition].position;

            // Teleport
            //var isKinematic = controller.thisRigidbody.isKinematic;
            //isKinematic = true;
            controller.gameObject.transform.position = teleportPosition;
            //isKinematic = false;

            lastTeleportPosition = randomTeleportPosition;
            Object.Destroy(teleport,3f);
        }

    }
}

