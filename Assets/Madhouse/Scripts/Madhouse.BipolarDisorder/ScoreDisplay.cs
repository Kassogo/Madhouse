using UnityEngine;
using TMPro;

namespace Madhouse.BipolarDisorder
{
    /// <summary>
    /// Displays the current score on the screen.
    /// </summary>
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void Awake()
        {
            if (_scoreText == null && !TryGetComponent(out _scoreText))
            {
                Debug.LogError("ScoreDisplay: TextMeshProUGUI is not assigned and not found!");
            }
        }

        private void Start()
        {
            if (ScoreManager.Instance == null) return;

            ScoreManager.Instance.OnScoreChanged += UpdateScoreText;
            UpdateScoreText(ScoreManager.Instance.Score); // Используем публичное свойство Score
        }

        private void OnDestroy()
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.OnScoreChanged -= UpdateScoreText;
            }
        }

        private void UpdateScoreText(int score)
        {
            if (_scoreText != null)
            {
                _scoreText.text = $"Score: {score}";
            }
        }
    }
}