using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockDoor : MonoBehaviour
{
    public MeshRenderer symbolImage;

    public float duration = 1.65f;

    public void ChangeSymbol(Material newSymbol)
    {
        symbolImage.material = newSymbol;
        //StartCoroutine(SmoothTransition(newSymbol));
    }

    IEnumerator SmoothTransition(Material newMaterial)
    {
        Material initialMat = symbolImage.material;
        float elapsedTime = 0.0f;

        while(elapsedTime < duration)
        {
            symbolImage.material.Lerp(initialMat, newMaterial, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
