using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        private int maxHealth;
        private int health;
        [SerializeField] private Slider slider;

        private void Update()
        {
            slider.value = health;
        }

        public void SetMaxHealth(int maxHealth)
        {
            this.maxHealth = maxHealth;
            health = maxHealth;
            slider.maxValue = maxHealth;
        }
        
        public void SetHealth(int health)
        {
            
            this.health = health;
        }
    }
}