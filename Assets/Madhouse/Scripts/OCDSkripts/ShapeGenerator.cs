using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ShapeGenerator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject _shapePrefab; // Префаб фигуры
    [SerializeField] private int _gridSize = 3; // Размер сетки (3x3, 4x4 и т.д.)
    [SerializeField] private float _spacing = 1.2f; // Расстояние между фигурами

    [Header("Difference Settings")]
    [SerializeField] private Color[] _possibleColors; // Возможные цвета для отличия
    [SerializeField] private float _minSizeDifference = 0.1f; // Минимальная разница в размере
    [SerializeField] private float _maxRotationDifference = 15f; // Макс. угол поворота

    private List<GameObject> currentShapes = new List<GameObject>();
    private int _correctShapeIndex = -1;

    public int CorrectShapeIndex
    {
        get { return _correctShapeIndex; }
    }





    void Start()
    {
        GenerateGrid();
    }

    // Генерация сетки фигур
    public void GenerateGrid()
    {
        ClearGrid();

        Vector2 startPosition = CalculateStartPosition(_gridSize);

        for (int y = 0; y < _gridSize; y++)
        {
            for (int x = 0; x < _gridSize; x++)
            {
                Vector3 position = new Vector3(
                    startPosition.x + x * _spacing,
                    startPosition.y - y * _spacing,
                    0
                );

                GameObject shape = Instantiate(_shapePrefab, position, Quaternion.identity, transform);
                shape.GetComponent<ShapeController>().Initialize(this, y * _gridSize + x);
                currentShapes.Add(shape);
            }
        }

        SetUniqueShape();
    }

    // Расчет стартовой позиции для центрирования сетки
    private Vector2 CalculateStartPosition(int size)
    {
        float offset = (size - 1) * _spacing * 0.5f;
        return new Vector2(-offset, offset);
    }

    // Создание уникальной фигуры
    private void SetUniqueShape()
    {
        if (currentShapes.Count == 0) return;

        _correctShapeIndex = Random.Range(0, currentShapes.Count);
        GameObject uniqueShape = currentShapes[_correctShapeIndex];

        // Случайный выбор типа отличия
        int differenceType = Random.Range(0, 3);

        switch (differenceType)
        {
            case 0: // Изменение цвета
                uniqueShape.GetComponent<SpriteRenderer>().color =
                    _possibleColors[Random.Range(0, _possibleColors.Length)];
                break;

            case 1: // Изменение размера
                float scaleModifier = 1 + Random.Range(_minSizeDifference, _minSizeDifference * 2);
                uniqueShape.transform.localScale *= scaleModifier;
                break;

            case 2: // Поворот
                float rotation = Random.Range(-_maxRotationDifference, _maxRotationDifference);
                uniqueShape.transform.Rotate(0, 0, rotation);
                break;
        }
    }

    // Очистка предыдущей сетки
    private void ClearGrid()
    {
        foreach (GameObject shape in currentShapes)
        {
            Destroy(shape);
        }
        currentShapes.Clear();
    }

    // Вызывается при клике на фигуру
    public void OnShapeClicked(int shapeIndex)
    {
        if (shapeIndex == _correctShapeIndex)
        {
            Debug.Log("Correct!");
            // Здесь вызвать метод успеха
            ScoreManager.Instance.SetScore(1);
        }
        {
            Debug.Log("Wrong!");
            // Здесь вызвать метод неудачи
        }
    }

    // Для изменения сложности (вызывается из менеджера уровней)
    public void SetDifficulty(int newGridSize)
    {
        _gridSize = newGridSize;
        GenerateGrid();
    }

}
