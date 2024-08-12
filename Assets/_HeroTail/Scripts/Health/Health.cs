using Kosilek.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kosilek.Characters
{
    public class Health : MonoBehaviour
    {
        private int health;
        private int maxHealth;

        [SerializeField] HealthUI healthUI;

        public void InitializationCharactersData(int health)
        {
            maxHealth = health;
            this.health = health;
            healthUI.InitializationCharactersData(health, maxHealth);
        }

        public void Damage(int damage)
        {
            if (damage > health)
                Death();

            else
                health -= damage;
            healthUI.UpdateHealthUI(health, maxHealth, true);
        }

        public void Healing(int healing)
        {
            if (health + healing >= maxHealth)
                health = maxHealth;
            else
                health += healing;
            healthUI.UpdateHealthUI(health, maxHealth, false);
        }

        public void Death()
        {

        }
    }
}