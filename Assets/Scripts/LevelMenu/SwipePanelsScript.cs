using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwipePanelsScript : MonoBehaviour
{
    public GameObject panel;
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
    private GameObject[] panelFixPoints;

    /// <summary>
    /// Список панелей на сцене
    /// </summary>
    private List<Transform> createdPanels;

    /// <summary>
    /// Индекс центральной (отображаемой) панели
    /// </summary>
    public int indexCurrentPanel = 0;

    /// <summary>
    /// расстояние движения пальца за кадр
    /// </summary>
    private float distanceTouches = 0;

    Touch touch;

    void Start()
    {
        createdPanels = GetComponentsInChildren<Transform>().ToList();
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

                    Debug.Log(directionSwipe);
                    break;
            }
        }
    }

    private void MoveToTouch()
    {
        float currentSpeed;

        if (directionTouch.x > 0) currentSpeed = distanceTouches;
        else currentSpeed = -distanceTouches;

        foreach (Transform panel in createdPanels)
        {
            //Переделать движение
        }
    }
}
