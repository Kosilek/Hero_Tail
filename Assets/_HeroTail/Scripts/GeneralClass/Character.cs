using Kosilek.Data;
using Kosilek.Enum;
using Kosilek.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kosilek.Characters
{
    public class Character : MonoBehaviour
    {
        #region Data
        [SerializeField] private CharacterData characterData;
        #endregion end Data

        #region ScriptsComponent
        [SerializeField] private CharacterUI characterUI;
        [SerializeField] private Health health;
        #endregion end ScriptsComponent

        #region Main State
        protected PlayerType playerType;
        protected CharacterType selectedWeapons;
        protected string characterName;
        protected float armor;
        protected float damage;
        protected float delayInPreparation;
        protected float attackTime;
        protected float delayInChangingWeapons;
        protected float delayAttackMeele; //Player and AI
        protected float delayAttackRange; // Player and AI
        protected CharacterType characterType; //AI
        protected int chanceOfAppearance; // AI
        #endregion end Main State

        #region List Action
        private List<(float delay, string actionName, Action action)> actionSequence;
        #endregion

        #region Coroutine
        protected Coroutine coroutineActionDelay;
        #endregion end Coroutine

        #region Animator
        [SerializeField] Animator animator;
        #endregion

        #region Const
        private const string MEELE = "Meele";
        private const string RANGE = "Range";
        private const string HIT = "Hit";
        private const string HEALING = "Healing";
        #endregion

        #region Awake
        protected virtual void Awake()
        {
            InitializationCharactersData();
        }

        private void InitializationCharactersData()
        {
            playerType = characterData.playerType;
            characterName = characterData.characterName;
            health.InitializationCharactersData(characterData.health);
            armor = characterData.armor;
            damage = characterData.damage;
            delayInPreparation = characterData.delayInPreparation;
            attackTime = characterData.attackTime;
            delayInChangingWeapons = characterData.delayInChangingWeapons;
            delayAttackMeele = characterData.delayAttackMeele;
            delayAttackRange = characterData.delayAttackRange;

            if (playerType == PlayerType.AI)
            {
                characterType = characterData.characterType;
                chanceOfAppearance = characterData.chanceOfAppearance;
            }

        }
        #endregion end Awake

        #region Action Character

        protected void ActionCharacter()
        {
            actionSequence = new List<(float, string, Action)>
            {
                (delayInPreparation, "Preparation", null),
                (attackTime, "Preparing for the attack", null),
            };

            StartCoroutine(IE());

            IEnumerator IE()
            {
                foreach (var (delay, actionName, action) in actionSequence)
                {
                    if (coroutineActionDelay != null)
                        StopCoroutine(coroutineActionDelay);

                    yield return coroutineActionDelay = StartCoroutine(IEDelayAction(delay, actionName, characterUI.image, action));
                }

                OnActionCompleted();
            }
        }
        private IEnumerator IEDelayAction(float delay, string nameAction, Image image, Action action)
        {
            action?.Invoke();
            for (float i = 0; i < delay; i += Time.deltaTime)
            {
                image.fillAmount = i / delay;
                yield return null;
            }

            coroutineActionDelay = null;
        }

        private void OnActionCompleted()
        {

        }

        private void ActiveGameOvbjectImage() => characterUI.ActiveGameObjectImage(true);
        private void UnActiveGameOvbjectImage() => characterUI.ActiveGameObjectImage(false);
        #endregion end Action Character

        #region Test

        public void TestFight()
        {
            var delayAttack = 0f;
            delayAttack = selectedWeapons == CharacterType.Melee ? delayAttack = delayAttackMeele : delayAttack = delayAttackRange;
            actionSequence = new List<(float, string, Action)>
            {
                (0f, "Preparation", ActiveGameOvbjectImage),
                (delayInPreparation, "Preparation", null),
                (attackTime, "Preparing for the attack", null),
                (delayAttack, "Preparing for the attack", UnActiveGameOvbjectImage)
            };

            StartCoroutine(IE());

            IEnumerator IE()
            {
                var a = 1;
                while (a > 0)
                {
                    foreach (var (delay, actionName, action) in actionSequence)
                    {
                        if (coroutineActionDelay != null)
                            StopCoroutine(coroutineActionDelay);

                        yield return coroutineActionDelay = StartCoroutine(IEDelayAction(delay, actionName, characterUI.image, action));
                    }

                    OnActionCompleted();
                }

              
            }
        }

        
        #endregion
    }
}