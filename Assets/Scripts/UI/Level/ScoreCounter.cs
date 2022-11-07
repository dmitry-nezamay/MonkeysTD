using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreCounter : MonoBehaviour
{
    private int _score;

    public int Score => _score;

    public event UnityAction ScoreChanged;

    private void OnEnable()
    {
        Floater.DestroyedByMonkey += OnFloaterDestroyedByMonkey;
        Monkey.NewMonkeyWasDragged += OnNewMonkeyWasDragged;
    }

    private void OnDisable()
    {
        Floater.DestroyedByMonkey -= OnFloaterDestroyedByMonkey;
        Monkey.NewMonkeyWasDragged -= OnNewMonkeyWasDragged;
    }

    public void OnStartLevel()
    {
        AddScore(Level.Instance.StartScore);
    }

    private void AddScore(int score)
    {
        _score += score;
        ScoreChanged?.Invoke();
    }

    private void OnFloaterDestroyedByMonkey(Floater floater)
    {
        AddScore(floater.Reward);
    }

    private void OnNewMonkeyWasDragged(Monkey monkey)
    {
        AddScore(-monkey.Price);
    }
}
