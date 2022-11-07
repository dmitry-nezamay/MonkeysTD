using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Monkey))]
public class MonkeyStatesRoute : StatesRoute
{
    public override void Init()
    {
        Monkey monkey = GetComponent<Monkey>();

        State idle = new IdleMonkeyState(monkey);
        State eat = new EatMonkeyState(monkey);

        StartState = idle;

        Transition approachedToFruit = new ApproachedToFruitMonkeyTransition(monkey);
        Transition ateFruit = new AteFruitMonkeyTransition(monkey);

        idle.TransitionOptions.Add(approachedToFruit, eat);
        eat.TransitionOptions.Add(ateFruit, idle);
    }
}
