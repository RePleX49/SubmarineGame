using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : ButtonScript
{
    public ClockDoor[] symbolHolders;
    public SymbolRotater[] rotatorSymbols;

    // Statue to point at ex: statue 4, statue 7, statue 2, statue 0
    public int[] statueOrder;
    public int[] answerOrder;

    public MeshRenderer clueDisplay;
    public Material[] clueMats;
    public Material[] answerMats;
    public Material[] revealMats;
    int answerIndex = 0;

    int currentRot = 0;
    Vector3 dialRotation = Vector3.zero;

    public TabletData images;

    bool bUpdated = false;

    // Start is called before the first frame update
    void Start()
    {
        dialRotation.y = currentRot * 45;

        UpdateSymbols();
        clueDisplay.material = clueMats[answerIndex];
    }

    public override void UseButton()
    {
        //changes the current rot which can be used to figure out if the button is set to the correct orientation
        currentRot++;

        // modulo to loop through values
        currentRot = currentRot % 8;

        //actually set the rotation in increments of 45 degrees
        dialRotation.y = currentRot * 45;
        transform.eulerAngles = dialRotation;
    }

    public void TryCurrentRot()
    {
        if (bUpdated && statueOrder[answerIndex] == currentRot)
        {
            bUpdated = false;
            Debug.Log("Change symbol");
            symbolHolders[currentRot].ChangeSymbol(revealMats[answerIndex]);
            //answerIndex++;
        }
    }

    // button press to update clue and statue symbols
    public void UpdateClue()
    {
        if(!bUpdated && rotatorSymbols[answerIndex].GetCurrentRot() == answerOrder[answerIndex])
        {
            if(answerIndex == answerOrder.Length - 1)
            {
                Debug.Log("Congratulations Puzzle Solved");
                return;
            }

            answerIndex++;
            UpdateSymbols();
        } 
    }

    void UpdateSymbols()
    {
        List<int> usedSymbols = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };

        // gets the correct door direction from current answer index
        int correctIndex = statueOrder[answerIndex];

        // Remove index from list to prevent repeats
        usedSymbols.Remove(statueOrder[answerIndex]);
        
        // assign the right symbol material based on answer index
        symbolHolders[correctIndex].ChangeSymbol(answerMats[answerIndex]);

        clueDisplay.material = clueMats[answerIndex];

        // switch the remaining doors to random incorrect symbols
        for (int i = 0; i < symbolHolders.Length; i++)
        {
            if (i == correctIndex)
                continue;

            int random = Random.Range(0, usedSymbols.Count);

            symbolHolders[i].ChangeSymbol(images.symbolMats[usedSymbols[random]]);

            usedSymbols.RemoveAt(random);
        }

        bUpdated = true;
    }
}
