using UnityEngine;

public class ShapeInteractionHandler : MonoBehaviour
{
    private int _correctShapeIndex = -1;
    private bool _canInteract = true; // Флаг, чтобы избежать повторных кликов после выбора

    /// <summary>
    /// Устанавливает индекс правильной фигуры для текущего раунда.
    /// </summary>
    /// <param name="correctIndex">Индекс уникальной фигуры.</param>
    public void Setup(int correctIndex)
    {
        _correctShapeIndex = correctIndex;
        _canInteract = true; // Разрешаем взаимодействие для нового раунда
        Debug.Log($"Правильный индекс установлен: {_correctShapeIndex}"); // Для отладки
    }

    /// <summary>
    /// Обрабатывает клик по фигуре с указанным индексом.
    /// </summary>
    /// <param name="clickedIndex">Индекс фигуры, по которой кликнули.</param>
    public void HandleShapeClick(int clickedIndex)
    {
        if (!_canInteract) return; // Не обрабатываем клики, если взаимодействие запрещено

        if (clickedIndex == _correctShapeIndex)
        {
            Debug.Log("Correct!");
            _canInteract = false; // Запрещаем дальнейшие клики до следующего раунда
            // Увеличиваем счет (предполагаем, что SetScore добавляет очки)
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(1);
            }
            else
            {
                Debug.LogWarning("ScoreManager не найден!");
            }

            // Здесь можно добавить вызов события "Успех" для PuzzleController
            // Например: public event Action OnCorrectChoice; OnCorrectChoice?.Invoke();
            FindObjectOfType<PuzzleController>()?.OnCorrectShapeSelected(); // Пример прямого вызова
        }
        else
        {
            Debug.Log($"Wrong! Clicked: {clickedIndex}, Correct: {_correctShapeIndex}");
            _canInteract = false; // Запрещаем дальнейшие клики до следующего раунда
                                  // Здесь можно добавить вызов события "Ошибка" или уменьшить очки/жизни
                                  // Например: public event Action OnWrongChoice; OnWrongChoice?.Invoke();
            FindObjectOfType<PuzzleController>()?.OnWrongShapeSelected(); // Пример прямого вызова
        }
    }
}