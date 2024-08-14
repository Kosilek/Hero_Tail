using Kosilek.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCanvas : Window
{
    [SerializeField] private List<CellData> cellInventory;
    [SerializeField] private List<CellData> cellCharacter;
    [SerializeField] private Button exitButton;
    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < cellInventory.Count; i++)
            cellInventory[i].DeleteItem();
        for (int i = 0; i < cellCharacter.Count; i++)
            cellCharacter[i].DeleteItem();
        AddListenerButton();
    }

    private void AddListenerButton()
    {
        exitButton.onClick.AddListener(Close);
    }

    

    internal void AddItemList(Item item)
    {
        Action<Item> action = item.itemData.isCharacterItem ? AddItemListCharacter : AddItemListPotion;
        action?.Invoke(item);
    }

    private void AddItemListCharacter(Item item)
    {
        var isAdd = false;

        for (int i = 0; i < cellInventory.Count; i++)
        {
            if (cellInventory[i].item == null)
            {
                isAdd = true;
                cellInventory[i].AddItem(1, item);
                return;
            }
        }
    }

    private void AddItemListPotion(Item item)
    {
        var isAdd = false;

        for (int i = 0; i < cellInventory.Count; i++)
        {
            if (cellInventory[i].item == null || cellInventory[i].item.itemData.id == item.itemData.id)
            {
                isAdd = true;
                cellInventory[i].AddItem(1, item);
                return;
            }
        }
    }

}
