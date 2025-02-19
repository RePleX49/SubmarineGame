﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolScroller : MonoBehaviour
{
    [Header("This'll be the starting symbol shown on the button, 0 should be blank")]
    [Header("Hey pals let's populate locales")]
    [SerializeField] private int currentSymbol = 0;
    [Header("Put all the symbols of the color you want in here, including the blank")]
    [SerializeField] private Material[] symbols;
    private int numOfSymbols;

    public TabletData symbolsObj;
    public int puzzleTag = 0;

    //private Material material;


    // Start is called before the first frame update
    void Start()
    {
        if (puzzleTag != 0)
        {
            symbols = symbolsObj.symbolMats;
            currentSymbol = 4;
        }

        numOfSymbols = symbols.Length;
        gameObject.GetComponent<MeshRenderer>().material = symbols[currentSymbol];

    }

    //takes in the tag from which button was pressed
    public void ChangeSymbol (int tagUpDown)
    {
        //changes the current symbol
        currentSymbol += tagUpDown;

        //wrap around
        if (currentSymbol < 0)
        {
            currentSymbol = numOfSymbols - 1;

        } else if (currentSymbol >= numOfSymbols)
        {
            currentSymbol = 0;
        }

        //change to the correct material
        gameObject.GetComponent<MeshRenderer>().material = symbols[currentSymbol];

    }

    //getter for managers
    public int GetCurrentSymbol()
    {
        if (puzzleTag != 0) {
            int symbolToReturn = currentSymbol + 1;
            if (symbolToReturn > 4)
            {
                symbolToReturn = 0;
            }

            return symbolToReturn;

        }

        return currentSymbol;
    }
}
