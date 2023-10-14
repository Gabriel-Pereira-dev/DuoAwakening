using System;
using EventArgs;
using UnityEngine;
using UnityEngine.Rendering;

namespace Player
{
    public class PlayerDamageEffect : MonoBehaviour
    {
        public Volume volume;
        public LifeScript life;
        public float minWeight = 0.5f;
        public float maxWeight = 1f;

        private void Start()
        {
            life.OnDamage += OnDamage;
        }

        private void Update()
        {
            var alpha = Time.deltaTime / 1f;
            var newWeight = Mathf.Lerp(volume.weight, 0f, alpha);
            volume.weight = newWeight;
        }

        private void OnDamage(object sender, DamageEventArgs args)
        {
            var lifeRate = (float)life.health / (float)life.maxHealth;
            var effectIntesity = minWeight + (maxWeight - minWeight) * (1f - lifeRate);
            volume.weight = effectIntesity;
        }
        
    }
}