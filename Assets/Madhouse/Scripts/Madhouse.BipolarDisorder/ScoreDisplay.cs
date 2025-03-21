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

        private void Start()
        {
            Debug.Log("ScoreDisplay Start вызван");

            if (ScoreManager.Instance == null)
            {
                Debug.LogError("ScoreManager.Instance is null! ScoreDisplay не сможет подписаться на события.");
                return;
            }

            ScoreManager.Instance.OnScoreChanged += UpdateScoreText;
            UpdateScoreText(ScoreManager.Instance._score); // Устанавливаем актуальный счет
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
            Debug.Log($"ScoreDisplay получил новый счет: {score}");
            if (_scoreText != null)
            {
                _scoreText.text = $"Score: {score}";
            }
        }
    }
}