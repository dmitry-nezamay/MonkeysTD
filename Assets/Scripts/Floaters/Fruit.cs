using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Floater
{
    [SerializeField] private int _calorie;

    public int Calorie => _calorie;
}
