using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPuzzleController : MonoBehaviour
{
    [Header("Input Settings")]

    public int correctInputSize;
    [SerializeField] private int correctInputMin;
    [SerializeField] private int correctInputMax;
    //An array containing the series of correct inputs for the puzzle in int form
    [SerializeField] public int[] correctInput;

    //An array to hold references to the objects (scripts) providing the inputs
    [SerializeField] private GameObject[] inputs;

    //An array to hold the objects displaying the clues, this script will populate them with the correct answers
    [SerializeField] private GameObject[] clues;

    [SerializeField] private TabletData[] allSymbolsObj;

    [SerializeField] private PipeFlow[] pipes;

    //An array holding all possible symbols
    [SerializeField] private Material[] allSymbols;

    public GameObject doorHolder;

    public GameObject doorTarget;

    public float doorMoveDuration;

    public float doorDropDistance;

    public int puzzleTag;

    public AudioSource incorrectBeep;

    public AudioSource correctBeep;

    public EndDoormanager doorManager;

    public StatueJuice statueJuice;

    public AudioSource doorOpenSound;

    public AudioSource pipeSound;
    public AudioSource pipeCorrectSound;

    public bool hasFinished = false;

    // Start is called before the first frame update
    void Awake()
    {

        //correctInput = new int[correctInputSize];
        if (puzzleTag != 1)
        {
            correctInput = Systems.randomSeeding.SetUpArrayBySeed(correctInput, correctInputMin, correctInputMax);
        }
        else
        {
            correctInput = Systems.randomSeeding.SetUpArrayBySeed(correctInput, correctInputMin, correctInputMax, 1);
        }


        if (puzzleTag == 0 || puzzleTag == -1)
        {
            SetUpClues();
        }

        if (puzzleTag == 2 || puzzleTag == -2)
        {
            SetUpClueRot();
        }
    }

    //Populate the clues with the correct symbols for the solution
    private void SetUpClues ()
    {
        for (int i = 0; i < clues.Length; i++)
        {
            Debug.Log("NUMBER: " + (correctInput[i] - 1) + "SYMBOL: " + allSymbolsObj[i].symbolMats[correctInput[i] - 1]);
            clues[i].GetComponent<MeshRenderer>().material = allSymbolsObj[i].symbolMats[correctInput[i] - 1];
            //clues[i].GetComponent<MeshRenderer>().material = allSymbols[correctInput[i]];
            //set the texture of each clue to the corresponding correct symbol 
        }
        
    }

    private void SetUpClueRot()
    {
        for (int i = 0; i < clues.Length; i++)
        {
            Vector3 clueRot = new Vector3 (clues[i].transform.localEulerAngles.x, clues[i].transform.localEulerAngles.y, 0);
            clueRot.z = correctInput[i] * 45;
            clues[i].transform.localEulerAngles = clueRot;
            //clues[i].transform.eulerAngles = new Vector3(0, 0, 180);
            //Debug.Log("Correct: " + correctInput[i] + "  Rot: " + correctInput[i] * 45 + "  ActualRot: " + clues[i].transform.eulerAngles);

            clues[i].GetComponent<MeshRenderer>().material = allSymbolsObj[i % 2].symbolMats[(int)inputs[i].GetComponent<SymbolRotater>().currentSymbol];
            inputs[i].GetComponent<MeshRenderer>().material = allSymbolsObj[i % 2].symbolMats[(int)inputs[i].GetComponent<SymbolRotater>().currentSymbol];
            //clues[i].GetComponent<MeshRenderer>().material = allSymbols[i];



            //clues[i].GetComponent<MeshRenderer>().material = allSymbols[correctInput[i]];
            //set the texture of each clue to the corresponding correct symbol 
        }
    }


    //check if each of the input objects is displaying the correct clue in the correct order
    public void CheckInput()
    {
        bool correct = true;

        for (int i = 0; i < correctInput.Length; i++)
        {
            SymbolScroller inputScriptScroll = inputs[i].GetComponent<SymbolScroller>();
            SymbolRotater inputScriptRot = inputs[i].GetComponent<SymbolRotater>();

            if (inputScriptScroll)
            {
                //if any inputs are not correct, the entire answer is rejected
                if (correctInput[i] != inputScriptScroll.GetCurrentSymbol())
                {
                    correct = false;
                    break;
                }
            }
            else
            {
                if (correctInput[i] != inputScriptRot.GetCurrentRot())
                {
                    correct = false;
                    break;
                }
            }
        }

        //the inputs are correct
        if (correct && !hasFinished)
        {
            hasFinished = true;
            OpenDoor();
            if (puzzleTag == 2)
            {
                statueJuice.isPuzzleComplete = true;
            }
        }
        else
        {
            //the inputs are incorrect
            Debug.Log("Incorrect Combination.");
            if (!incorrectBeep.isPlaying)
            {
                incorrectBeep.Play();
            }
        }
    }

    //To be implemented in the future, open the door and load the next area
    private void OpenDoor() {
        correctBeep.Play();
        Debug.Log("Open the Door! You Win!!");

        if (puzzleTag < 1)
        {
            Vector3 targetEnd = doorHolder.transform.localPosition + new Vector3(0f, -doorDropDistance, 0f);

            StartCoroutine(Systems.transforms.DoorLerp(doorHolder.transform, targetEnd, doorMoveDuration));
            if (!doorOpenSound.isPlaying)
            {
                doorOpenSound.Play();
            }
        }
        else
        {
            doorManager.CompletePuzzle(puzzleTag);
            StartCoroutine(ActivatePipes());
            pipeSound.Play();
            pipeCorrectSound.Play();
            //open main door
        }

    }

    IEnumerator ActivatePipes()
    {
        foreach(PipeFlow pipe in pipes)
        {
            pipe.FillPipe();

            yield return new WaitForSeconds(1.0f);
        }

        yield return null;
    }

}
