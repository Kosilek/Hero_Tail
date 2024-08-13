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

    [Header("Other")]
    public Camera MainCamera;
}
