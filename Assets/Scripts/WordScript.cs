using UnityEngine;
using UnityEditor;
using System;
using TMPro;

public class WordScript : MonoBehaviour
{
    [SerializeField]
    private string word;
    public string Word { get { return word; } private set { word = ""; } }

    public bool isDisplayed = false;

    private TextMeshPro[] textCompLetters;

    void Start()
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
}
