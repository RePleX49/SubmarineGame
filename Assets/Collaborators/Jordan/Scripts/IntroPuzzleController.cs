using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPuzzleController : MonoBehaviour
{
    [Header("Button Settings")]
    //An array containing the series of correct inputs for the puzzle in int form
    [SerializeField] private int[] correctInput;

    //An array to hold references to the objects (scripts) providing the inputs
    [SerializeField] private SymbolScroller[] inputs;

    //An array to hold the objects displaying the clues, this script will populate them with the correct answers
    [SerializeField] private GameObject[] clues;

    //An array holding all possible symbols
    [SerializeField] private Texture[] allSymbols;

    public GameObject doorHolder;

    public GameObject doorTarget;

    public float doorMoveDuration;

    public int puzzleTag;

    // Start is called before the first frame update
    void Start()
    {
        if (puzzleTag == 0)
        {
            SetUpClues();
        }
    }

    //Populate the clues with the correct symbols for the solution
    private void SetUpClues ()
    {
        for (int i = 0; i < clues.Length; i++)
        {
            clues[i].GetComponent<MeshRenderer>().material.SetTexture("_MainTex", allSymbols[correctInput[i]]);
            //set the texture of each clue to the corresponding correct symbol 
        }
    }


    //check if each of the input objects is displaying the correct clue in the correct order
    public void CheckInput()
    {
        bool correct = true;

        for (int i = 0; i < correctInput.Length; i++)
        {
            //if any inputs are not correct, the entire answer is rejected
            if (correctInput[i] != inputs[i].GetCurrentSymbol())
            {
                correct = false;
                break;
            }
        }

        //the inputs are correct
        if (correct)
        {
            OpenDoor();
        }
        else
        {
            //the inputs are incorrect
            Debug.Log("Incorrect Combination.");
        }
    }

    //To be implemented in the future, open the door and load the next area
    private void OpenDoor() {
        Debug.Log("Open the Door! You Win!!");

        StartCoroutine(Systems.transforms.LerpMove(doorHolder.transform, doorTarget.transform.position, 
        doorHolder.transform.rotation, doorHolder.transform.localScale, doorMoveDuration));
    
    }


}
