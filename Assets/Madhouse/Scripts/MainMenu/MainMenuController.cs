using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private LevelsData _levelsData;
    [SerializeField] private Transform _contantPlace;
    [SerializeField] private LineLevel _line;
    [SerializeField] private Button _buttonNext;
    [SerializeField] private Button _buttonPlay;
    [SerializeField] private MainMenuView _menuView;

    private int _indexLevel = 0;

    private void OnEnable()
    {
        _buttonNext.onClick.AddListener(SetNextDistraction);
        _menuView.Set(_levelsData.Levels[0]);
    }

    private void OnDisable()
    {
        _buttonNext.onClick.RemoveListener(SetNextDistraction);
    }

    private void SetNextDistraction()
    {
        _indexLevel++;
        if (_indexLevel >= _levelsData.Levels.Count)
            _indexLevel = 0;

        _menuView.Set(_levelsData.Levels[_indexLevel]);
    }

    private void LoadLevel(LevelModel levelModel)
    {
        SceneManager.LoadScene(levelModel.IndexScene);
    }
}
