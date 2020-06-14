using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelsMenuManager : Singleton<LevelsMenuManager>
{
    /// <summary>
    /// Шаблон панели
    /// </summary>
    public GameObject panel;

    /// <summary>
    /// Массив точек фиксаций панелей
    /// </summary>
    public Transform[] panelFixPoints;

    /// <summary>
    /// Загружаемые уровни
    /// </summary>
    private List<GameObject> levels;

    /// <summary>
    /// Родительский объект для панелей на сцене
    /// </summary>
    public Transform containerForPanels;

    void Awake()
    {
        levels = Resources.LoadAll("Levels", typeof(GameObject)).Cast<GameObject>().ToList();
        CreateLevel();
    }

    void CreateLevel()
    {
        foreach (Transform point in panelFixPoints)
        {
            GameObject panel = Instantiate(this.panel, point.position, Quaternion.identity);
            panel.transform.SetParent(containerForPanels);
        }
    }
}
