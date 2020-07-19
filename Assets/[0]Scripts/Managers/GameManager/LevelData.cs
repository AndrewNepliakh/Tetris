using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
}

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData")]
public class LevelData : BaseInjectable
{
    public List<Level> levels;

    public int GetLevelsCount() => levels.Count;
}