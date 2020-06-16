using UnityEngine;
using System;
using TMPro;
using System.Linq;

public class WordScript : MonoBehaviour
{
    [SerializeField]
    private string word;
    public string Word { get { return word; } private set { word = ""; } }

    public bool isDisplayed = false;

    private TextMeshPro[] textCompLetters;

    void Awake()
    {
        textCompLetters = GetComponentsInChildren<TextMeshPro>();
        char[] lettersWord = word.ToCharArray();

        for (int i = 0; i < lettersWord.Length; i++)
        {
            textCompLetters[i].text = Convert.ToString(lettersWord[i]);
            textCompLetters[i].enabled = false;
        }
    }

    public void ShowWord()
    {
        isDisplayed = true;

        for (int i = 0; i < textCompLetters.Length; i++)
        {
            textCompLetters[i].enabled = true;
        }
    }

    /// <summary>
    /// Отображение случайной буквы
    /// </summary>
    /// <returns>Отобразилось ли слово полностью?</returns>
    public bool ShowRandomLetter()
    {
        if (isDisplayed) return true;

        TextMeshPro[] lettersForShowing = textCompLetters.Where(rec => rec.enabled == false).ToArray();
        if (lettersForShowing.Length == 1)
        {
            lettersForShowing[0].enabled = true;
            isDisplayed = true;
            return true;
        }

        System.Random random = new System.Random();
        lettersForShowing[random.Next(0, lettersForShowing.Length)].enabled = true;
        
        return false;
    }
}
