using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    public void LoadMainMenu()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
