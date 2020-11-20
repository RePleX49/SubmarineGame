using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    public float rotationDuration = 1.0f;

    public ClockDoor[] symbolHolders;

    public AudioClip correctSound;
    public AudioClip wrongSound;

    public AudioSource audioSource;

    // Statue to point at ex: statue 4, statue 7, statue 2, statue 0
    public int correctStatueIndex;

    public MeshRenderer clueDisplay;

    // clue to give to other player
    public CreatureSymbols clueSymbol;

    // clue from other player
    public CreatureSymbols answerSymbol;

    // rotator symbol to give to other player
    public CreatureSymbols revealSymbol;

    int currentRot = 0;
    Vector3 dialRotation = Vector3.zero;

    public TabletData images;
    public TabletData revealImages;
    public Material blankSymbol;

    bool bAnswered = false;
    bool bIsTurning = false;

    public int correctInputSize;
    [SerializeField] private int correctInputMin;
    [SerializeField] private int correctInputMax;

    private int[] correctInputHolder;

    public bool isPlayerA;
    // Start is called before the first frame update
    void Start()
    {

        correctInputHolder = new int[correctInputSize];
        correctInputHolder = Systems.randomSeeding.SetUpArrayBySeed(correctInputHolder, correctInputMin, correctInputMax);

        if (isPlayerA)
        {
            answerSymbol = (CreatureSymbols)correctInputHolder[0];
            clueSymbol = (CreatureSymbols)correctInputHolder[1];
            revealSymbol = (CreatureSymbols)correctInputHolder[2];
            correctStatueIndex = correctInputHolder[3];
        }
        else
        {
            answerSymbol = (CreatureSymbols)correctInputHolder[1];
            clueSymbol = (CreatureSymbols)correctInputHolder[0];
            revealSymbol = (CreatureSymbols)correctInputHolder[3];
            correctStatueIndex = correctInputHolder[2];
        }

        dialRotation.y = currentRot * 45;

        UpdateSymbols();
        clueDisplay.material = images.symbolMats[(int)clueSymbol];
    }

    public void TurnRight()
    {
        if(bAnswered || bIsTurning)
        {
            return;
        }

        //changes the current rot which can be used to figure out if the button is set to the correct orientation
        currentRot++;

        // modulo to loop through values
        currentRot = currentRot % 8;

        //actually set the rotation in increments of 45 degrees
        dialRotation.y = currentRot * 45;
        StartCoroutine(RotateDial(dialRotation));
    }

    public void TurnLeft()
    {
        if (bAnswered || bIsTurning)
        {
            return;
        }

        //changes the current rot which can be used to figure out if the button is set to the correct orientation
        currentRot--;

        // modulo to loop through values
        currentRot = currentRot % 8;
        currentRot = currentRot < 0 ? currentRot + 8 : currentRot;

        //actually set the rotation in increments of 45 degrees
        dialRotation.y = currentRot * 45;
        StartCoroutine(RotateDial(dialRotation));
    }

    public void TryCurrentRot()
    {
        if(bIsTurning || bAnswered)
        {
            return;
        }

        if (correctStatueIndex == currentRot)
        {
            bAnswered = true;
            Debug.Log("Change symbol");
            symbolHolders[currentRot].ChangeSymbol(revealImages.symbolMats[(int)revealSymbol]);
            ClearSymbols();
            audioSource.clip = correctSound;
            audioSource.Play();
            //answerIndex++;
        }
    }

    void ClearSymbols()
    {
        for (int i = 0; i < symbolHolders.Length; i++)
        {
            if (i == correctStatueIndex)
                continue;

            symbolHolders[i].ChangeSymbol(blankSymbol);
        }
    }

    IEnumerator RotateDial(Vector3 targetEuler)
    {
        Quaternion targetRot = Quaternion.Euler(targetEuler);
        Quaternion initialRot = transform.rotation;

        float elapsedTime = 0.0f;

        bIsTurning = true;

        while(elapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Lerp(initialRot, targetRot, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        bIsTurning = false;
    }

    void UpdateSymbols()
    {
        //List<int> usedSymbols = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };
        List<Material> usedMaterials = new List<Material>(images.symbolMats);

        Material answerMat = images.symbolMats[(int)answerSymbol];

        // assign the right symbol material based on answer index
        symbolHolders[correctStatueIndex].ChangeSymbol(answerMat);
        usedMaterials.Remove(answerMat);

        clueDisplay.material = images.symbolMats[(int)clueSymbol];

        // switch the remaining doors to random incorrect symbols
        for (int i = 0; i < symbolHolders.Length; i++)
        {
            if (i == correctStatueIndex)
                continue;

            int random = Random.Range(0, usedMaterials.Count);

            symbolHolders[i].ChangeSymbol(usedMaterials[random]);

            usedMaterials.RemoveAt(random);
        }
    }
}
