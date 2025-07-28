using UnityEngine;
using System.Collections.Generic;

public class PuzzleController : MonoBehaviour
{
    [Header("Core Settings")]
    [SerializeField] private int _gridSize = 3;

    [Header("Component References")]
    [SerializeField] private ShapeGridManager _gridManager;
    [SerializeField] private UniqueShapeModifier _shapeModifier;
    [SerializeField] private ShapeInteractionHandler _interactionHandler;

    void Start()
    {
        if (_gridManager == null || _shapeModifier == null || _interactionHandler == null)
        {
            Debug.LogError("Не все компоненты назначены в PuzzleController!");
            return;
        }
        GenerateNewPuzzle();
    }

    public void GenerateNewPuzzle()
    {
        // 1. Генерируем сетку (ShapeGridManager выберет и применит базовый цвет)
        _gridManager.GenerateGrid(_gridSize, this);

        // 2. Получаем список созданных фигур
        IReadOnlyList<GameObject> currentShapes = _gridManager.CurrentShapes;

        // --- Изменено: Получаем индекс базового цвета ---
        int baseColorIndex = _gridManager.CurrentBaseColorIndex;
        // --- Конец измененного ---

        // 3. Применяем уникальную модификацию, передавая индекс базового цвета
        int correctShapeIndex = _shapeModifier.ApplyUniqueModification(currentShapes, baseColorIndex); // <--- Передаем индекс

        // 4. Настраиваем обработчик кликов
        if (correctShapeIndex != -1)
        {
            _interactionHandler.Setup(correctShapeIndex);
        }
        else
        {
            Debug.LogError("Не удалось создать уникальную фигуру!");
        }
    }

    public void OnShapeClicked(int shapeIndex)
    {
        _interactionHandler.HandleShapeClick(shapeIndex);
    }

    public void OnCorrectShapeSelected()
    {
        Debug.Log("PuzzleController: Correct shape selected! Proceeding...");
        Invoke(nameof(GenerateNewPuzzle), 1.0f);
    }

    public void OnWrongShapeSelected()
    {
        Debug.Log("PuzzleController: Wrong shape selected!");
        Invoke(nameof(GenerateNewPuzzle), 1.0f);
    }

    public void SetDifficulty(int newGridSize)
    {
        _gridSize = Mathf.Max(2, newGridSize);
        GenerateNewPuzzle();
    }
}