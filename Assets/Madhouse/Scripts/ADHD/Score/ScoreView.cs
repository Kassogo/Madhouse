using UnityEngine;
using TMPro;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Вьюшка для показа счёта.
    /// </summary>
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        /// <summary>
        /// Показ счёта.
        /// </summary>
        /// <param name="score"></param>
        public void ShowScore(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}
