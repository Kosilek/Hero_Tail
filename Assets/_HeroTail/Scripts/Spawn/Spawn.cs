using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Kosilek.Manager;
using Kosilek.Characters;
using static UnityEngine.EventSystems.EventTrigger;

public class Spawn : MonoBehaviour
{
    public bool isPlayer;

    [SerializeField] private Transform parentTransform;

    public void SpawnCharacters()
    {
        Action action = isPlayer ? SpawnPlayer : SpawnEnemy;
        action?.Invoke();
    }

    private void SpawnPlayer()
    {
        LevelManager.Instance.player = Instantiate(ContainerManager.Instance.player.gameObject, transform.position, transform.rotation).GetComponent<PlayerCntr>();
        LevelManager.Instance.player.transform.SetParent(parentTransform);
        LevelManager.Instance.player.InitializationCharactersData(false);
    }

    private void SpawnEnemy()
    {
        var enemy = ChoosingAMonster();
        LevelManager.Instance.enemy = Instantiate(enemy.gameObject, transform.position, transform.rotation).GetComponent<Enemy>();
        LevelManager.Instance.enemy.transform.SetParent(parentTransform);
        LevelManager.Instance.enemy.InitializationCharactersData(false);
    }

    private Enemy ChoosingAMonster()
    {
        int totalChance = ContainerManager.Instance.containerEnemy.Sum(pair => pair.chanceOfAppearance);
        var randomNumber = UnityEngine.Random.Range(0f, totalChance);
        foreach (var container in ContainerManager.Instance.containerEnemy)
        {
            randomNumber -= container.chanceOfAppearance;
            if (randomNumber <= 0)
            {
                Debug.Log("Random = " + container.chanceOfAppearance + " name + " + container.enemy.name);
                return container.enemy;
            }
        }
        Debug.LogError("Error: Enemy is not");
        return null;
    }
}
