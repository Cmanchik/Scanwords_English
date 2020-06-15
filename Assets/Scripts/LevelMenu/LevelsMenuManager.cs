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

    /// <summary>
    /// Кол-во уровней на одной панели
    /// </summary>
    [Range(1, 6)]
    public int countLevelsPanels = 2;

    void Awake()
    {
        levels = Resources.LoadAll("Levels", typeof(GameObject)).Cast<GameObject>().ToList();
        CreateLevel();
    }

    void CreateLevel()
    {
        for (int i = 0; i < panelFixPoints.Length; i++)
        {
            GameObject panel = Instantiate(this.panel, panelFixPoints[i].position, Quaternion.identity);
            panel.transform.SetParent(containerForPanels);
        }
    }

    public int GetMaxCountPanels()
    {
        return (int)Math.Ceiling(levels.Count() / (double)countLevelsPanels);
    }

    public int GetMaxCountLevels()
    {
        return levels.Count;
    }
}