using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletManager : MonoBehaviour
{
    // array with the correct symbols for each structure
    public Symbols[] symbolOrder = new Symbols[2];
    int answerIndex = 0;

    [HideInInspector]
    public Symbols answerSymbol;

    public GameObject[] symbolStructures;
    public TabletButton[] symbolButtons = new TabletButton[4];
    public TabletData tabletData;
    public GameObject completionMessage;

    // Start is called before the first frame update
    void Start()
    {
        answerSymbol = symbolOrder[answerIndex];
        symbolStructures[answerIndex].SetActive(true);
        ChangeSymbols();
    }

    void ChangeSymbols()
    {
        List<int> usedSymbols = new List<int>();

        // switch a random button to the correct symbol
        int randButton = Random.Range(0, symbolButtons.Length - 1);
        symbolButtons[randButton].activeSymbol = symbolOrder[answerIndex];
        usedSymbols.Add((int)symbolOrder[answerIndex]);
        symbolButtons[randButton].ChangeImage();

        Debug.Log(symbolButtons[randButton] + " Symbol is " + symbolButtons[randButton].activeSymbol);

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
            } while (symbolButtons[i].activeSymbol == (Symbols)randIndex || usedSymbols.Contains(randIndex));

            symbolButtons[i].activeSymbol = (Symbols)randIndex;
            usedSymbols.Add(randIndex);
            symbolButtons[i].ChangeImage();

            Debug.Log(symbolButtons[i] + " Symbol is " + symbolButtons[i].activeSymbol);
        }
    }

    public void UpdatePuzzle()
    {
        symbolStructures[answerIndex].SetActive(false);
        answerIndex++;

        if(answerIndex > symbolStructures.Length - 1)
        {
            //Puzzle Complete!
            Debug.Log("Puzzle Complete");
            completionMessage.SetActive(true);
            return;
        }

        ChangeSymbols();
        answerSymbol = symbolOrder[answerIndex];
        symbolStructures[answerIndex].SetActive(true);
    }
}

public enum Symbols
{
    Six,
    Swirl,
    Lightning,
    Clam,
    Seahorse,
    Abstract,
    Count
};
