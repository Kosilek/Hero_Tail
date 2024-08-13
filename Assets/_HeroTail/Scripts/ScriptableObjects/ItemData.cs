using UnityEngine;
using Kosilek.Enum;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Kosilek.Data
{
    [CreateAssetMenu(fileName = "New Item Data", menuName = "Item Data", order = 51)]
    public class ItemData : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
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
          

           
        }
    }
#endif
}