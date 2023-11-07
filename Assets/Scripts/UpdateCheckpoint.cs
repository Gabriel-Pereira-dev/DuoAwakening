using System;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class UpdateCheckpoint : MonoBehaviour
    {
        public Transform newCheckpoint;
        public float playerY = 0f;
        
        public UpdateCheckpointnEvent onDead = new();
        private void OnTriggerEnter(Collider other)
        {
            var otherGameobject = other.gameObject;
            var otherLayer = otherGameobject.layer;
            var collidedWithPlayer = LayerMask.NameToLayer("Player") == otherLayer;
            if (collidedWithPlayer)
            {
                var gameManager = GameManager.Instance;

                var position = newCheckpoint.position;
                gameManager.lastCheckpoint = new Vector3(position.x, playerY, position.z);
            }
        }

    }
    
    [Serializable] public class UpdateCheckpointnEvent: UnityEvent<GameObject>{}
}