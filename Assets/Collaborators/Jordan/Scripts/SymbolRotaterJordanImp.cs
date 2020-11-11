using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolRotaterJordanImp : MonoBehaviour
{
    [Header("(for future randomization)")]
    [Header("Put all the symbols of the color you want in here")]
    [Header("What's up let's set this baby up")]
    [SerializeField] private Material[] symbols;
    [Header("Set this number to the symbol you want the button to show")]
    [SerializeField] private CreatureSymbols currentSymbol = CreatureSymbols.Angler;

    private GameObject rotHolder;
    private Vector3 symbolRot = Vector3.zero;

    [Header("Set this baby to a number 0-7 if you want the symbol to start at a rotation")]
    [SerializeField] private int currentRot = 0;
    //private Material material;

    // Start is called before the first frame update
    void Start()
    {
        //sets to the correct symbol to display
        gameObject.GetComponent<MeshRenderer>().material = symbols[(int)currentSymbol];
        symbolRot.z = currentRot * 45.0f;
        rotHolder = gameObject.transform.parent.gameObject;
        rotHolder.transform.localEulerAngles = symbolRot;
    }

    //takes in the tag from which button was pressed
    public void ChangeRot(int tagClockCounter)
    {
        //changes the current rot which can be used to figure out if the button is set to the correct orientation
        currentRot += tagClockCounter;
        
        // modulo to loop through values
        currentRot = currentRot % 8;
        currentRot = currentRot < 0 ? currentRot + 8 : currentRot;

        //actually set the rotation in increments of 45 degrees
        symbolRot.z = currentRot * 45;
        rotHolder.transform.localEulerAngles = symbolRot;
    }

    //getter for the manager script
    public int GetCurrentRot()
    {
        return currentRot;
    }

    public enum CreatureSymbols
    { 
        Angler,
        Fish,
        Snail,
        Star,
        Jellyfish,
        Crab,
        count
    };
}
