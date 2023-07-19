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

    }
}