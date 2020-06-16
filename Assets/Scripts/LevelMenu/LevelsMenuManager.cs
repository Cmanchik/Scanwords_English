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
    /// Шаблон кнопки
    /// </summary>
    public GameObject button;

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

    
    [SerializeField]
    private int countLevelsPanels;
    /// <summary>
    /// Кол-во уровней на одной панели
    /// </summary>
    public int CountLevelsPanels { get { return countLevelsPanels; } private set { countLevelsPanels = value; } }

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