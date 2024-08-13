using Kosilek.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kosilek.UI
{
    public class GameCanvas : Window
    {
        [Header("Button")]
        [SerializeField]
        private Button startBattleButton;
        [SerializeField]
        private Button healingButton;
        [SerializeField]
        private Button admitDefeatButton;
        [SerializeField]
        internal Button changingWeaponsButton;

        public Image iconButton;
        public Sprite meeleSprite;
        public Sprite rangeSprite;

        #region Awake
        protected override void Awake()
        {
            base.Awake();
            AddListenerButton();
        }

        private void AddListenerButton()
        {
            startBattleButton.onClick.AddListener(ActionStartBattle);
            healingButton.onClick.AddListener(ActionHealingButton);
            admitDefeatButton.onClick.AddListener(ActionAdmitDefeat);
            changingWeaponsButton.onClick.AddListener(ActionChangingWeapons);
        }

        private void ActionStartBattle()
        {
            LevelManager.Instance.StartBattle();
            ActiveButton(false);
        }

        private void ActionHealingButton()
        {
            LevelManager.Instance.player.health.Healing();
        }

        private void ActionAdmitDefeat()
        {
            LevelManager.Instance.LoseBattle();
        }

        private void ActionChangingWeapons()
        {
            LevelManager.Instance.player.ChangingWeapons();
        }
        #endregion end Awake
        internal void IdleCanvas()
        {
            ActiveButton(true);
        }

        private void ActiveButton(bool state)
        {
            startBattleButton.gameObject.SetActive(state);
            healingButton.gameObject.SetActive(state);
            admitDefeatButton.gameObject.SetActive(!state);
        }
    }
}