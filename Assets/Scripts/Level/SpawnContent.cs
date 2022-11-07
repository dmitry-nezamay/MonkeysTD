using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FloaterToSpawn
{
    public Floater Floater;
    public AnimationCurve SpawnProbability;
    public int NumberToSpawn;
}

[CreateAssetMenu(menuName = "Custom/Create WaveContent")]
public class SpawnContent : ScriptableObject
{
    [SerializeField] private float _duration;
    [SerializeField] private List<FloaterToSpawn> _floaters;

    public float Duration => _duration;
    public List<FloaterToSpawn> Floaters => _floaters;
}
