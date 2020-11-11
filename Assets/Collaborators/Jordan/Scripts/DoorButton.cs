using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : ButtonScript
{
    //When the camera script raycasts to this it will call try button which will call the appropriate behavior in the puzzle controller
    public IntroPuzzleController controller;

    public override void UseButton()
    {
        controller.CheckInput();
    }
}
