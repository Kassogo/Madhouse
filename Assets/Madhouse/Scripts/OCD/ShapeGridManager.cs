using System.Collections.Generic;
using UnityEngine;

public class ShapeGridManager : MonoBehaviour
{
    [Header("Настройки сетки")]
    [SerializeField] private GameObject[] _insectPrefabs; // Массив префабов насекомых
    [SerializeField] private float _spacing = 1.2f;
    [SerializeField] private float _yOffset = -2f; // Смещение сетки по Y (опустить вниз)

    [Header("Настройки цвета")]
    [SerializeField] private Color _fixedColor = Color.white; // Фиксированный цвет для всех насекомых

    private List<GameObject> _currentShapes = new List<GameObject>();
    public IReadOnlyList<GameObject> CurrentShapes => _currentShapes.AsReadOnly();

    public void GenerateGrid(int gridSize, PuzzleController puzzleController)
    {
        ClearGrid();

        if (_insectPrefabs == null || _insectPrefabs.Length == 0)
        {
            Debug.LogError("Массив префабов насекомых (_insectPrefabs) не настроен или пуст в ShapeGridManager!", this);
            return;
        }

        GameObject chosenInsectPrefab = _insectPrefabs[Random.Range(0, _insectPrefabs.Length)];

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

                GameObject insectInstance = Instantiate(chosenInsectPrefab, position, Quaternion.identity, transform);
                int shapeIndex = y * gridSize + x;

                SpriteRenderer sr = insectInstance.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = _fixedColor; // Устанавливаем фиксированный цвет
                }
                else
                {
                    Debug.LogWarning($"На префабе {chosenInsectPrefab.name} отсутствует SpriteRenderer.", insectInstance);
                }

                ShapeController shapeController = insectInstance.GetComponent<ShapeController>();
                if (shapeController != null)
                {
                    shapeController.Initialize(puzzleController, shapeIndex);
                }
                else
                {
                    Debug.LogWarning($"Префаб {chosenInsectPrefab.name} не содержит компонент ShapeController!", insectInstance);
                }

                _currentShapes.Add(insectInstance);
            }
        }
    }

    public void ClearGrid()
    {
        for (int i = _currentShapes.Count - 1; i >= 0; i--)
        {
            if (_currentShapes[i] != null)
            {
                Destroy(_currentShapes[i]);
            }
        }
        _currentShapes.Clear();
    }

    private Vector2 CalculateStartPosition(int size)
    {
        float offset = (size - 1) * _spacing * 0.5f;
        return new Vector2(-offset, offset + _yOffset); // Применяем смещение по Y
    }
}