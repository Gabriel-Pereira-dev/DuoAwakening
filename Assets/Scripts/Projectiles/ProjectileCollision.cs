using System;
using UnityEngine;

namespace Projectiles
{
    public class ProjectileCollision : MonoBehaviour
    {
        public GameObject hitEffect;
        [HideInInspector]public GameObject attacker;
        [HideInInspector]public int damage = 1;
        private void Awake()
        {
            
        }

        private void Start()
        {
            
        }

        private void OnCollisionEnter(Collision other)
        {
            // Process Player Collision
            var hitObject = other.gameObject;
            var hitLayer = hitObject.layer;
            var collidedWithPlayer = LayerMask.NameToLayer("Player") == hitLayer;

            if (collidedWithPlayer)
            {
                var hitLife = hitObject.GetComponent<LifeScript>();
                if (hitLife != null)
                {
                    hitLife.InflictDamage(attacker,damage);
                }
            }
            
            // Creat hit reaction
            if (hitEffect != null)
            {
                var effect = Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                Destroy(effect,10f);
            }
            
            // Destroy Projectile
            Destroy(gameObject);
        }
    }
}