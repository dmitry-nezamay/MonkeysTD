using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CoverageArea : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Monkey _monkey;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_monkey.IsDragging)
            if (_monkey.HasEnteredTriggers)
                _renderer.color = MonkeyColorController.CoverageAreaWithEnteredTriggersColorOnDrag;
            else
                _renderer.color = MonkeyColorController.CoverageAreaColorOnDrag;
    }

    public void Init(Monkey monkey)
    {
        Awake();

        _monkey = monkey;
        transform.localScale = 2 * _monkey.Radius * Vector3.one;
        _renderer.sortingOrder = _monkey.SortingOrder - 1;
    }
}
