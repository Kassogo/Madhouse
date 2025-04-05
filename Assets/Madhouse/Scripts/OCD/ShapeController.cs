using UnityEngine;

public class ShapeController : MonoBehaviour
{
    private PuzzleController _puzzleController;
    private int _shapeIndex;
    private Collider2D _collider; // Для оптимизации

    void Awake()
    {
        _collider = GetComponent<Collider2D>(); // Получаем коллайдер один раз
        if (_collider == null)
        {
            Debug.LogWarning("ShapeController не имеет Collider2D, клики не будут работать!", this);
        }
    }

    /// <summary>
    /// Инициализация фигуры контроллером.
    /// </summary>
    /// <param name="controller">Ссылка на главный контроллер.</param>
    /// <param name="index">Индекс этой фигуры в сетке.</param>
    public void Initialize(PuzzleController controller, int index)
    {
        _puzzleController = controller;
        _shapeIndex = index;
    }

    // Вызывается Unity при клике мышкой (требует Collider на объекте)
    private void OnMouseDown()
    {
        if (_puzzleController != null)
        {
            // Сообщаем контроллеру, что по этой фигуре кликнули
            _puzzleController.OnShapeClicked(_shapeIndex);
        }
        else
        {
            Debug.LogWarning("PuzzleController не инициализирован для этой фигуры!", this);
        }
    }
}
