using Kosilek.Characters;
using Kosilek.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kosilek.UI
{
    public class XPUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countLevelText;
        [SerializeField] private TextMeshProUGUI countXPText;
        [SerializeField] private Image xpBar;

        private string countXP = "{0} / {1}";

        internal void UpdateUIText(ref int xp, int maxXp, int countXp, ref int level)
        {
            if (countXp + xp < maxXp)
                xp += countXp;
            else
            {
                xp = xp + countXp - maxXp;
                maxXp = UpdateUILevel(ref level, false);
            }

            Debug.Log("XP = " + xp + " MaXXP = " + maxXp);
            countXPText.text = string.Format(countXP, xp, maxXp);
            UpdateUIImageFill(xp, maxXp);
        }

        internal int UpdateUILevel(ref int level, bool isStart)
        {
            var maxXp = 0;
            if(!isStart)
            {
                level++;
                maxXp = LevelManager.Instance.player.exp.UpdateMaxXp();
            }
            countLevelText.text = level.ToString();
            LevelManager.Instance.player.exp.UpdateStatCharacter();
            LevelManager.Instance.player.health.Healing(LevelManager.Instance.player.health.maxHealth);
            return maxXp;
        }

        private void UpdateUIImageFill(int xp, int maxXp)
        {
            xpBar.fillAmount = ((float)xp / (float)maxXp);
        }
    }
}