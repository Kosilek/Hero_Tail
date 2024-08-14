using Kosilek;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kosilek.UI;

public class CanvasManager : SimpleSingleton<CanvasManager>
{
    [Header("Canvas")]
    public GameCanvas gameCanvas;
    public ReaspwnCanvas respawnCanvas;
    public InventoryCanvas inventoryCanvas;

    [Header("Other")]
    public Camera MainCamera;

    [Header("CharacterUI")]
    public XPUI xpUI;
}
