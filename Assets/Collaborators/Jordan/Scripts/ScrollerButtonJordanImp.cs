using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD:Assets/Collaborators/Jordan/Scripts/ScrollerButtonJordanImp.cs
public class ScrollerButtonJordanImp : MonoBehaviour
=======
public class ScrollerButton : ButtonScript
>>>>>>> b1ccafcc49fbbc00d06e25fdd49ce7c4cc45dc8c:Assets/Collaborators/Jordan/Scripts/ScrollerButton.cs
{
    [Header("1 for Up Scrolling Button, -1 for Down")]
    [Header("Button Settings")]
    public int tagUpDown;

    [Header("Leave the non used one blank")]
    [Header("The object with the Symbol Scroller or Rotater Script that this button is linked to")]
    public SymbolScroller scrollObject;
    public SymbolRotaterJordanImp rotateObject;

    public override void UseButton()
    {
        if (scrollObject)
        {
            scrollObject.ChangeSymbol(tagUpDown);
        }
        else if(rotateObject)
        {
            rotateObject.ChangeRot(tagUpDown);
        }
    }
}
