using Kosilek.Characters;
using System;
using UnityEngine;

namespace Kosilek.Struct
{
    [Serializable]
    public class ContainerEnemy
    {
        public Enemy enemy;
        //[HideInInspector]
        public int chanceOfAppearance;
    }
}