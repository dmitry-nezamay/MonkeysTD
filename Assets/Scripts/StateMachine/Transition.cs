using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition
{
    public bool DoTransit { get; protected set; } = false;

    public virtual void Update() { }

    public void ResetDoTransit()
    {
        DoTransit = false;
    }
}
