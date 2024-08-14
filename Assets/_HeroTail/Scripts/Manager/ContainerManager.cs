using Kosilek.Characters;
using Kosilek.Data;
using Kosilek.Struct;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kosilek.Manager
{
    public class ContainerManager : SimpleSingleton<ContainerManager>
    {
        public PlayerCntr player;
        public List<ContainerEnemy> containerEnemy;
        public List<Item> items;

        protected override void Awake()
        {
            base.Awake();
            for (int i = 0; i < containerEnemy.Count; i++)
            {
                containerEnemy[i].enemy.InitializationCharactersData(true);
                containerEnemy[i].chanceOfAppearance = containerEnemy[i].enemy.GetComponent<Enemy>().chanceOfAppearance;
            }
        }
    }
}
