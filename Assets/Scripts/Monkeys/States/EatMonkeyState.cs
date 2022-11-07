using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatMonkeyState : State
{
    private Monkey _monkey;

    public EatMonkeyState(Monkey monkey)
    {
        _monkey = monkey;
    }

    public override void Enter()
    {
        _monkey.StartEatFruitCoroutine();
    }
}
