using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private Text _viewer;
    
    private ScoreCounter _counter;

    private void OnEnable()
    {
        _counter = Level.Instance.ScoreCounter;
        _counter.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _counter.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged()
    {
        _viewer.text = _counter.Score.ToString();
    }
}
