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
        public PlayerType playerType;
        public string characterName;
        public int health;
        public int armor;
        public int damage;
        public float delayInPreparation;
        public float attackTime;
        public float delayInChangingWeapons;
        public float delayAttackMeele; //Player and AI
        public float delayAttackRange; // Player and AI
        public CharacterType characterType; //AI
        public int chanceOfAppearance; // AI
        public NPCType npcType; //AI
        public int XP; //AI
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
            serializedObject.Update();
            SerializedProperty playerType = serializedObject.FindProperty("playerType");
            EditorGUILayout.PropertyField(playerType);
            SerializedProperty characterName = serializedObject.FindProperty("characterName");
            EditorGUILayout.PropertyField(characterName);
            SerializedProperty health = serializedObject.FindProperty("health");
            EditorGUILayout.PropertyField(health);
            SerializedProperty armor = serializedObject.FindProperty("armor");
            EditorGUILayout.PropertyField(armor);
            SerializedProperty damage = serializedObject.FindProperty("damage");
            EditorGUILayout.PropertyField(damage);
            SerializedProperty delayInPreparation = serializedObject.FindProperty("delayInPreparation");
            EditorGUILayout.PropertyField(delayInPreparation);
            SerializedProperty attackTime = serializedObject.FindProperty("attackTime");
            EditorGUILayout.PropertyField(attackTime);

            if (characterData.playerType == PlayerType.AI)
            {
                SerializedProperty characterType = serializedObject.FindProperty("characterType");
                EditorGUILayout.PropertyField(characterType);
                SerializedProperty npcType = serializedObject.FindProperty("npcType");
                EditorGUILayout.PropertyField(npcType);
                SerializedProperty chanceOfAppearance = serializedObject.FindProperty("chanceOfAppearance");
                EditorGUILayout.PropertyField(chanceOfAppearance);
                SerializedProperty XP = serializedObject.FindProperty("XP");
                EditorGUILayout.PropertyField(XP);

                if (characterData.characterType == CharacterType.Melee)
                {
                    SerializedProperty delayAttackMeele = serializedObject.FindProperty("delayAttackMeele");
                    EditorGUILayout.PropertyField(delayAttackMeele);
                }
                else
                {
                    SerializedProperty delayAttackRange = serializedObject.FindProperty("delayAttackRange");
                    EditorGUILayout.PropertyField(delayAttackRange);
                }
              
            }
            else
            {
                SerializedProperty delayInChangingWeapons = serializedObject.FindProperty("delayInChangingWeapons");
                EditorGUILayout.PropertyField(delayInChangingWeapons);
                SerializedProperty delayAttackMeele = serializedObject.FindProperty("delayAttackMeele");
                EditorGUILayout.PropertyField(delayAttackMeele);
                SerializedProperty delayAttackRange = serializedObject.FindProperty("delayAttackRange");
                EditorGUILayout.PropertyField(delayAttackRange);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}