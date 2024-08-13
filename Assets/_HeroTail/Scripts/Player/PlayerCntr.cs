using System;
using Kosilek.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kosilek.Enum;
using Kosilek.UI;
using Unity.VisualScripting;

namespace Kosilek.Characters
{
    public class PlayerCntr : Character
    {
        #region Const
        private const float DELAY_CHANGING_WEAPON = 2f;
        #endregion end Const

        private List<(float, string, Action)> listAction = new();

        protected override void Awake()
        {
            base.Awake();
        }

        internal void ChangingWeapons()
        {
            if (LevelManager.Instance.player.selectedWeapons == CharacterType.Melee)
            {
                LevelManager.Instance.player.selectedWeapons = CharacterType.Ranged;
                CanvasManager.Instance.gameCanvas.iconButton.sprite = CanvasManager.Instance.gameCanvas.rangeSprite;
            }
            else
            {
                LevelManager.Instance.player.selectedWeapons = CharacterType.Melee;
                CanvasManager.Instance.gameCanvas.iconButton.sprite = CanvasManager.Instance.gameCanvas.meeleSprite;
            }

            CanvasManager.Instance.gameCanvas.changingWeaponsButton.interactable = false;
            isChangingWeapon = true;
            characterUI.typeActionImageColor.color = characterUI.colorChangingWeapon;
            listAction = new List<(float, string, Action)>();
            LevelManager.Instance.StopBattle(LevelManager.Instance.player);
            var delayAttack = 0f;
            delayAttack = selectedWeapons == CharacterType.Melee ? delayAttack = delayAttackMeele : delayAttack = delayAttackRange;
            if (isPreparation)
            {
                StartCoroutine(IEChangingWeaponsPreparation(delayAttack));
            }
            else if (isTimeAttack)
            {
                StartCoroutine(IEIEChangingWeaponsTimeAttack(delayAttack));
            }
        }

        private IEnumerator IEIEChangingWeaponsTimeAttack(float delayAttack)
        {
            isBattle = true;
            listAction = GetListActionTimeAttack(delayAttack);
            yield return StartCoroutine(IEDelayAction(DELAY_CHANGING_WEAPON, "ChangingWeapons", characterUI.image, null, null));
            CanvasManager.Instance.gameCanvas.changingWeaponsButton.interactable = true;
            StartBattle(listAction);
        }

        private IEnumerator IEChangingWeaponsPreparation(float delayAttack)
        {
            isBattle = true;
            listAction = GetListActionTimeAttack(delayAttack);
            yield return StartCoroutine(IEDelayAction(DELAY_CHANGING_WEAPON, "ChangingWeapons", characterUI.image, null, null));
            CanvasManager.Instance.gameCanvas.changingWeaponsButton.interactable = true;
            yield return StartCoroutine(IEDelayAction(timerPreparation, delayInPreparation, "delayInPreparation", characterUI.image, ActionPreparing, null));
            StartBattle(listAction);
        }
    }
}