using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class MainMenuManager : Singleton<MainMenuManager>
{
    [SerializeField]
    private string gameplayScene = "GameplayScene";
    [SerializeField]
    private string levelsScene = "LevelsScene";

    AsyncOperation asyncOperationGameScene;
    AsyncOperation asyncOperationLevelsScene;

    public Text currentLevelPanel;

    void Start()
    {
        asyncOperationGameScene = SceneManager.LoadSceneAsync(gameplayScene, LoadSceneMode.Single);
        asyncOperationLevelsScene = SceneManager.LoadSceneAsync(levelsScene, LoadSceneMode.Single);

        asyncOperationGameScene.allowSceneActivation = false;
        asyncOperationLevelsScene.allowSceneActivation = false;

        currentLevelPanel.text = Convert.ToString(PlayerPrefs.GetInt("CurrentLevel", 1)) + " LEVEL";
    }

    public void LoadGameplayScene()
    {
        asyncOperationGameScene.allowSceneActivation = true;
    }

    public void LoadLevelsScene()
    {
        SceneManager.LoadScene(2);
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
}
