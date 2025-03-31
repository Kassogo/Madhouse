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

            if (ScoreManager.Instance == null)
            {
                return;
            }

            ScoreManager.Instance.OnScoreChanged += UpdateScoreText;
            UpdateScoreText(ScoreManager.Instance._score); 
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