using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonScript : MonoBehaviour
{
    private int level;
    private TextMeshPro textMesh;

    public Text text;

    public void SetLevel(int value)
    {
        level = value;
        text.text = Convert.ToString(value);
    }
}
