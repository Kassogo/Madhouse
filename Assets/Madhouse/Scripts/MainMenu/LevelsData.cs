using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels",menuName = "Madhouse/LevelsData")]
public class LevelsData : ScriptableObject
{
    public List<LevelModel> Levels => _levelsData;

    [SerializeField] private List<LevelModel> _levelsData;
}