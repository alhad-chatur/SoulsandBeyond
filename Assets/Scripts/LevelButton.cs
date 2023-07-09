using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public string levelNo = "level 0";
    public string sceneToLoad;
    public TextMeshProUGUI levelNameTMPro;
    private Button _button;

    private void Start()
    {
        levelNameTMPro = GetComponentInChildren<TextMeshProUGUI>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(LoadLevel);
    }

    private void Update()
    {
        levelNameTMPro.text = levelNo; 
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
