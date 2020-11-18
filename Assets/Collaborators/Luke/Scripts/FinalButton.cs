using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalButton : ButtonScript
{
    public int[] lockRotationAnswer;
    public SymbolRotater[] locks;
    public GameObject finalText;

    public override void UseButton()
    {
        bool bCorrectCombination = true;

        for(int i = 0; i < locks.Length; i++)
        {
            if(locks[i].GetCurrentRot() != lockRotationAnswer[i])
            {
                bCorrectCombination = false;
                break;
            }
        }

        if(bCorrectCombination)
        {
            // TODO activate final door/ cutscene
            finalText.SetActive(true);
        }
    }
}
