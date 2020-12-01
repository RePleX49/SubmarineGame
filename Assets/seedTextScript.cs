using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class seedTextScript : MonoBehaviour
{

    private RandomSeeding randomManager;
    public TMP_Text seedText;

    // Start is called before the first frame update
    void Start()
    {
        randomManager = GameObject.FindWithTag("Random").GetComponent<RandomSeeding>();
        if (randomManager)
        {
            seedText.text = "SEED: " + randomManager.seed;
        }

    }
}
