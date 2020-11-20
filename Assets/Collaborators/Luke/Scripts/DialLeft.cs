using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialLeft : ButtonScript
{
    public ClockManager manager;

    public override void UseButton()
    {
        manager.TurnLeft();
    }
}
