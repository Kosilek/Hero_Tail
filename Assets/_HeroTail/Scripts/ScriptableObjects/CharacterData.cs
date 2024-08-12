using Kosilek.Enum;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Kosilek.Data
{
    [CreateAssetMenu(fileName = "New Character Data", menuName = "Character Data", order = 51)]
    public class CharacterData : ScriptableObject
    {
        #region MainValues
        internal PlayerType playerType;
        internal string characterName;
        internal int health;
        internal int armor;
        internal int damage;
        internal float delayInPreparation;
        internal float attackTime;
        internal float delayInChangingWeapons;
        internal float delayAttackMeele; //Player and AI
        internal float delayAttackRange; // Player and AI
        internal CharacterType characterType; //AI
        internal int chanceOfAppearance; // AI
        internal NPCType npcType; //AI
        #endregion
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(CharacterData))]
    public class CharacterDataEditor : Editor
    {
        CharacterData characterData;
        static bool showTileEditor = false;

        public void OnEnable()
        {
            characterData = (CharacterData)target;
        }

        public override void OnInspectorGUI()
        {
            characterData.playerType = (PlayerType)EditorGUILayout.EnumPopup("Player Type", characterData.playerType);
            characterData.characterName = EditorGUILayout.TextField("Name", characterData.characterName);
            characterData.health = EditorGUILayout.IntField("Health", characterData.health);
            characterData.armor = EditorGUILayout.IntField("Armor", characterData.armor);
            characterData.damage = EditorGUILayout.IntField("Damage", characterData.damage);
            characterData.delayInPreparation = EditorGUILayout.FloatField("Delay In Preparation", characterData.delayInPreparation);
            characterData.attackTime = EditorGUILayout.FloatField("Attack Time", characterData.attackTime);
            characterData.delayInChangingWeapons = EditorGUILayout.FloatField("Delay In Changing Weapons", characterData.delayInChangingWeapons);

            if (characterData.playerType == PlayerType.AI)
            {
                characterData.characterType = (CharacterType)EditorGUILayout.EnumPopup("Character Type", characterData.characterType);
                characterData.npcType = (NPCType)EditorGUILayout.EnumPopup("NPC TYPE", characterData.npcType);
                characterData.chanceOfAppearance = EditorGUILayout.IntField("Chance Of Appearance", characterData.chanceOfAppearance);

                if (characterData.characterType == CharacterType.Melee)
                    characterData.delayAttackMeele = EditorGUILayout.FloatField("Meele Delay", characterData.delayAttackMeele);
                else
                    characterData.delayAttackRange = EditorGUILayout.FloatField("Range Delay", characterData.delayAttackRange);
            }
            else
            {
                characterData.delayAttackMeele = EditorGUILayout.FloatField("Meele Delay", characterData.delayAttackMeele);
                characterData.delayAttackRange = EditorGUILayout.FloatField("Range Delay", characterData.delayAttackRange);
            }
        }
    }
#endif
}