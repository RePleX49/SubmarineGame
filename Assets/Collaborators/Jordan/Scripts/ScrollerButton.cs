using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollerButton : MonoBehaviour
{

    public int tagUpDown;
    public SymbolScroller scrollObject;


    public void TryButton()
    {
        //Debug.Log("Call");
        scrollObject.ChangeSymbol(tagUpDown);
    }

}
