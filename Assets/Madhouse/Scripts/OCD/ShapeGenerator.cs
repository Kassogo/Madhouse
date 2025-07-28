using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject shapePrefab; // Префаб фигуры
    [SerializeField] private int gridSize = 3; // Размер сетки (3x3, 4x4 и т.д.)
    [SerializeField] private float spacing = 1.2f; // Расстояние между фигурами

    [Header("Difference Settings")]
    [SerializeField] private Color[] possibleColors; // Возможные цвета для отличия
    [SerializeField] private float minSizeDifference = 0.1f; // Минимальная разница в размере
    [SerializeField] private float maxRotationDifference = 15f; // Макс. угол поворота

    private List<GameObject> currentShapes = new List<GameObject>();
    private int correctShapeIndex = -1;

    void Start()
    {
        GenerateGrid();
    }

    // Генерация сетки фигур
    public void GenerateGrid()
    {
        ClearGrid();

        Vector2 startPosition = CalculateStartPosition(gridSize);

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                Vector3 position = new Vector3(
                    startPosition.x + x * spacing,
                    startPosition.y - y * spacing,
                    0
                );

                GameObject shape = Instantiate(shapePrefab, position, Quaternion.identity, transform);
                shape.GetComponent<ShapeController>().Initialize(this, y * gridSize + x);
                currentShapes.Add(shape);
            }
        }

        SetUniqueShape();
    }

    // Расчет стартовой позиции для центрирования сетки
    private Vector2 CalculateStartPosition(int size)
    {
        float offset = (size - 1) * spacing * 0.5f;
        return new Vector2(-offset, offset);
    }

    // Создание уникальной фигуры
    private void SetUniqueShape()
    {
        if (currentShapes.Count == 0) return;

        correctShapeIndex = Random.Range(0, currentShapes.Count);
        GameObject uniqueShape = currentShapes[correctShapeIndex];

        // Случайный выбор типа отличия
        int differenceType = Random.Range(0, 3);

        switch (differenceType)
        {
            case 0: // Изменение цвета
                uniqueShape.GetComponent<SpriteRenderer>().color =
                    possibleColors[Random.Range(0, possibleColors.Length)];
                break;

            case 1: // Изменение размера
                float scaleModifier = 1 + Random.Range(minSizeDifference, minSizeDifference * 2);
                uniqueShape.transform.localScale *= scaleModifier;
                break;

            case 2: // Поворот
                float rotation = Random.Range(-maxRotationDifference, maxRotationDifference);
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
        if (shapeIndex == correctShapeIndex)
        {
            Debug.Log("Correct!");
            // Здесь вызвать метод успеха
        }
        else
        {
            Debug.Log("Wrong!");
            // Здесь вызвать метод неудачи
        }
    }

    // Для изменения сложности (вызывается из менеджера уровней)
    public void SetDifficulty(int newGridSize)
    {
        gridSize = newGridSize;
        GenerateGrid();
    }
}
