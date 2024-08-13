using Kosilek.Enum;
using Kosilek.Manager;
using Kosilek.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;


namespace Kosilek.Characters
{
    public class Health : MonoBehaviour
    {
        private int health;
        private int maxHealth;

        [SerializeField] HealthUI healthUI;

        internal void InitializationCharactersData(int health, bool isStart)
        {
            maxHealth = health;
            this.health = health;
            healthUI.InitializationCharactersData(health, maxHealth, isStart);
        }

        internal void Damage(int damage, int armor, PlayerType playerType)
        {
            if (damage - armor <= 0)
                damage = 1;
            else damage -= armor;

          //  Debug.Log("health = " + health + " damage = " + damage);

            if (damage >= health)
            {
                health = 0;
                Death(playerType);
            }
            else
                health -= damage;
            healthUI.UpdateHealthUI(health, maxHealth, true, false);
        }

        internal void Healing(int healing)
        {
            if (health + healing >= maxHealth)
                health = maxHealth;
            else
                health += healing;
            healthUI.UpdateHealthUI(health, maxHealth, false, false);
        }

        public void Healing()
        {
            health = maxHealth;
            healthUI.UpdateHealthUI(health, maxHealth, false, false);
        }

        private void Death(PlayerType playerType)
        {
            LevelManager.Instance.StopBattle();

            StartCoroutine(IE());

            IEnumerator IE()
            {
                yield return new WaitForSeconds(.5f);
                if (playerType == PlayerType.AI)
                {
                    LevelManager.Instance.DestroyCharacters(LevelManager.Instance.enemy);
                }
                else
                {
                    LevelManager.Instance.DestroyCharacters(LevelManager.Instance.enemy);
                    LevelManager.Instance.DestroyCharacters(LevelManager.Instance.player);
                    CanvasManager.Instance.respawnCanvas.Open();
                }
            }
        }
    }
}