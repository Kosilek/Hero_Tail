using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Kosilek.UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countHealthText;
        [SerializeField] private Image hpBar;

        private string countHealth = "{0} / {1}";
        private Coroutine coroutineUpdateHpBar;

        #region Const
        private const float SPEED_UPDATE_IMAGE_HEALTH = 1f;
        #endregion

        public void InitializationCharactersData(int health, int maxHealth)
        {
            UpdateHealthUI(health, maxHealth, false);
        }

        public void UpdateHealthUI(int health, int maxHealth, bool isDamage)
        {
            UpdateCountHealthText(health, maxHealth);
            UpdateImageHealthBar(health, maxHealth, isDamage);
        }

        private void UpdateCountHealthText(int health, int maxHealth)
        {
            countHealthText.text = string.Format(countHealth, health, maxHealth);
        }

        private void UpdateImageHealthBar(int health, int maxHealth, bool isDamage)
        {
            var fillAmount = (float)((float)health / (float)maxHealth);
            if (coroutineUpdateHpBar != null)
                StopCoroutine(coroutineUpdateHpBar);

            coroutineUpdateHpBar = isDamage ? StartCoroutine(IEUpdateImageBarDamage(fillAmount)) : StartCoroutine(IEUpdateImageBarHealing(fillAmount));
        }

        private IEnumerator IEUpdateImageBarDamage(float fillAmount)
        {
            Debug.Log("hpBar.fillAmount = " + hpBar.fillAmount + " FillAmount = " + fillAmount);
            for (float i = hpBar.fillAmount; i >= fillAmount; i -= Time.deltaTime * SPEED_UPDATE_IMAGE_HEALTH)
            {
                hpBar.fillAmount = i;
                yield return null;
            }
            Debug.Log("End coro = " + " FillAmount = " + fillAmount);
            hpBar.fillAmount = fillAmount;
            coroutineUpdateHpBar = null;
        }

        private IEnumerator IEUpdateImageBarHealing(float fillAmount)
        {
            Debug.Log("hpBar.fillAmount = " + hpBar.fillAmount + " FillAmount = " + fillAmount);
            for (float i = hpBar.fillAmount; i <= fillAmount; i += Time.deltaTime * SPEED_UPDATE_IMAGE_HEALTH)
            {
                hpBar.fillAmount = i;
                yield return null;
            }
            Debug.Log("End coro = " + " FillAmount = " + fillAmount);
            hpBar.fillAmount = fillAmount;
            coroutineUpdateHpBar = null;
        }
    }
}