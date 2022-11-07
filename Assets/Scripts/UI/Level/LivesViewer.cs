using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesViewer : MonoBehaviour
{
    [SerializeField] private Text _viewer;
    
    private LivesCounter _counter;

    private void OnEnable()
    {
        _counter = Level.Instance.LivesCounter;
        _counter.LivesChanged += OnLivesChanged;
    }

    private void OnDisable()
    {
        _counter.LivesChanged -= OnLivesChanged;
    }

    private void OnLivesChanged()
    {
        _viewer.text = _counter.Lives.ToString();
    }
}
