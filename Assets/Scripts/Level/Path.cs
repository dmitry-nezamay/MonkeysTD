using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnPoint))]
public class Path : MonoBehaviour
{
    [SerializeField] private SpawnPoint _spawnPoint;
    [SerializeField] private List<PathPoint> _pathPoints;
    [SerializeField] private FinishPoint _finishPoint;

    public SpawnPoint SpawnPoint => _spawnPoint;
    public List<PathPoint> PathPoints => _pathPoints;
    public FinishPoint FinishPoint => _finishPoint;
}
