using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatesRoute : MonoBehaviour
{
    public State StartState {get; protected set;}

    public abstract void Init();
}
