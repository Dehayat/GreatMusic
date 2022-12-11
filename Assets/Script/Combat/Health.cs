using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rosa
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private float health = 100f;

        [SerializeField] private float maxHealth;

        private void Awake()
        {
            if (maxHealth <= 0)
            {
                maxHealth = health;
            }
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }
        
        public float GetHealth()
        {
            return health;
        }

        public void Damage(float amount)
        {
            health -= amount;
            health = Mathf.Max(health, 0f);
        }
        public bool WillDie(float amount)
        {
            return amount >= health;
        }
    }
}