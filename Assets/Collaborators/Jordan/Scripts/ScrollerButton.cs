using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollerButton : MonoBehaviour
{
    [Header("Button Settings")]
    [Tooltip("1 for Up Scrolling Button, -1 for Down")]
    public int tagUpDown;

    [Tooltip("The object with the Symbol Scroller Script that this button is linked to")]
    public SymbolScroller scrollObject;


    public void TryButton()
    {
        scrollObject.ChangeSymbol(tagUpDown);
    }

}
