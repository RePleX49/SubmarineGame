using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolScroller : MonoBehaviour
{

    [SerializeField] private int currentSymbol = 0;

    [SerializeField] private int numOfSymbols;

    [SerializeField] private Texture[] symbols;

    private Material material;


    // Start is called before the first frame update
    void Start()
    {
        numOfSymbols = symbols.Length;
        material = gameObject.GetComponent<MeshRenderer>().material;
        material.SetTexture("_MainTex", symbols[currentSymbol]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeSymbol (int tagUpDown)
    {
        //Debug.Log("Change");
        currentSymbol += tagUpDown;
        if (currentSymbol < 0)
        {
            currentSymbol = numOfSymbols - 1;

        } else if (currentSymbol >= numOfSymbols)
        {
            currentSymbol = 0;
        }

        material.SetTexture("_MainTex", symbols[currentSymbol]);


    }

    public int GetCurrentSymbol()
    {
        return currentSymbol;
    }
}
