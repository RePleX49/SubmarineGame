using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalButton : ButtonScript
{
    public int[] lockRotationAnswer;
    public SymbolRotater[] locks;
    public GameObject finalText;

    public ClockManager[] dials;

    public EndDoormanager finalDoorScript;
    public EndingCutscene endingCutscene;

    public bool isPlayerA;

    [SerializeField] private int correctInputMin;
    [SerializeField] private int correctInputMax;
    [SerializeField] private int[] correctInputHolder = new int[16];
    [SerializeField] private PipeFlow[] pipes;

    public AudioSource correctSound;
    public AudioSource incorrectSound;

    void Awake()
    {

        correctInputHolder = Systems.randomSeeding.SetUpArrayBySeed(correctInputHolder, correctInputMin, correctInputMax, 4, 2, 4, 1);
        //if (isPlayerA)
        //{
        //    correctInputHolder = Systems.randomSeeding.SetUpArrayBySeed(correctInputHolder, correctInputMin, correctInputMax, 4, 2);
        //}
        //else
        //{
        //    correctInputHolder = Systems.randomSeeding.SetUpArrayBySeed(correctInputHolder, correctInputMin, correctInputMax, 4, 1);
        //}

        for (int i = 0; i < dials.Length; i++)
        {
          
            if (isPlayerA) 
            {
                dials[i].answerSymbol = (CreatureSymbols)correctInputHolder[0 + (4 * i)];
                dials[i].clueSymbol = (CreatureSymbols)correctInputHolder[1 + (4 * i)];
                dials[i].revealSymbol = (CreatureSymbols)correctInputHolder[2 + (4 * i)];
                dials[i].correctStatueIndex = correctInputHolder[3 + (4 * i)];
                locks[i].currentSymbol = (CreatureSymbols)correctInputHolder[3 + (4 * i)];
                lockRotationAnswer[i] = correctInputHolder[2 + (4 * i)];
                locks[i].GetComponent<MeshRenderer>().material = locks[i].symbolsData.symbolMats[(int)locks[i].currentSymbol];
            }
            else
            {
                //Debug.Log("ANSWER: " + (CreatureSymbols)correctInputHolder[1 + (4 * i)]);
                //Debug.Log("ROTATOR: " + (CreatureSymbols)correctInputHolder[2 + (4 * i)]);
                //Debug.Log("ROTATOR ANGLE: " + correctInputHolder[3 + (4 * i)]);
                dials[i].answerSymbol = (CreatureSymbols)correctInputHolder[1 + (4 * i)];
                dials[i].clueSymbol = (CreatureSymbols)correctInputHolder[0 + (4 * i)];
                dials[i].revealSymbol = (CreatureSymbols)correctInputHolder[3 + (4 * i)];
                dials[i].correctStatueIndex = correctInputHolder[2 + (4 * i)];
                locks[i].currentSymbol = (CreatureSymbols)correctInputHolder[2 + (4 * i)]; 
                lockRotationAnswer[i] = correctInputHolder[3 + (4 * i)];
                locks[i].GetComponent<MeshRenderer>().material = locks[i].symbolsData.symbolMats[(int)locks[i].currentSymbol];
            }
        }
    }

    public override void UseButton()
    {
        bool bCorrectCombination = true;

        for(int i = 0; i < locks.Length; i++)
        {
            if(locks[i].GetCurrentRot() != lockRotationAnswer[i])
            {
                bCorrectCombination = false;
                incorrectSound.Play();
                break;
            }
        }

        if(bCorrectCombination)
        {
            // TODO activate final door/ cutscene
            //finalText.SetActive(true); 
            correctSound.Play();         
            StartCoroutine(ActivatePipes());
            finalDoorScript.OpenDoor();
            endingCutscene.canWin = true;

        }
    }

    IEnumerator ActivatePipes()
    {
        foreach (PipeFlow pipe in pipes)
        {
            pipe.FillPipe();

            yield return new WaitForSeconds(1.0f);
        }

        yield return null;
    }
}
