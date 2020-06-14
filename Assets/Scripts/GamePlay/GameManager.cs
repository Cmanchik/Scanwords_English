using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public RectTransform mainMenu;
    public RectTransform gameplayMenu;
    public RectTransform gameoverMenu;

    [SerializeField]
    private string mainMenuScene = "MainMenuScene";
    [SerializeField]
    private string levelsScene = "LevelsScene";

    public ButtonLettersScript buttonLettersScript;

    void Start()
    {
        CreateLevel();
    }

    private void CreateLevel()
    {
        if (mainMenu) mainMenu.gameObject.SetActive(false);
        if (gameoverMenu) gameoverMenu.gameObject.SetActive(false);
        if (gameplayMenu) gameplayMenu.gameObject.SetActive(true);

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

        level++;
        CreateLevel();
    }

    public void LoadLevelsScene()
    {
        SceneManager.LoadScene(levelsScene);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
