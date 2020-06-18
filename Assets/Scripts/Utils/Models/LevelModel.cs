using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelModel : ScriptableObject
{
    public int level;

    public LevelModel(int level)
    {
        this.level = level;
    }
}
