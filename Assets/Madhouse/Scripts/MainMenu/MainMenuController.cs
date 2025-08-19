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
        _buttonPlay.onClick.AddListener(LoadLevel);
        _menuView.Set(_levelsData.Levels[0]);
    }

    private void OnDisable()
    {
        _buttonNext.onClick.RemoveListener(SetNextDistraction);
        _buttonPlay.onClick.RemoveListener(LoadLevel);
    }

    private void SetNextDistraction()
    {
        _indexLevel++;
        if (_indexLevel >= _levelsData.Levels.Count)
            _indexLevel = 0;

        _menuView.Set(_levelsData.Levels[_indexLevel]);
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(_levelsData.Levels[_indexLevel].IndexScene);
    }
}
