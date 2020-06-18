using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonScript : MonoBehaviour
{
    private int level;
    private TextMeshPro textMesh;

    public Button button;
    public Text text;

    private void Start()
    {
        button.onClick.AddListener(Load);
    }

    public void SetLevel(int value)
    {
        level = value;
        text.text = Convert.ToString(value);

        if (level > PlayerPrefs.GetInt("MaxLevel", 1)) button.interactable = false;
    }

    public void Load()
    {
        PlayerPrefs.SetInt("CurrentLevel", level);
        LevelsMenuManager.Instance.LoadGameplayScene();
    }
}
