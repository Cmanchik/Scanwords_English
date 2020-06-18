using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Linq;

public class MainMenuManager : Singleton<MainMenuManager>
{
    public Text currentLevelPanel;

    void Start()
    {
        PlayerPrefs.SetInt("MaxCountLevels", Resources.LoadAll("Levels", typeof(GameObject)).Cast<GameObject>().Count());
        currentLevelPanel.text = Convert.ToString(PlayerPrefs.GetInt("CurrentLevel", 1)) + " LEVEL";
    }

    public void LoadGameplayScene()
    {
        SceneManager.LoadScene(1);
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
