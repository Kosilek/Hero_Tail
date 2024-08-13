using Kosilek;
using Kosilek.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kosilek.Manager
{
    public class LevelManager : SimpleSingleton<LevelManager>
    {
        public PlayerCntr player;
        public Enemy enemy;

        [Header("Spawn")]
        public Spawn spawnPlayer;
        public Spawn spawnEnemy;

        private void Start()
        {
            spawnPlayer.SpawnCharacters();
        }

        internal void StartBattle()
        {
            SpawnEnemy();
            StartCoroutine(IE());

            IEnumerator IE()
            {
                yield return new WaitForSeconds(.5f);
                StartBattleCharacter();
            }
        }

        private void SpawnEnemy()
        {
            spawnEnemy.SpawnCharacters();
        }

        private void StartBattleCharacter()
        {
            StartBattleCharacter(player);
            StartBattleCharacter(enemy);
        }

        private void StartBattleCharacter(Character character)
        {
            character.isBattle = true;
            character.StartBattle();
        }

        internal void StopBattle()
        {
            StopBattle(player);
            StopBattle(enemy);
            CanvasManager.Instance.gameCanvas.IdleCanvas();
        }

        internal void StopBattle(Character character)
        {
            character.isBattle = false;
            if (character.coroutineActionDelay != null)
                character.StopCoroutine(character.coroutineActionDelay);
        }

        internal void DestroyCharacters(Character character)
        {
            Destroy(character.gameObject);
            character = null;
        }

        internal void LoseBattle()
        {
            StopBattle();
            DestroyCharacters(enemy);
        }
    }
}