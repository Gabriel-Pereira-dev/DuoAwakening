using System.Collections;
using UnityEngine;

namespace Behaviors.LichBoss
{

    public class LichBossHelper
    {
        private LichBossController controller;

        public LichBossHelper(LichBossController controller)
        {
            this.controller = controller;
        }

        public float GetDistanceToPlayer()
        {
            var player = GameManager.Instance.player;
            var playerTransformPosition = player.transform.position;
            var origin = controller.transform.position;
            var positionDifference = playerTransformPosition - origin;
            var distance = positionDifference.magnitude;
            return distance;
            
        }

        public bool HasLowHealth()
        {
            var life = controller.thisLife;
            float lifeRate = (float)life.health / (float)life.maxHealth; ;
            return lifeRate <= controller.lowHealthThreshold;
        }

        public void StartStateCoroutine(IEnumerator enumerator)
        {
            controller.StartCoroutine(enumerator);
            controller.stateCoroutines.Add(enumerator);
        }
        
        public void ClearStateCoroutines()
        {
            foreach (var enumerator in controller.stateCoroutines)
            {
                controller.StopCoroutine(enumerator);
            }
            controller.stateCoroutines.Clear();
        }
        
        public void FacePlayer()
        {
            var transform = controller.transform;
            var player = GameManager.Instance.player;
            var vecToPlayer = player.transform.position - transform.position;
            vecToPlayer.y = 0;
            vecToPlayer.Normalize();
            var desiredRotation = Quaternion.LookRotation(vecToPlayer);
            var newRotation = Quaternion.LerpUnclamped(transform.rotation, desiredRotation, 0.2f);
            transform.rotation = newRotation;
        }
    }
}