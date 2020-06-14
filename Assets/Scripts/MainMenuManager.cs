using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class MainMenuManager : Singleton<MainMenuManager>
{
    [SerializeField]
    private string gameplayScene = "GameplayScene";
    [SerializeField]
    private string levelsScene = "LevelsScene";

    AsyncOperation asyncOperationGameScene;
    AsyncOperation asyncOperationLevelsScene;

    public GameObject currentLevelPanel;

    void Start()
    {
        asyncOperationGameScene = SceneManager.LoadSceneAsync(gameplayScene, LoadSceneMode.Single);
        asyncOperationLevelsScene = SceneManager.LoadSceneAsync(levelsScene, LoadSceneMode.Single);

        asyncOperationGameScene.allowSceneActivation = false;
        asyncOperationLevelsScene.allowSceneActivation = false;

        currentLevelPanel.GetComponentInChildren<TextMeshPro>().text = GameManager.Instance.Level + "\nLevel";
    }

    public void LoadGameplayScene()
    {
        asyncOperationGameScene.allowSceneActivation = true;
    }

    public void LoadLevelsScene()
    {
        asyncOperationLevelsScene.allowSceneActivation = true;
    }
}
