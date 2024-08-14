using Kosilek.Data;
using Kosilek.Enum;
using Kosilek.Manager;
using Kosilek.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Kosilek.Characters
{
    public class Character : MonoBehaviour
    {
        #region Data
        [SerializeField] internal CharacterData characterData;
        #endregion end Data

        #region ScriptsComponent
        [SerializeField] internal CharacterUI characterUI;
        [SerializeField] internal Health health;
        #endregion end ScriptsComponent

        #region Controll Battle
        internal bool isBattle = false;
        #endregion

        #region Main State
        protected PlayerType playerType;
        protected CharacterType selectedWeapons;
        protected string characterName;
        internal int armor;
        internal int damage;
        protected float delayInPreparation;
        protected float attackTime;
        protected float delayInChangingWeapons;
        protected float delayAttackMeele; //Player and AI
        protected float delayAttackRange; // Player and AI
        protected CharacterType characterType; //AI
        internal int chanceOfAppearance; // AI
        internal int xp; //AI
        #endregion end Main State

        #region List Action
        private List<(float delay, string actionName, Action action)> actionSequence;
        private Action callAttack;
        #endregion

        #region Coroutine
        internal Coroutine coroutineActionDelay;
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

        #region Canvas
        [SerializeField] private Canvas gameCanvasCharacter;
        #endregion

        #region Only Player
        public float timerPreparation = 0f;
        internal bool isPreparation = false;
        internal bool isTimeAttack = false;
        public bool isChangingWeapon = false;
        #endregion

        #region Awake
        protected virtual void Awake()
        {
            gameCanvasCharacter.worldCamera = CanvasManager.Instance.MainCamera;
        }

        internal void InitializationCharactersData(bool isStart)
        {
            playerType = characterData.playerType;
            characterName = characterData.characterName;
            health.InitializationCharactersData(characterData.health, isStart);
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
                xp = characterData.XP;
            }

        }
        #endregion end Awake

        #region Action Character

        internal virtual void StartBattle()
        {
            Action action;
            if (playerType == PlayerType.Playe)
                action = selectedWeapons == CharacterType.Melee ? StartAnimMeeleAttack : StartAnimRangeAttack;
            else
                action = characterType == CharacterType.Melee ? StartAnimMeeleAttack : StartAnimRangeAttack;

                ActionCharacter(action);
        }

        internal virtual void StartBattle(List<(float, string, Action)> listAction)
        {
            Action action;
            if (playerType == PlayerType.Playe)
                action = selectedWeapons == CharacterType.Melee ? StartAnimMeeleAttack : StartAnimRangeAttack;
            else
                action = characterType == CharacterType.Melee ? StartAnimMeeleAttack : StartAnimRangeAttack;

            ActionCharacter(action, listAction);
        }

        protected void ActionCharacter(Action action)
        {
            var delayAttack = 0f;
            callAttack = null;
            callAttack = action;
            delayAttack = selectedWeapons == CharacterType.Melee ? delayAttack = delayAttackMeele : delayAttack = delayAttackRange;
            actionSequence = new List<(float, string, Action)>
            {
                (0f, "Preparation", ActiveGameOvbjectImage),
                (delayInPreparation, "Preparation", ActionPreparing),
                (attackTime, "Preparing for the attack", ActionTimeAttack),
                (delayAttack, "Preparing for the attack", EndActionList),
                (0f, "DamageObject", DamageObject)
            };

            StartCoroutine(IE());

            IEnumerator IE()
            {
                while (isBattle)
                {
                    foreach (var (delay, actionName, action) in actionSequence)
                    {
                        if (coroutineActionDelay != null)
                            StopCoroutine(coroutineActionDelay);

                        yield return coroutineActionDelay = StartCoroutine(IEDelayAction(delay, actionName, characterUI.image, action, null));
                    }

                    OnActionCompleted();
                }
            }
        }

        internal void ActionPreparing()
        {
            characterUI.typeActionImageColor.color = characterUI.colorPreparation;
            isPreparation = true;
            isTimeAttack = false;
            isChangingWeapon = false;
        }

        private void ActionTimeAttack()
        {
            characterUI.typeActionImageColor.color = characterUI.colorTimeAttack;
            isPreparation = false;
            isTimeAttack = true;
            isChangingWeapon = false;
        }

        protected void ActionCharacter(Action action, List<(float, string, Action)> listAction)
        {
            var delayAttack = 0f;
            callAttack = null;
            callAttack = action;
            delayAttack = selectedWeapons == CharacterType.Melee ? delayAttack = delayAttackMeele : delayAttack = delayAttackRange;
            actionSequence = listAction;

            StartCoroutine(IE());

            IEnumerator IE()
            { 
                while (isBattle)
                {
                    foreach (var (delay, actionName, action) in actionSequence)
                    {
                        if (coroutineActionDelay != null)
                            StopCoroutine(coroutineActionDelay);

                        yield return coroutineActionDelay = StartCoroutine(IEDelayAction(delay, actionName, characterUI.image, action, null));
                    }

                    OnActionCompleted();
                }
            }
        }

        protected List<(float, string, Action)> GetListActionPreparing(float delayAttack)
        {
            var actionSequence = new List<(float, string, Action)>
            {
                (delayInPreparation, "Preparation", ActionPreparing),
                (attackTime, "Preparing for the attack", ActionTimeAttack),
                (delayAttack, "Preparing for the attack", EndActionList),
                (0f, "DamageObject", DamageObject),
                (0f, "Preparation", ActiveGameOvbjectImage),
            };
            return actionSequence;
        }

        protected List<(float, string, Action)> GetListActionTimeAttack(float delayAttack)
        {
            var actionSequence = new List<(float, string, Action)>
            {
                (attackTime, "Preparing for the attack", ActionTimeAttack),
                (delayAttack, "Preparing for the attack", EndActionList),
                (0f, "DamageObject", DamageObject),
                (0f, "Preparation", ActiveGameOvbjectImage),
                (delayInPreparation, "Preparation", ActionPreparing)
            };
            return actionSequence;
        }

        internal IEnumerator IEDelayAction(float delay, string nameAction, Image image, Action call, Action endCall)
        {
            call?.Invoke();
           // Debug.Log(" = isChangingWeapon = " + isChangingWeapon " ");
            for (float i = 0; i < delay; i += Time.deltaTime)
            {
                if (!isChangingWeapon)
                    timerPreparation = i;
                image.fillAmount = i / delay;
                yield return null;
            }
            endCall?.Invoke();
            coroutineActionDelay = null;
        }

        internal IEnumerator IEDelayAction(float startValue ,float delay, string nameAction, Image image, Action call, Action endCall)
        {
            call?.Invoke();
            // Debug.Log(" = isChangingWeapon = " + isChangingWeapon " ");
            for (float i = startValue; i < delay; i += Time.deltaTime)
            {
                if (!isChangingWeapon)
                    timerPreparation = i;
                image.fillAmount = i / delay;
                yield return null;
            }
            endCall?.Invoke();
            timerPreparation = 0f;
            coroutineActionDelay = null;
        }

        private void OnActionCompleted()
        {

        }

        private void ActiveGameOvbjectImage()
        {
            characterUI.ActiveGameObjectImage(true);
            CanvasManager.Instance.gameCanvas.changingWeaponsButton.interactable = true;
        }

        private void UnActiveGameOvbjectImage()
        {
            characterUI.ActiveGameObjectImage(false);
        }


        private void EndActionList()
        {
            UnActiveGameOvbjectImage();
            CanvasManager.Instance.gameCanvas.changingWeaponsButton.interactable = false;
            callAttack?.Invoke();
        }
        #endregion end Action Character

        #region Animation
        protected void StartAnimMeeleAttack() => animator.SetTrigger(MEELE);

        protected void StartAnimRangeAttack() => animator.SetTrigger(RANGE);

        internal void StartAnimRangeHIT() => animator.SetTrigger(HIT);

        internal void StartAnimHealing() => animator.SetTrigger(HEALING);
        #endregion

        private void DamageObject()
        {
            Action action = playerType == PlayerType.Playe ? DamageEnemy : DamagePlayer;
            action?.Invoke();
            action = playerType == PlayerType.Playe ? LevelManager.Instance.enemy.StartAnimRangeHIT : LevelManager.Instance.player.StartAnimRangeHIT;
            action.Invoke();
        }

        private void DamagePlayer()
        {
            LevelManager.Instance.player.health.Damage(damage, LevelManager.Instance.player.armor, PlayerType.Playe);
        }

        private void DamageEnemy()
        {
            LevelManager.Instance.enemy.health.Damage(damage, LevelManager.Instance.enemy.armor, PlayerType.AI);
        }

        #region Drop
        internal void DropItem(Transform transform)
        {
            var random = UnityEngine.Random.Range(0, 101);

            if (random > 50)
                return;

            var item = ChoosingAMonster();
            LevelManager.Instance.dropItem = Instantiate(item.gameObject, transform.position, Quaternion.identity).GetComponent<Item>();
            CanvasManager.Instance.gameCanvas.addItemButton.gameObject.SetActive(true);
        }

        private Item ChoosingAMonster()
        {
            int totalChance = ContainerManager.Instance.items.Sum(pair => pair.itemData.chanceOfAppearance);
            var randomNumber = UnityEngine.Random.Range(0f, totalChance);
            foreach (var container in ContainerManager.Instance.items)
            {
                randomNumber -= container.itemData.chanceOfAppearance;
                if (randomNumber <= 0)
                {
                    return container;
                }
            }
            Debug.LogError("Error: Enemy is not");
            return null;
        }
        #endregion end Drop

        #region Test


        #endregion
    }
}