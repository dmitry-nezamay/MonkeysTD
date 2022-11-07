using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner))]
[RequireComponent(typeof(LivesCounter))]
[RequireComponent(typeof(ScoreCounter))]
public class Level : MonoBehaviour
{
    private static Level _instance;

    public static Level Instance => _instance;

    [SerializeField] private Transform _floatersContainer;
    [SerializeField] private Transform _monkeysContainer;
    [SerializeField] private int _startLives;
    [SerializeField] private int _startScore;
    [SerializeField] private List<Monkey> _monkeys;
    
    private Spawner _spawner;
    private LivesCounter _livesCounter;
    private ScoreCounter _scoreCounter;

    public Transform FloatersContainer => _floatersContainer;
    public Transform MonkeysContainer => _monkeysContainer;
    public int StartLives => _startLives;
    public int StartScore => _startScore;
    public List<Monkey> Monkeys => _monkeys;
    public Spawner Spawner => _spawner;
    public LivesCounter LivesCounter => _livesCounter;
    public ScoreCounter ScoreCounter => _scoreCounter;

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
        _livesCounter = GetComponent<LivesCounter>();
        _scoreCounter = GetComponent<ScoreCounter>();
    }

    private void Start()
    {
        StartLevel();
    }

    private void OnEnable()
    {
        if (_instance == null)
            _instance = this;
        else
            Debug.LogError($"{gameObject.name} OnEnable error!");
    }

    private void OnDisable()
    {
        if (_instance == this)
            _instance = null;
    }

    public void StartLevel()
    {
        Time.timeScale = 1;

        Floater[] floaters = Level.Instance.FloatersContainer.GetComponentsInChildren<Floater>();

        foreach (Floater floater in floaters)
            Destroy(floater.gameObject);

        Monkey[] monkeys = Level.Instance.MonkeysContainer.GetComponentsInChildren<Monkey>();

        foreach (Monkey monkey in monkeys)
            Destroy(monkey.gameObject);

        _livesCounter.OnStartLevel();
        _scoreCounter.OnStartLevel();

        _spawner.StartSpawn();
    }
}
