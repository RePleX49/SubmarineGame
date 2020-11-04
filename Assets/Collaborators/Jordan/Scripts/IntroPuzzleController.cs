using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPuzzleController : MonoBehaviour
{


    [SerializeField] private int[] correctInput;

    [SerializeField] private SymbolScroller[] inputs;

    [SerializeField] private GameObject[] clues;

    [SerializeField] private Texture[] allSymbols;

    // Start is called before the first frame update
    void Start()
    {
        SetUpClues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetUpClues ()
    {
        for (int i = 0; i < clues.Length; i++)
        {
            clues[i].GetComponent<MeshRenderer>().material.SetTexture("_MainTex", allSymbols[correctInput[i]]);
        }
    }


    public void CheckInput()
    {
        bool correct = true;

        for (int i = 0; i < correctInput.Length; i++)
        {
            if (correctInput[i] != inputs[i].GetCurrentSymbol())
            {
                correct = false;
                break;
            }
        }

        if (correct)
        {
            OpenDoor();
        }
        else
        {
            Debug.Log("Incorrect Combination.");
        }
    }

    private void OpenDoor() {
        Debug.Log("Open the Door! You Win!!");
    
    }


}
