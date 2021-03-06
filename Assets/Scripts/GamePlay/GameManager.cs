﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private int level = 1;
    public int Level { get { return level; } }

    public GameObject pointLevelPanel;

    private GameObject scanwordPanel;
    private GameObject currentPanel;

    private WordScript[] wordScripts;

    [SerializeField]
    private string mainMenuScene = "MainMenuScene";
    [SerializeField]
    private string levelsScene = "LevelsScene";

    public ButtonLettersScript buttonLettersScript;

    void Start()
    {
        LoadLevel();
        CreateLevel();
    }

    private void CreateLevel()
    {
        if (level > PlayerPrefs.GetInt("MaxCountLevels")) LoadEndScene();

        if (currentPanel)
            Destroy(currentPanel);

        scanwordPanel = Resources.Load("Levels/ScanwordPanel_" + level) as GameObject;
        currentPanel = Instantiate(scanwordPanel, pointLevelPanel.transform.position, Quaternion.identity);
        wordScripts = currentPanel.GetComponentsInChildren<WordScript>();

        buttonLettersScript.RemoveButtons();

        List<string> words = new List<string>();
        foreach (WordScript wordScript in wordScripts)
        {
            words.Add(wordScript.Word);
        }
        buttonLettersScript.InitButtons(words.ToArray());

        LoadWords();
    }

    public void СheckWord(string word)
    {
        foreach (WordScript wordScript in wordScripts)
        {
            if (wordScript.Word == word)
            {
                wordScript.ShowWord();
                break;
            }
        }

        CheckWin();
    }

    void CheckWin()
    {
        foreach (WordScript wordScript in wordScripts)
        {
            if (!wordScript.isDisplayed) return;
        }

        PlayerPrefs.DeleteKey(level + " level");
        level++;

        SaveLevel();
        CreateLevel();
    }

    public void LoadLevelsScene()
    {
        SaveLevel();
        SaveWords();
        SceneManager.LoadScene(levelsScene);
    }

    public void LoadMainMenuScene()
    {
        SaveLevel();
        SaveWords();
        SceneManager.LoadScene(mainMenuScene);
    }

    private void OnApplicationPause(bool pause)
    {
        SaveLevel();
        SaveWords();
    }

    /// <summary>
    /// Сохранение значения текущего уровня
    /// </summary>
    private void SaveLevel()
    {
        // Сохранение текущего уровня
        PlayerPrefs.SetInt("CurrentLevel", level);

        if (PlayerPrefs.GetInt("MaxActiveLevel") < level) PlayerPrefs.SetInt("MaxActiveLevel", level);
    }

    /// <summary>
    /// Сохранение отображенных слов на уровне
    /// </summary>
    private void SaveWords()
    {
        // сохраняем только отображенные слова
        StringBuilder activeWords = new StringBuilder("");
        foreach (WordScript word in wordScripts)
        {
            if (word.isDisplayed)
            {
                activeWords.Append(word.Word + "|");
            }
        }
        PlayerPrefs.SetString(Convert.ToString(level) + " level", activeWords.ToString());
    }

    private void LoadLevel()
    {
        level = PlayerPrefs.GetInt("CurrentLevel", 1);
    }

    /// <summary>
    /// Загрузка отображенных слов уровня
    /// </summary>
    private void LoadWords()
    {
        if (!PlayerPrefs.HasKey(level + " level")) return;

        string activeWords = PlayerPrefs.GetString(Convert.ToString(level) + " level");
        string[] words = activeWords.Split('|');

        foreach (string word in words)
        {
            СheckWord(word);
        }
    }

    private void LoadEndScene()
    {
        SceneManager.LoadScene(3);
    }
}
