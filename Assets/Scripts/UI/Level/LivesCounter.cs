using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LivesCounter : MonoBehaviour
{
    [SerializeField] private Panel _gameOverPanel;

    private int _lives;

    public int Lives => _lives;

    public event UnityAction LivesChanged;

    private void OnEnable()
    {
        Floater.DestroyedByFinishPoint += OnFloaterDestroyedByFinishPoint;
    }

    private void OnDisable()
    {
        Floater.DestroyedByFinishPoint -= OnFloaterDestroyedByFinishPoint;
    }

    public void OnStartLevel()
    {
        AddLives(Level.Instance.StartLives);
    }

    private void AddLives(int lives)
    {
        _lives += lives;
        _lives = Math.Max(_lives, 0);
        LivesChanged?.Invoke();

        if (_lives <= 0)
            _gameOverPanel.OpenPanel(_gameOverPanel.gameObject);
    }

    private void OnFloaterDestroyedByFinishPoint(Floater floater)
    {
        AddLives(-1 * (floater as Fruit).Calorie);
    }
}
