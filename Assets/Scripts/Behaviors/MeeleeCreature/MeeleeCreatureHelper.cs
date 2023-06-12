using UnityEngine;

namespace Behaviors.MeeleeCreature.States
{

    public class MeeleeCreatureHelper
    {
        private MeeleeCreatureController controller;



        public MeeleeCreatureHelper(MeeleeCreatureController controller)
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

        public bool IsPlayerOnSight()
        {
            var player = GameManager.Instance.player;
            var playerTransformPosition = player.transform.position;
            var origin = controller.transform.position;
            var positionDifference = playerTransformPosition - origin;
            var direction = positionDifference.normalized;
            var distance = positionDifference.magnitude;
            var searchRadius = controller.searchRadius;
            if (distance > searchRadius)
            {
                return false;
            }
            var layerMask = LayerMask.GetMask("Default", "Player");
            if (Physics.Raycast(origin, direction, out var hitInfo, searchRadius, layerMask))
            {
                if (hitInfo.transform.gameObject != GameManager.Instance.player)
                {
                    return false;
                }
            }
            return true;
        }

    }
}