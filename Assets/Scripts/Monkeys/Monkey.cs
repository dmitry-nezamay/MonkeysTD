using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Monkey : MonoBehaviour
{
    private static float _rotationSpeed = 3f;

    public static float RotationSpeed => _rotationSpeed;

    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _price;
    [SerializeField] private float _radius;
    [SerializeField] private float _appetite;

    private SpriteRenderer _renderer;
    private Animator _animator;
    private bool _isDragging = false;
    private List<Transform> _enteredTriggers = new List<Transform>();
    private CoverageArea _coverageArea;
    private StateMachine _stateMachine;
    private Floater _nearestFloater;
    private Floater _targetFloater;

    public string Name => _name;
    public Sprite Icon => _icon;
    public int Price => _price;
    public float Radius => _radius;
    public bool IsDragging => _isDragging;
    public bool HasEnteredTriggers => (_enteredTriggers.Count > 0);
    public int SortingOrder => _renderer.sortingOrder;
    public Floater NearestFloater => _nearestFloater;
    public Floater TargetFloater => _targetFloater;

    public static event UnityAction<Monkey> NewMonkeyWasDragged;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _coverageArea = GetComponentInChildren<CoverageArea>();

        if (_coverageArea != null)
        {
            _coverageArea.Init(this);
            _coverageArea.gameObject.SetActive(false);
        }

        StatesRoute statesRoute = GetComponent<StatesRoute>();
        statesRoute.Init();
        _stateMachine = new StateMachine();
        _stateMachine.Init(statesRoute.StartState);
    }

    private void Update()
    {
        if (_isDragging)
        {
            _renderer.color = MonkeyColorController.MonkeyColorOnDrag;
        }
        else
        {
            _stateMachine.CurrentState.Update();

            foreach (var transitionOption in _stateMachine.CurrentState.TransitionOptions)
                transitionOption.Key.Update();

            if (_stateMachine.TryGetNextState(out State nextState) == true)
                _stateMachine.ChangeState(nextState);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDragging && _enteredTriggers.Contains(collision.transform) == false)
            _enteredTriggers.Add(collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isDragging && _enteredTriggers.Contains(collision.transform) == true)
            _enteredTriggers.Remove(collision.transform);
    }

    public void OnÂeginDrag()
    {
        _isDragging = true;

        if (_coverageArea != null)
            _coverageArea.gameObject.SetActive(true);
    }

    public void OnEndDrag()
    {
        if (_isDragging && HasEnteredTriggers)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _isDragging = false;
            _enteredTriggers.Clear();
            _renderer.color = MonkeyColorController.MonkeyColorOnEndDrag;
            NewMonkeyWasDragged?.Invoke(this);
        }
    }

    public void RotateToTarget()
    {
        List<Floater> floaters = new List<Floater>(Level.Instance.FloatersContainer.GetComponentsInChildren<Floater>());

        if (floaters.Count > 0)
        {
            Floater nearestFloater = floaters[0];
            float minDistance = Vector3.Distance(transform.position, nearestFloater.transform.position);

            for (int i = 1; i < floaters.Count; i++)
                if (Vector3.Distance(transform.position, floaters[i].transform.position) < minDistance)
                {
                    nearestFloater = floaters[i];
                    minDistance = Vector3.Distance(transform.position, floaters[i].transform.position);
                }

            _nearestFloater = nearestFloater;

            Quaternion targetQuaternion = Quaternion.Euler(
                transform.rotation.x,
                transform.rotation.y,
                Mathf.Atan2(_nearestFloater.transform.position.y - transform.position.y, _nearestFloater.transform.position.x - transform.position.x) * Mathf.Rad2Deg + 90);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetQuaternion, Monkey.RotationSpeed * Time.deltaTime);
        }
        else
        {
            _nearestFloater = null;
        }
    }

    public void SelectAnimatorState()
    {
        _animator.StopPlayback();

        if (_stateMachine.CurrentState.GetType() == typeof(IdleMonkeyState))
            PlayStateMotion(MonkeyAnimatorController.States.Idle);
        else if (_stateMachine.CurrentState.GetType() == typeof(EatMonkeyState))
            PlayStateMotion(MonkeyAnimatorController.States.Eat);
    }

    public void SetTargetFloater(Floater floater)
    {
        _targetFloater = floater;
    }

    public void StartEatFruitCoroutine()
    {
        if (_targetFloater != null)
            StartCoroutine(EatFruitCoroutine());
    }

    private void PlayStateMotion(string StateName)
    {
        int stateHash = Animator.StringToHash(StateName);

        if (_animator.HasState(0, stateHash))
            _animator.CrossFade(stateHash, 0.1f);
    }

    private IEnumerator EatFruitCoroutine()
    {
        _targetFloater.gameObject.SetActive(false);
        _targetFloater.transform.parent = transform;

        float grabDuration = 0.6f;
        PlayStateMotion(MonkeyAnimatorController.States.Grab);
        yield return new WaitForSeconds(grabDuration);

        transform.rotation = Quaternion.identity;
        PlayStateMotion(MonkeyAnimatorController.States.Eat);
        float eatDuration = ((_targetFloater as Fruit).Calorie) / _appetite;
        yield return new WaitForSeconds(eatDuration);

        Destroy(_targetFloater.gameObject);
        _targetFloater = null;
    }
}
