using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ButtonLettersScript : MonoBehaviour
{
    public float radius = 0;
    public GameObject button;

    private float startAngle = 0; 
    private int step = 0;

    public void InitButtons(string[] words)
    {
        // Буквы для кнопок
        string[] letters = ParseWords(words);

        // Инициализация кнопок
        GameObject[] buttons = new GameObject[letters.Length];
        for (int i = 0; i < letters.Length; i++)
        {
            buttons[i] = button;
        }

        if (buttons.Length == 0) return;

        step = 360 / buttons.Length;
        startAngle = 90 - step;

        float currentAngle = startAngle;

        // Создание на сцене, назначение букв и определение позиции
        for (int i = 0; i < buttons.Length; i++)
        {
            var createdButton = Instantiate(button, new Vector3(0, 0), Quaternion.identity);

            createdButton.GetComponentInChildren<TextMeshPro>().text = letters[i];

            createdButton.transform.SetParent(transform);
            createdButton.transform.localPosition = new Vector3
                (Mathf.Cos(currentAngle * Mathf.PI / 180) * radius, Mathf.Sin(currentAngle * Mathf.PI / 180) * radius, -1);

            currentAngle += step;
        }
    }

    string[] ParseWords(string[] words) 
    {
        List<string> finalLetters = new List<string>();

        foreach (string word in words)
        {
            char[] lettersCurrentWord = word.ToCharArray();
            foreach (char letter in lettersCurrentWord)
            {
                if (!finalLetters.Contains(Convert.ToString(letter))) 
                    finalLetters.Add(Convert.ToString(letter));
            }
        }

        return finalLetters.ToArray();
    }

    public void RemoveButtons()
    {
        Transform[] buttons = GetComponentsInChildren<Transform>();

        for(int i = 1; i < buttons.Length; i++)
        {
            Destroy(buttons[i].gameObject);
        }
    }
}
