using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private SceneAsset _scene;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void Init(SceneAsset scene)
    {
        _scene = scene;

        Text text = GetComponentInChildren<Text>();

        if (text != null)
            text.text = _scene.name;
    }

    private void OnButtonClick()
    {
        SceneManager.LoadScene(_scene.name);
    }
}
