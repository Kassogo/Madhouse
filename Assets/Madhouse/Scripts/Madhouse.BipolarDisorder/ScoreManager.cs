using UnityEngine;

namespace Madhouse.BipolarDisorder
{
    /// <summary>
    /// Manages the player's score.
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }
        public int _score = 0;
        public event System.Action<int> OnScoreChanged;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        /// <summary>
        /// Updates the score based on whether there is a match and triggers the score change event.
        /// </summary>
        /// <param name="isMatch"></param>
        public void UpdateScore(bool isMatch)
        {
            _score += isMatch ? 1 : -1;
            OnScoreChanged?.Invoke(_score);
        }
    }
}
