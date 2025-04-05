using UnityEngine;
using TMPro; 

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _scoreText;

    private int _score = 0; 

    public int CurrentScore => _score;

    private void Awake()
    {
        // Проверка, не создан ли уже экземпляр ScoreManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // Если экземпляр уже существует, уничтожаем этот дубликат
            Debug.LogWarning("Найден еще один экземпляр ScoreManager. Уничтожаем дубликат.", this.gameObject);
            Destroy(gameObject);
            return; 
        }
    }

    private void Start()
    {
        // Устанавливаем начальное значение текста при запуске
        UpdateScoreText();
    }

    /// <summary>
    /// Добавляет указанное количество очков к текущему счету.
    /// </summary>
    /// <param name="pointsToAdd">Количество очков для добавления.</param>
    public void AddScore(int pointsToAdd)
    {
        _score += pointsToAdd;
        // Debug.Log($"Добавлено {pointsToAdd} очков. Текущий счет: {_score}"); // Для отладки
        UpdateScoreText();
    }

    /// <summary>
    /// Сбрасывает счет до нуля.
    /// </summary>
    public void ResetScore()
    {
        _score = 0;
        // Debug.Log("Счет сброшен."); // Для отладки
        UpdateScoreText();
    }

    /// <summary>
    /// Обновляет текстовое поле счета.
    /// </summary>
    private void UpdateScoreText()
    {
        // Проверка, назначено ли текстовое поле в инспекторе
        if (_scoreText != null)
        {
            _scoreText.text = "Score: " + _score;
        }
        else
        {
            Debug.LogWarning("ScoreText не назначен в ScoreManager!", this.gameObject);
        }
    }
}