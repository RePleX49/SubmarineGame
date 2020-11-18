using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : ButtonScript
{
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

    bool bAnswered = false;

    // Start is called before the first frame update
    void Start()
    {
        dialRotation.y = currentRot * 45;

        UpdateSymbols();
        clueDisplay.material = images.symbolMats[(int)clueSymbol];
    }

    public override void UseButton()
    {
        //changes the current rot which can be used to figure out if the button is set to the correct orientation
        currentRot++;

        // modulo to loop through values
        currentRot = currentRot % 8;

        //actually set the rotation in increments of 45 degrees
        dialRotation.y = currentRot * 45;
        transform.eulerAngles = dialRotation;
    }

    public void TryCurrentRot()
    {
        if (!bAnswered && correctStatueIndex == currentRot)
        {
            bAnswered = true;
            Debug.Log("Change symbol");
            symbolHolders[currentRot].ChangeSymbol(revealImages.symbolMats[(int)revealSymbol]);
            audioSource.clip = correctSound;
            audioSource.Play();
            //answerIndex++;
        }
    }

    // button press to update clue and statue symbols
    //public void UpdateClue()
    //{
    //    if(!bUpdated && rotatorSymbols[answerIndex].GetCurrentRot() == answerOrder[answerIndex])
    //    {
    //        if(answerIndex == answerOrder.Length - 1)
    //        {
    //            Debug.Log("Congratulations Puzzle Solved");
    //            return;
    //        }

    //        audioSource.clip = correctSound;
    //        audioSource.Play();

    //        answerIndex++;
    //        UpdateSymbols();
    //    } 
    //}

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
