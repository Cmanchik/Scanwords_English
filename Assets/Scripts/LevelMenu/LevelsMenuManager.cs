using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelsMenuManager : Singleton<LevelsMenuManager>
{
    public GameObject panel;

    private List<GameObject> levels;
    void Start()
    {
        levels = Resources.LoadAll("Levels", typeof(GameObject)).Cast<GameObject>().ToList(); 
    }

    void CreateLevel()
    {

    }
}
