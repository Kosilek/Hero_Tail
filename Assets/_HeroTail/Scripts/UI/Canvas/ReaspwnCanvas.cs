using Kosilek.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kosilek.UI
{
    public class ReaspwnCanvas : Window
    {
        [SerializeField] private Button respawnButton;

        protected override void Awake()
        {
            base.Awake();
            AddListenerButton();
        }

        private void AddListenerButton()
        {
            respawnButton.onClick.AddListener(ActionRespawn);
        }

        private void ActionRespawn()
        {
            Close();
            LevelManager.Instance.spawnPlayer.SpawnCharacters();
        }
    }
}
