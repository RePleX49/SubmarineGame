using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialRight : ButtonScript
{
    public ClockManager manager;
    public AudioSource buttonClickSound;

    public override void UseButton()
    {
        buttonClickSound.Play();
        manager.TurnRight();
    }
}
