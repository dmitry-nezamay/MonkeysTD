using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class IdleMonkeyState : State
{
    private Monkey _monkey;

    public IdleMonkeyState(Monkey monkey)
    {
        _monkey = monkey;
    }

    public override void Enter()
    {
        _monkey.SelectAnimatorState();
    }

    public override void Update()
    {
        _monkey.RotateToTarget();
    }
}
