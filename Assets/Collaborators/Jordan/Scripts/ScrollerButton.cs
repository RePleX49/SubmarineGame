using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollerButton : MonoBehaviour
{
    [Header("1 for Up Scrolling Button, -1 for Down")]
    [Header("Button Settings")]
    public int tagUpDown;

    [Header("Leave the non used one blank")]
    [Header("The object with the Symbol Scroller or Rotater Script that this button is linked to")]
    public SymbolScroller scrollObject;
    public SymbolRotater rotateObject;


    public void TryButton()
    {
        if (scrollObject)
        {
            scrollObject.ChangeSymbol(tagUpDown);
        }
        else
        {
            rotateObject.ChangeRot(tagUpDown);
        }
    }

}
