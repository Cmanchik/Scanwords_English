using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwipePanelsScript : MonoBehaviour
{
    public GameObject panel;
    public float rangeSwipe = 60;

    private Vector2 startPosTouch;  // повторые касания по экрану
    private Vector2 startPosSwipe;  // первый раз коснулся экрана

    private Vector2 directionTouch; // полное движение пальца по экрану от касания до отпускания
    private Vector2 directionSwipe; // расстояние движения пальца за кадр

    private bool directionChosen;

    private GameObject[] panels;
    private List<Transform> createdPanels;

    public int indexCurrentPanel = 0;

    private float distanceTouches = 0;
    public float timeSwipe = 70;

    Touch touch;

    void Start()
    {
        createdPanels = GetComponentsInChildren<Transform>().ToList();
        distanceTouches = createdPanels[1].position.x - createdPanels[0].position.x;
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
    

    private void MoveToTouch()
    {
        float currentSpeed = distanceTouches;

        if (directionTouch.x > 0) currentSpeed = distanceTouches;
        else currentSpeed = -distanceTouches;

        foreach (Transform panel in createdPanels)
        {
            //Переделать движение
        }
    }
}
