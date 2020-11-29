using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoormanager : ButtonScript
{

    [SerializeField] private int puzzlesComplete = 0;

    private char prevPuzzleComplete = 'a';

    public GameObject doorHolder;

    public GameObject doorTarget;

    public float doorMoveDuration;

    public float doorDropDistance;

    public AudioSource incorrectBeep;

    public AudioSource correctBeep;

    public AudioSource doorOpenSound;

    public void Update()
    {
        //TODO TAKE THIS OUT!!!!!!!!!!!!!!!!!!!
        //if(Input.GetKeyDown(KeyCode.H))
        //{
        //    puzzlesComplete = 2;
        //    UseButton();
        //}
    }

    public void CompletePuzzle (int puzzleTag)
    {
        if (puzzleTag == 1)
        {
            if (prevPuzzleComplete != 'p')
            {
                puzzlesComplete++;
                prevPuzzleComplete = 'p';
            }
        } else if (puzzleTag == 2)
        {
            if (prevPuzzleComplete != 's')
            {
                puzzlesComplete++;
                prevPuzzleComplete = 's';
            }
        }
    }

    public void OpenDoor()
    {
        puzzlesComplete = 2;
        UseButton();
    }

    public override void UseButton()
    {
        if(puzzlesComplete >= 2)
        {
            Vector3 targetEnd = doorHolder.transform.localPosition + new Vector3(0f, -doorDropDistance, 0f);

            StartCoroutine(Systems.transforms.DoorLerp(doorHolder.transform, targetEnd, doorMoveDuration));
            if (!doorOpenSound.isPlaying)
            {
                doorOpenSound.Play();
            }
            correctBeep.Play();
        }
        else
        {
            if(!incorrectBeep.isPlaying)
            {
                incorrectBeep.Play();
            }
        }
    }
}
