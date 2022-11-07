using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesViewer : MonoBehaviour
{
    [SerializeField] private Text _viewer;

    private Spawner _spawner;

    private void OnEnable()
    {
        _spawner = Level.Instance.Spawner;
        _spawner.NewWaveStarted += OnNewWaveStarted;
    }

    private void OnDisable()
    {
        _spawner.NewWaveStarted -= OnNewWaveStarted;
    }

    private void OnNewWaveStarted(int currentWaveIndex)
    {
        _viewer.text = $"Волна: {currentWaveIndex}/{_spawner.WavesNumber}";
    }
}
