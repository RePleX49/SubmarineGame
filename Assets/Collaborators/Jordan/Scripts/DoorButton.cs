using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    //When the camera script raycasts to this it will call try button which will call the appropriate behavior in the puzzle controller
    public IntroPuzzleController controller;


    public void TryButton()
    {
        controller.CheckInput();
    }
}
