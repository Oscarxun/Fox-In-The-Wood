using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    private static int level,levelcomplete;
    // Start is called before the first frame update
    public static int Level
    {
        get{
            return level;
        }
        set{
            level = value;
        }
    }

    public static int LevelCompleted
    {
        get
        {
            return levelcomplete;
        }
        set
        {
            levelcomplete = value;
        }
    }
}
