using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwipePanelsScript : MonoBehaviour
{
    public float rangeSwipe = 60;

    /// <summary>
    /// точка повторого касания по экрану
    /// </summary>
    private Vector2 startPosTouch;
    /// <summary>
    /// точка первого касания экрана
    /// </summary>
    private Vector2 startPosSwipe;

    /// <summary>
    /// направление движения пальца за кадр
    /// </summary>
    private Vector2 directionTouch;
    /// <summary>
    /// напрвление движения пальца по экрану от касания до отпускания
    /// </summary>
    private Vector2 directionSwipe;

    /// <summary>
    /// Выбранно ли напраление?
    /// </summary>
    private bool directionChosen;

    /// <summary>
    /// Массив точек фиксаций панелей
    /// </summary>
    private Transform[] panelFixPoints;

    /// <summary>
    /// Список панелей на сцене
    /// </summary>
    private List<Transform> createdPanels;

    /// <summary>
    /// Индекс центральной (отображаемой) панели
    /// </summary>
    public int indexCurrentPanel = 1;

    /// <summary>
    /// расстояние движения пальца за кадр
    /// </summary>
    private float distanceTouches = 0;

    /// <summary>
    /// Скорость смены панелей
    /// </summary>
    public float speedSwipe = 5;
    /// <summary>
    /// Скорость движения панелей за точкой касания
    /// </summary>
    public float speedTouch = 5;

    /// <summary>
    /// Расстояние между панелями
    /// </summary>
    private float distBtwPanels;

    Touch touch;

    void Start()
    {
        createdPanels = GameObject.FindGameObjectsWithTag("LevelPanel").Select(rec => rec.GetComponent<Transform>()).ToList();

        for (int i = 0; i < createdPanels.Count; i++)
        {
            createdPanels[i].GetComponent<LevelPanelScript>().InitButtons(i);
        }

        createdPanels[0].gameObject.SetActive(false);
        if (LevelsMenuManager.Instance.GetMaxCountPanels() == 1) createdPanels[createdPanels.Count - 1].gameObject.SetActive(false);

        panelFixPoints = LevelsMenuManager.Instance.panelFixPoints;
        distBtwPanels = Mathf.Abs(createdPanels[0].position.x - createdPanels[1].position.x);
    }

    void Update()
    {
        
        // Track a single touch as a direction control.
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            // Handle finger movements based on touch phase.
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    startPosTouch = touch.position;
                    startPosSwipe = touch.position;
                    directionChosen = false;
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    if (Vector2.Distance(touch.position, startPosTouch) > 2)
                    {
                        distanceTouches = Vector2.Distance(touch.position, startPosTouch);
                        directionTouch = touch.position - startPosTouch;
                        MoveToTouch();
                    }
                    startPosTouch = touch.position;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    directionChosen = true;
                    directionSwipe = touch.position - startPosSwipe;

                    if (Mathf.Abs(Vector2.Distance(touch.position, startPosSwipe)) >= rangeSwipe)
                        ControlSwipe();

                    break;
            }
        }

        if (directionChosen)
        {
            for (int i = 0; i < createdPanels.Count; i++)
            {
                createdPanels[i].position = Vector2.Lerp(createdPanels[i].position, panelFixPoints[i].position, speedSwipe);
            }
        }
    }

    private void MoveToTouch()
    {
        float currentDir;

        if (directionTouch.x > 0) currentDir = distanceTouches;
        else currentDir = -distanceTouches;

        foreach (Transform panel in createdPanels)
        {
            panel.position = Vector3.Lerp(panel.position, new Vector2(panel.position.x + currentDir, panel.position.y), speedTouch);
        }
    }

    private void ControlSwipe()
    {
        if (directionSwipe.x > 0)
        {
            if (indexCurrentPanel != 1)
            {
                Transform panel = createdPanels[createdPanels.Count - 1];
                createdPanels.RemoveAt(createdPanels.Count - 1);

                if (!panel.gameObject.activeInHierarchy) panel.gameObject.SetActive(true);

                panel.position = new Vector2(createdPanels[0].position.x - distBtwPanels, 0);
                panel.GetComponent<LevelPanelScript>().InitButtons(indexCurrentPanel - 2);

                createdPanels.Insert(0, panel);

                if (indexCurrentPanel == 2)
                {
                    createdPanels[0].gameObject.SetActive(false);
                }

                indexCurrentPanel--;
            }
        }
        else if (directionSwipe.x < 0)
        {
            if (indexCurrentPanel != LevelsMenuManager.Instance.GetMaxCountPanels())
            {
                Transform panel = createdPanels[0];
                createdPanels.RemoveAt(0);

                if (!panel.gameObject.activeInHierarchy) panel.gameObject.SetActive(true);

                panel.position = new Vector2(createdPanels[createdPanels.Count - 1].position.x + distBtwPanels, 0);
                panel.GetComponent<LevelPanelScript>().InitButtons(indexCurrentPanel + 2);
                createdPanels.Add(panel);

                if (indexCurrentPanel == LevelsMenuManager.Instance.GetMaxCountPanels() - 1)
                {
                    createdPanels[createdPanels.Count - 1].gameObject.SetActive(false);
                }

                indexCurrentPanel++;
            }
        }
    }
}
