using UnityEngine;
using Kosilek.Enum;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Kosilek.Data
{
    [CreateAssetMenu(fileName = "New Item Data", menuName = "Item Data", order = 51)]
    public class ItemData : ScriptableObject
    {
        public ItemType itemType;
        public PotionType pointerType;
        public CharacterType characterType;
        public int armor;
        public int health;
        public int healing;
        public int damage;
        public float delayOfAttak;
        public bool isStacked = false;
        public int maxStacked;
        public Sprite sprite;
        public string nameItem;
        public int id;
        public bool isCharacterItem;
        public int chanceOfAppearance;
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(ItemData))]
    public class ItemDataEditor : Editor
    {
        ItemData itemData;
        static bool showTileEditor = false;

        public void OnEnable()
        {
            itemData = (ItemData)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            SerializedProperty itemType = serializedObject.FindProperty("itemType");
            EditorGUILayout.PropertyField(itemType);
            SerializedProperty spriteProp = serializedObject.FindProperty("sprite");
            EditorGUILayout.PropertyField(spriteProp);
            SerializedProperty nameItem = serializedObject.FindProperty("nameItem");
            EditorGUILayout.PropertyField(nameItem);
            SerializedProperty id = serializedObject.FindProperty("id");
            EditorGUILayout.PropertyField(id);
            SerializedProperty isCharacterItem = serializedObject.FindProperty("isCharacterItem");
            EditorGUILayout.PropertyField(isCharacterItem);
            switch (itemData.itemType)
            {
                case ItemType.Helmet:
                    StateArmor();
                    break;
                case ItemType.Armor:
                    StateArmor();
                    break;
                case ItemType.Kneepads:
                    StateArmor();
                    break;
                case ItemType.Shoes:
                    StateArmor();
                    break;
                case ItemType.Weapons:
                    SerializedProperty damage = serializedObject.FindProperty("damage");
                    EditorGUILayout.PropertyField(damage);
                    SerializedProperty delayOfAttak = serializedObject.FindProperty("delayOfAttak");
                    EditorGUILayout.PropertyField(delayOfAttak);
                    SerializedProperty characterType = serializedObject.FindProperty("characterType");
                    EditorGUILayout.PropertyField(characterType);
                    break;
                case ItemType.Potion:
                    itemData.pointerType = (PotionType)EditorGUILayout.EnumPopup("PotionType", itemData.pointerType);
                    itemData.healing = EditorGUILayout.IntField("healing", itemData.healing);
                    itemData.maxStacked = EditorGUILayout.IntField("maxStacked", itemData.maxStacked);
                    itemData.isStacked = true;
                    break;
            }
            SerializedProperty chanceOfAppearance = serializedObject.FindProperty("chanceOfAppearance");
            EditorGUILayout.PropertyField(chanceOfAppearance);
            serializedObject.ApplyModifiedProperties();
        }

        private void StateArmor()
        {
            SerializedProperty armor = serializedObject.FindProperty("armor");
            EditorGUILayout.PropertyField(armor);
            SerializedProperty health = serializedObject.FindProperty("health");
            EditorGUILayout.PropertyField(health);
        }
    }
#endif
}