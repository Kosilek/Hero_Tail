using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kosilek.Data
{
    [CreateAssetMenu(fileName = "New XP Data", menuName = "XP Data", order = 51)]
    public class XPData : ScriptableObject
    {
        [SerializeField]
        internal int updateHealth;
        [SerializeField]
        internal int updateArmor;
        [SerializeField]
        internal int updateDamage;
    }
}
