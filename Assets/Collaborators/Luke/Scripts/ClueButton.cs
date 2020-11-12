using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueButton : ButtonScript
{
    public ClockManager manager;

    public override void UseButton()
    {
        manager.UpdateClue();
    }
}
