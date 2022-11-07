using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    [SerializeField] private MonkeyItem _itemTemplate;
    [SerializeField] private GameObject _itemContainer;

    private void Awake()
    {
        foreach (Monkey monkey in Level.Instance.Monkeys)
        {
            MonkeyItem newItem = Instantiate(_itemTemplate, _itemContainer.transform);
            newItem.Init(monkey);
            newItem.Render();
        }
    }
}
