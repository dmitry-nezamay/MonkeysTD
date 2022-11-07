using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelPanel : MonoBehaviour
{
    [SerializeField] private LevelButton _buttonTemplate;
    [SerializeField] private SceneAsset[] _levels;

    private void Awake()
    {
        foreach (var scene in _levels)
        {
            LevelButton button = Instantiate(_buttonTemplate, transform);
            button.Init(scene);
        }
    }
}
