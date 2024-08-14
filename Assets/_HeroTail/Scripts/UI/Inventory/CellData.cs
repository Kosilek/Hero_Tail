using Kosilek.Enum;
using Kosilek.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellData : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textCount;
    private int count = 0;
    [SerializeField]
    private Button button;
    internal Item item;


    internal void AddItem(int count, Item item)
    {
        this.count += count;
        image.enabled = true;
        textCount.enabled = true;
        this.item = item;
        UpdateUiImage(item.itemData.sprite);
        UpdateUiText(count);
        button.onClick.AddListener(ActionItem);
        button.interactable = true;
    }

    internal void DeleteItem()
    {
        count = 0;
        image.enabled = false;
        textCount.enabled = false;
        button.onClick.RemoveAllListeners();
        button.interactable = false;
        item = null;
    }

    private void UseItem(int count)
    {

    }

    internal void UpdateUiImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    internal void UpdateUiText(int count)
    {
        this.count += count;
        textCount.text = count.ToString();
    }

    private void ActionItem()
    {
        Action action = item.itemData.itemType == ItemType.Potion ? ActionItemPotion : ActionItemCharacter;
        action?.Invoke();
    }

    private void ActionItemCharacter()
    {

    }

    private void ActionItemPotion()
    {
        if (count > 1)
        {
            UpdateUiText(-1);
            LevelManager.Instance.player.health.Healing(item.itemData.healing);
            LevelManager.Instance.player.StartAnimHealing();
        }
        else
        {
            LevelManager.Instance.player.health.Healing(item.itemData.healing);
            LevelManager.Instance.player.StartAnimHealing();
            DeleteItem();
        }
    }
}
