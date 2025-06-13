using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private LevelsData _levelsData;
    [SerializeField] private Transform _contantPlace;
    [SerializeField] private LineLevel _line;

    private List<LineLevel> _lines;

    private void OnEnable()
    {
        CreateLevelLines();
    }

    private void OnDisable()
    {
        DestroyLevelLines();
    }

    private void CreateLevelLines()
    {
        _lines = new();
        for (int i = 0; i < _levelsData.Levels.Count; i++)
        {
            LineLevel lineLevel = Instantiate(_line, _contantPlace);
            lineLevel.Init(_levelsData.Levels[i]);
            lineLevel.OnTouchLineLevel += LoadLevel;
            _lines.Add(lineLevel);
        }
    }

    private void DestroyLevelLines()
    {
        for (int i = 0; i < _lines.Count; i++)
        {
            _lines[i].OnTouchLineLevel -= LoadLevel;
            Destroy(_lines[i].gameObject);
        }
        _lines = null;
    }

    private void LoadLevel(LevelModel levelModel)
    {
        SceneManager.LoadScene(levelModel.IndexScene);
    }
}
