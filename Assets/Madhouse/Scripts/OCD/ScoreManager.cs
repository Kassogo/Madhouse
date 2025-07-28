using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _scoreText;

    private int _score = 0;

    public int CurrentScore => _score;

    // Событие, которое срабатывает при достижении 50 очков
    public event System.Action OnScoreReached;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Найден еще один экземпляр ScoreManager. Уничтожаем дубликат.", this.gameObject);
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int pointsToAdd)
    {
        _score += pointsToAdd;
        UpdateScoreText();
        if (_score >= 50)
        {
            OnScoreReached?.Invoke();  // Вызываем событие при достижении 50 очков
        }
    }

    public void ResetScore()
    {
        _score = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
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