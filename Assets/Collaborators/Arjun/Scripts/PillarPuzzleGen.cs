using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarPuzzleGen : MonoBehaviour
{
    public bool isPlayerA;
    public int pillar1;
    public int pillar2;
    public int pillar3;
    public GameObject[] pillars1;
    public GameObject[] pillars2;
    public GameObject[] pillars3;
    public IntroPuzzleController puzzleController;

    // Start is called before the first frame update
    void Start()
    {
        if (isPlayerA)
        {
            pillar1 = puzzleController.correctInput[1];
            pillar2 = puzzleController.correctInput[3];
            pillar3 = puzzleController.correctInput[5]; 
        }
        else
        {
            pillar1 = puzzleController.correctInput[0];
            pillar2 = puzzleController.correctInput[2];
            pillar3 = puzzleController.correctInput[4];
        }

        foreach (GameObject pillar in pillars1) { pillar.SetActive(false); }
        foreach (GameObject pillar in pillars2) { pillar.SetActive(false); }
        foreach (GameObject pillar in pillars3) { pillar.SetActive(false); }

        pillars1[pillar1 - 1].SetActive(true);
        pillars2[pillar2 - 1].SetActive(true);
        pillars3[pillar3 - 1].SetActive(true);
    }
}
