using System.Collections.Generic;
using UnityEngine;

public class UniqueShapeModifier : MonoBehaviour
{
    [Header("Настройки отличий")]
    [SerializeField] private float _minSizeDifference = 0.1f; // Минимальная разница в размере насекомого
    [SerializeField] private float _maxSizeDifferenceMultiplier = 2.0f; // Максимальный множитель размера насекомого
    [SerializeField] private float _maxRotationDifference = 15.0f; // Максимальный угол поворота насекомого для мелкого поворота
    [SerializeField] private Sprite _uniqueInsectSprite; // Уникальный спрайт насекомого (например, жук)

    /// <summary>
    /// Выбирает случайное насекомое из списка и применяет к нему уникальное отличие.
    /// </summary>
    /// <param name="shapes">Список насекомых для модификации.</param>
    /// <returns>Индекс модифицированного (уникального) насекомого или -1, если список пуст.</returns>
    public int ApplyUniqueModification(IReadOnlyList<GameObject> shapes)
    {
        if (shapes == null || shapes.Count == 0)
        {
            Debug.LogWarning("Список насекомых для модификации пуст.");
            return -1;
        }

        int uniqueShapeIndex = Random.Range(0, shapes.Count);
        GameObject uniqueShape = shapes[uniqueShapeIndex];

        if (uniqueShape == null)
        {
            Debug.LogError($"Выбранное насекомое с индексом {uniqueShapeIndex} оказалось null!");
            return -1;
        }

        // Сбрасываем предыдущие модификации
        ResetModification(uniqueShape);

        // Выбираем случайный тип отличия
        int differenceType = Random.Range(0, 4); // Увеличиваем до 4, чтобы учесть новый кейс

        switch (differenceType)
        {
            case 0: // Изменение размера
                float scaleModifier = 1 + Random.Range(_minSizeDifference, _minSizeDifference * _maxSizeDifferenceMultiplier);
                uniqueShape.transform.localScale = Vector3.one * scaleModifier;
                break;

            case 1: // Замена спрайта (например, жук вместо бабочки)
                SpriteRenderer sr = uniqueShape.GetComponent<SpriteRenderer>();
                if (sr != null && _uniqueInsectSprite != null)
                {
                    sr.sprite = _uniqueInsectSprite;
                }
                else
                {
                    Debug.LogWarning("Не удалось заменить спрайт: отсутствует SpriteRenderer или _uniqueInsectSprite не настроен.", uniqueShape);
                }
                break;

            case 2: // Мелкий поворот
                float rotation = Random.Range(-_maxRotationDifference, _maxRotationDifference);
                if (Mathf.Approximately(rotation, 0f)) rotation = _maxRotationDifference * Mathf.Sign(Random.Range(-1f, 1f));
                uniqueShape.transform.Rotate(0, 0, rotation);
                break;

            case 3: // Поворот на 90, 180 или 270 градусов
                float[] fixedRotations = { 90f, 180f, 270f };
                float fixedRotation = fixedRotations[Random.Range(0, fixedRotations.Length)];
                uniqueShape.transform.Rotate(0, 0, fixedRotation);
                break;
        }

        return uniqueShapeIndex;
    }

    /// <summary>
    /// Сбрасывает модификации насекомого.
    /// </summary>
    private void ResetModification(GameObject shape)
    {
        shape.transform.localScale = Vector3.one;
        shape.transform.rotation = Quaternion.identity;
    }
}