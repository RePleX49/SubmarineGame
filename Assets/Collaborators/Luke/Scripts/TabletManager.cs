using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletManager : MonoBehaviour
{
    // array with the correct symbols for each structure
    public Symbols[] symbolOrder = new Symbols[2];

    public TabletButton[] symbolButtons = new TabletButton[4];

    public TabletData tabletData;

    // Start is called before the first frame update
    void Start()
    {
        ChangeSymbols();
    }

    void ChangeSymbols()
    {      
        // switch a random button to the correct symbol
        int randButton = Random.Range(0, symbolButtons.Length - 1);
        symbolButtons[randButton].activeSymbol = symbolOrder[0];

        // switch the remaining buttons to random incorrect symbols
        for (int i = 0; i < symbolButtons.Length; i++)
        {
            if (i == randButton)
            {
                continue;
            }

            int randIndex = 0;
            do
            {
                randIndex = Random.Range(0, (int)Symbols.Count - 1);
            } while (symbolButtons[i].activeSymbol == (Symbols)randIndex);

            symbolButtons[i].activeSymbol = (Symbols)randIndex;
            Debug.Log(symbolButtons[i] + " Symbol is " + symbolButtons[i].activeSymbol);
        }
    }
}

public enum Symbols
{
    Symbol1,
    Symbol2,
    Symbol3,
    Symbol4,
    Symbol5,
    Symbol6,
    Symbol7,
    Symbol8,
    Count
};
