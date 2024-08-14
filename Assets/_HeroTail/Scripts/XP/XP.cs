using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kosilek.Data;
using Kosilek.UI;
using Kosilek.Manager;

namespace Kosilek.Characters
{
    public class XP : MonoBehaviour
    {
        [SerializeField] internal XPData xpData;
        [SerializeField]
        private int xp = 0;
        [SerializeField]
        private int maxXp = 10;
        [SerializeField]
        private int level = 1;

        private void Start()
        {
            CanvasManager.Instance.xpUI.UpdateUIText(ref xp, maxXp, 0, ref level);
            CanvasManager.Instance.xpUI.UpdateUILevel(ref level, true);
        }

        internal void GetXp(int countXp)
        {
            CanvasManager.Instance.xpUI.UpdateUIText(ref xp, maxXp, countXp, ref level);
        }

        internal void UpdateStatCharacter()
        {
            LevelManager.Instance.player.health.maxHealth = LevelManager.Instance.player.characterData.health;
            LevelManager.Instance.player.health.maxHealth += xpData.updateHealth * (level - 1);
            LevelManager.Instance.player.armor = LevelManager.Instance.player.characterData.armor;
            LevelManager.Instance.player.armor += xpData.updateArmor * (level - 1);
            LevelManager.Instance.player.damage = LevelManager.Instance.player.characterData.damage;
            LevelManager.Instance.player.damage += xpData.updateDamage * (level - 1);
        }

        internal int UpdateMaxXp()
        {
            maxXp *= 2;
            return maxXp;
        }
    }
}