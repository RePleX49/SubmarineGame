using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushDial : ButtonScript
{
    public ClockManager manager;

    public override void UseButton()
    {
        if(manager)
        {
            manager.TryCurrentRot();
        }
        else
        {
            Debug.Log("couldn't find manager on " + this);
        }
    }
}
