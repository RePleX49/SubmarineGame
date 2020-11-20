using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialRight : ButtonScript
{
    public ClockManager manager;

    public override void UseButton()
    {
        manager.TurnRight();
    }
}
