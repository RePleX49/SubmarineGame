using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockDoor : MonoBehaviour
{
    public MeshRenderer symbolImage;

    public void ChangeSymbol(Material newSymbol)
    {
        symbolImage.material = newSymbol;
    }
}
