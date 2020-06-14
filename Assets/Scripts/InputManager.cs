using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    List<GameObject> selectedLetters = new List<GameObject>();
    LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    public void StartSelectLetters(GameObject button)
    {
        selectedLetters.Add(button);
        lineRenderer.enabled = true;
    }

    // Вызывается когда нажимаем на кнопку и проводим по кнопке
    public void SelectLetter(GameObject button)
    {
        if (lineRenderer.enabled && !selectedLetters.Contains(button)) selectedLetters.Add(button);
    }

    // Вызывается когда отпускается палец от экрана
    public void InputLetters()
    {
        StringBuilder word = new StringBuilder("");
        foreach (GameObject button in selectedLetters)
        {
            word.Append(button.GetComponentInChildren<TextMeshPro>().text);
        }
        GameManager.Instance.СheckWord(word.ToString());

        lineRenderer.enabled = false;
        selectedLetters.Clear();
    }

    void Update()
    {
        lineRenderer.positionCount = selectedLetters.Count + 1;

        for (int i = 0; i < selectedLetters.Count; i++)
        {
            lineRenderer.SetPosition(i, selectedLetters[i].transform.position);
        }

        if (Input.touchCount > 0)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            touchPos.z = -1;

            lineRenderer.SetPosition(selectedLetters.Count, touchPos);
        }
    }
}
