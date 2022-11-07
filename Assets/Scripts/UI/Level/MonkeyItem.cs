using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MonkeyItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image _frame;
    [SerializeField] private Image _icon;
    [SerializeField] private Text _price;
    [SerializeField] private Button _selectButton;
    [SerializeField] private Material _grayscale;

    private ScoreCounter _scoreCounter;
    private Monkey _monkeyTemplate;
    private Monkey _newMonkey;
    private Material _newMaterial;

    private void Awake()
    {
        _newMaterial = Instantiate(_grayscale);
        _frame.material = _newMaterial;
        _icon.material = _newMaterial;
    }

    private void OnEnable()
    {
        _scoreCounter = Level.Instance.ScoreCounter;
        _scoreCounter.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _scoreCounter.ScoreChanged -= OnScoreChanged;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _newMonkey = null;

        if (_monkeyTemplate.Price <= _scoreCounter.Score)
        {
            _newMonkey = Instantiate(_monkeyTemplate, Level.Instance.MonkeysContainer);
            _newMonkey.OnÂeginDrag();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_newMonkey == null)
            return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(eventData.position);
        mousePosition.z = 0;
        _newMonkey.transform.position = mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_newMonkey == null)
            return;

        bool destroyNewMonkeyObject = false;

        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        foreach (var raycastResult in raycastResults)
            if (raycastResult.gameObject.TryGetComponent(out Shop shop))
                destroyNewMonkeyObject = true;

        if (destroyNewMonkeyObject)
            Destroy(_newMonkey.gameObject);
        else
            _newMonkey.OnEndDrag();
    }

    public void Init(Monkey monkey)
    {
        _monkeyTemplate = monkey;
    }

    public void Render()
    {
        _icon.sprite = _monkeyTemplate.Icon;
        _price.text = _monkeyTemplate.Price.ToString();
    }

    private void OnScoreChanged()
    {
        if (_newMaterial != null)
        {
            float grayscaleAmount = _monkeyTemplate.Price <= _scoreCounter.Score ? 0 : 1;
            _newMaterial.SetFloat("_GrayscaleAmount", grayscaleAmount);
        }
    }
}
