using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoormanager : MonoBehaviour
{

    private int puzzlesComplete = 0;

    private char prevPuzzleComplete = 'a';

    public GameObject doorHolder;

    public GameObject doorTarget;

    public float doorMoveDuration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzlesComplete >= 2)
        {
            StartCoroutine(Systems.transforms.LerpMove(doorHolder.transform, doorTarget.transform.position,
            doorHolder.transform.rotation, doorHolder.transform.localScale, doorMoveDuration));
        }
    }


    public void CompletePuzzle (int puzzleTag)
    {
        if (puzzleTag == 1)
        {
            prevPuzzleComplete = 'p';
        } else if (puzzleTag == 2)
        {
            prevPuzzleComplete = 's';
        }

        puzzlesComplete++;
    }
}
