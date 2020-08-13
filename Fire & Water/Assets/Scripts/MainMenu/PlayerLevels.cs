using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevels : MonoBehaviour
{
    public enum LevelIndex
    {
        LEVEL_01 = 0,
        LEVEL_02
    }


    private List<LevelIndex> unlockedLevels;

    public PlayerLevels()
    {
        unlockedLevels = new List<LevelIndex>();
    }

    public void UnlockLevel(LevelIndex _levelIndex)
    {
        unlockedLevels.Add(_levelIndex);
    }

    public bool IsLevelUnlocked(LevelIndex _levelIndex)
    {
        return unlockedLevels.Contains(_levelIndex);
    }

    public int GetLevelIndex(LevelIndex _levelIndex)
    {
        return (int)_levelIndex;
    }
}
