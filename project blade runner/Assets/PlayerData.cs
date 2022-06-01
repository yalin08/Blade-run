using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int CurrentLevel;

    public PlayerData(levelManager manager)
    {
        CurrentLevel = manager.currentLevel;
    }
}
