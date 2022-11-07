using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AteFruitMonkeyTransition : Transition
{
    private Monkey _monkey;

    public AteFruitMonkeyTransition(Monkey monkey)
    {
        _monkey = monkey;
    }

    public override void Update()
    {
        if (_monkey.TargetFloater == null)
            DoTransit = true;
    }
}
