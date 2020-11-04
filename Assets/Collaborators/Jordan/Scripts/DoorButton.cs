using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public IntroPuzzleController controller;


    public void TryButton()
    {
       //Debug.Log("Call");
        controller.CheckInput();
    }
}
