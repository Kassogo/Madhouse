using System.Collections.Generic;
using UnityEngine;

public class ShapeGridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private GameObject _shapePrefab; // Префаб фигуры 
    [SerializeField] private float _spacing = 1.2f;   // Расстояние 

    [Header("Color Settings")]
    [Tooltip("Основные цвета, один из которых будет выбран для всех фигур в сетке.")]
    [SerializeField] private Color[] _baseColors; // Массив возможных базовых цветов



    private List<GameObject> _currentShapes = new List<GameObject>();
    private int _currentBaseColorIndex = -1; // Индекс выбранного базового цвета
    public IReadOnlyList<GameObject> CurrentShapes => _currentShapes.AsReadOnly(); // Доступ только для чтения
    public int CurrentBaseColorIndex => _currentBaseColorIndex;


    // --- Публичные методы ---

    /// <summary>
    /// Генерирует сетку фигур заданного размера.
    /// </summary>
    /// <param name="gridSize">Размер стороны сетки (например, 3 для 3x3).</param>
    /// <param name="puzzleController">Контроллер для инициализации ShapeController.</param>
    public void GenerateGrid(int gridSize, PuzzleController puzzleController)
    {
        ClearGrid();
        _currentBaseColorIndex = -1; // Сбрасываем индекс

        Color chosenBaseColor = Color.white; // Цвет по умолчанию, если массив пуст
        if (_baseColors != null && _baseColors.Length > 0)
        {
            _currentBaseColorIndex = Random.Range(0, _baseColors.Length);
            chosenBaseColor = _baseColors[_currentBaseColorIndex];
        }
        else
        {
            Debug.LogWarning("Массив базовых цветов (_baseColors) не настроен в ShapeGridManager!", this);
        }

        Vector2 startPosition = CalculateStartPosition(gridSize);

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                Vector3 position = new Vector3(
                    startPosition.x + x * _spacing,
                    startPosition.y - y * _spacing,
                    0
                );

                GameObject shapeInstance = Instantiate(_shapePrefab, position, Quaternion.identity, transform);
                int shapeIndex = y * gridSize + x;

                SpriteRenderer sr = shapeInstance.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = chosenBaseColor; // Устанавливаем базовый цвет
                }
                else
                {
                    Debug.LogWarning($"На префабе {_shapePrefab.name} отсутствует SpriteRenderer.", shapeInstance);
                }

                // Инициализируем ShapeController (если он есть на префабе)
                ShapeController shapeController = shapeInstance.GetComponent<ShapeController>();
                if (shapeController != null)
                {
                    // Передаем ссылку на контроллер и индекс фигуры
                    shapeController.Initialize(puzzleController, shapeIndex);
                }
                else
                {
                    Debug.LogWarning($"Префаб {_shapePrefab.name} не содержит компонент ShapeController!", shapeInstance);
                }

                _currentShapes.Add(shapeInstance);
            }
        }
    }

    /// <summary>
    /// Уничтожает все созданные фигуры и очищает список.
    /// </summary>
    public void ClearGrid()
    {
        // Уничтожаем копии, чтобы не было ошибок при выходе из Play Mode в редакторе
        for (int i = _currentShapes.Count - 1; i >= 0; i--)
        {
            if (_currentShapes[i] != null)
            {
                Destroy(_currentShapes[i]);
            }
        }
        _currentShapes.Clear();
    }

    // --- Приватные методы ---

    /// <summary>
    /// Расчет стартовой позиции для центрирования сетки.
    /// </summary>
    private Vector2 CalculateStartPosition(int size)
    {
        float offset = (size - 1) * _spacing * 0.5f;
        return new Vector2(-offset, offset);
    }
}