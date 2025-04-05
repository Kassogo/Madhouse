using System.Collections.Generic;
using UnityEngine;

public class UniqueShapeModifier : MonoBehaviour
{
    [Header("Difference Settings")]
    [Tooltip("Уникальные цвета. Должны соответствовать по индексам массиву _baseColors в ShapeGridManager!")]
    [SerializeField] private Color[] _uniqueColors;           // Возможнве цвета отличий
    [SerializeField] private float _minSizeDifference = 0.1f; // Мин. разница в размере 
    [SerializeField] private float _maxSizeDifferenceMultiplier = 2.0f; // Множитель для макс. разницы размера
    [SerializeField] private float _maxRotationDifference = 15.0f; // Макс. угол поворота 

    /// <summary>
    /// Выбирает случайную фигуру из списка и применяет к ней уникальное отличие.
    /// </summary>
    /// <param name="shapes">Список фигур для модификации.</param>
    /// <returns>Индекс модифицированной (уникальной) фигуры или -1, если список пуст.</returns>
    /// /// <param name="baseColorIndex">Индекс базового цвета, выбранного для этой сетки.</param>
    public int ApplyUniqueModification(IReadOnlyList<GameObject> shapes, int baseColorIndex)
    {
        if (shapes == null || shapes.Count == 0)
        {
            Debug.LogWarning("Список фигур для модификации пуст.");
            return -1;
        }

        int uniqueShapeIndex = Random.Range(0, shapes.Count);
        GameObject uniqueShape = shapes[uniqueShapeIndex];

        if (uniqueShape == null)
        {
            Debug.LogError($"Выбранная фигура с индексом {uniqueShapeIndex} оказалась null!");
            return -1; 
        }


        // Сбрасываем предыдущие модификации (кроме цвета)
        ResetModification(uniqueShape);

        int differenceType = Random.Range(0, 3);

        switch (differenceType)
        {
            case 0: // Изменение цвета
                SpriteRenderer sr = uniqueShape.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    
                    if (_uniqueColors != null && baseColorIndex >= 0 && baseColorIndex < _uniqueColors.Length)
                    {
                        sr.color = _uniqueColors[baseColorIndex]; // Берем цвет по индексу базового
                    }
                    else
                    {
                        Debug.LogWarning($"Не удалось применить уникальный цвет: массив _uniqueColors не настроен или baseColorIndex ({baseColorIndex}) некорректен.", this);
                        // Можно добавить логику выбора другого типа отличия, если цвет не удался
                    }
                    
                }
                else
                {
                    Debug.LogWarning("Не удалось изменить цвет: отсутствует SpriteRenderer.", uniqueShape);
                }
                break;

            case 1: // Изменение размера
                float scaleModifier = 1 + Random.Range(_minSizeDifference, _minSizeDifference * _maxSizeDifferenceMultiplier);
                uniqueShape.transform.localScale = Vector3.one * scaleModifier;
                break;

            case 2: // Поворот
                float rotation = Random.Range(-_maxRotationDifference, _maxRotationDifference);
                if (Mathf.Approximately(rotation, 0f)) rotation = _maxRotationDifference * Mathf.Sign(Random.Range(-1f, 1f));
                uniqueShape.transform.Rotate(0, 0, rotation);
                break;
        }

        return uniqueShapeIndex;
    }

    /// <summary>
    /// Сбрасывает модификации фигуры (кроме базового цвета).
    /// </summary>
    private void ResetModification(GameObject shape)
    {

        shape.transform.localScale = Vector3.one;
        shape.transform.rotation = Quaternion.identity;
    }
}