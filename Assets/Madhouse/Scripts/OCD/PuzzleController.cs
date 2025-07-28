using UnityEngine;
using System.Collections.Generic;

public class PuzzleController : MonoBehaviour
{
    [Header("Таймер")]
    [SerializeField] private TimerManager _timerManager;

    [Header("Основные настройки")]
    [SerializeField] private int _baseGridSize = 3;

    [Header("Пороги сложности")]
    [SerializeField] private int _scoreThresholdGrid4 = 15;
    [SerializeField] private int _scoreThresholdGrid5 = 35;

    [Header("Ссылки на компоненты")]
    [SerializeField] private ShapeGridManager _gridManager;
    [SerializeField] private UniqueShapeModifier _shapeModifier;
    [SerializeField] private ShapeInteractionHandler _interactionHandler;

    [Header("Элементы интерфейса")]
    [SerializeField] private GameObject _winPanel;  // Окошко победы

    private int _currentGridSize;

    void Start()
    {
        if (_gridManager == null || _shapeModifier == null || _interactionHandler == null)
        {
            Debug.LogError("Не все компоненты назначены в PuzzleController!");
            return;
        }

        if (TimerManager.Instance == null)
        {
            Debug.LogError("TimerManager не найден на сцене!");
            return;
        }
        _timerManager = TimerManager.Instance;

        _currentGridSize = _baseGridSize;

        GenerateNewPuzzle();

        // Подписываемся на событие достижения 50 очков
        ScoreManager.Instance.OnScoreReached += ShowWinPanel;
    }

    void OnDestroy()
    {
        // Отписываемся от события при уничтожении объекта
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreReached -= ShowWinPanel;
        }
    }

    public void GenerateNewPuzzle()
    {
        int score = ScoreManager.Instance != null ? ScoreManager.Instance.CurrentScore : 0;

        if (score >= _scoreThresholdGrid5)
        {
            _currentGridSize = 5;
        }
        else if (score >= _scoreThresholdGrid4)
        {
            _currentGridSize = 4;
        }
        else
        {
            _currentGridSize = _baseGridSize;
        }

        _gridManager.GenerateGrid(_currentGridSize, this);
        IReadOnlyList<GameObject> currentShapes = _gridManager.CurrentShapes;
        int correctShapeIndex = _shapeModifier.ApplyUniqueModification(currentShapes);
        if (correctShapeIndex != -1)
        {
            _interactionHandler.Setup(correctShapeIndex);
        }
        else
        {
            Debug.LogError("Не удалось создать уникальное насекомое!");
        }
    }

    public void OnShapeClicked(int shapeIndex)
    {
        _interactionHandler.HandleShapeClick(shapeIndex);
    }

    public void OnCorrectShapeSelected()
    {
        Debug.Log("PuzzleController: Выбрано правильное насекомое! Продолжаем...");
        _timerManager.AddTime();
        Invoke(nameof(GenerateNewPuzzle), 1.0f);
    }

    public void OnWrongShapeSelected()
    {
        Debug.Log("PuzzleController: Выбрано неверное насекомое!");
        _timerManager.RemoveTime();
        Invoke(nameof(GenerateNewPuzzle), 1.0f);
    }

    public void SetDifficulty(int newGridSize)
    {
        _currentGridSize = Mathf.Max(2, newGridSize);
        GenerateNewPuzzle();
    }

    // Метод для активации окошка победы
    private void ShowWinPanel()
    {
        if (_winPanel != null)
        {
            _winPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogWarning("WinPanel не назначен в PuzzleController!");
        }
    }
}