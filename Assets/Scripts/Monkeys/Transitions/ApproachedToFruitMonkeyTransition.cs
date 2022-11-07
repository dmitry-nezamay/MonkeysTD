using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachedToFruitMonkeyTransition : Transition
{
    private Monkey _monkey;

    public ApproachedToFruitMonkeyTransition(Monkey monkey)
    {
        _monkey = monkey;
    }

    public override void Update()
    {
        if (_monkey.NearestFloater == null)
            return;

        if (_monkey.NearestFloater != null && Vector3.Distance(_monkey.transform.position, _monkey.NearestFloater.transform.position) <= _monkey.Radius)
        {
            _monkey.SetTargetFloater(_monkey.NearestFloater);
            DoTransit = true;
        }
    }
}
