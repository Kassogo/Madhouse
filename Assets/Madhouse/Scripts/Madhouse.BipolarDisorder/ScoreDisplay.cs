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
            _scoreText = GetComponent<TextMeshProUGUI>();
            if (_scoreText == null)
            {
                Debug.LogError("TextMeshProUGUI component not found on this GameObject!");
            }
            else
            {
                Debug.Log("TextMeshProUGUI component found.");
            }
        }

        private void OnEnable()
        {
            Debug.Log("ScoreDisplay OnEnable вызван");
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.OnScoreChanged += UpdateScoreText;
                UpdateScoreText(0); // Устанавливаем начальный текст
            }
        }

        private void OnDisable()
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.OnScoreChanged -= UpdateScoreText;
            }
        }

        private void UpdateScoreText(int _score)
        {
            Debug.Log($"ScoreDisplay получил новый счет: {_score}");
            if (_scoreText != null)
            {
                _scoreText.text = $"Score: {_score}";
            }
        }
    }
}
