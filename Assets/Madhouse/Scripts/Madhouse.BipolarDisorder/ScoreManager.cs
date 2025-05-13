using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace Madhouse.BipolarDisorder
{
    /// <summary>
    /// Manages the player's score.
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }
        public int Score => _score; // Публичное свойство для доступа к счету

        private int _score = 0;
        public event Action<int> OnScoreChanged;

        private void Awake()
        {
            InitializeSingleton();
        }

        /// <summary>
        /// Ensures this class follows the Singleton pattern.
        /// </summary>
        private void InitializeSingleton()
        {
            if (Instance == null)
            {
                Instance = this;
                // DontDestroyOnLoad(gameObject); // Уберите эту строку, если ScoreManager должен сбрасываться при перезагрузке сцены
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            _score = 0;
            OnScoreChanged?.Invoke(_score);
        }

        /// <summary>
        /// Updates the score based on whether there is a match and triggers the score change event.
        /// </summary>
        /// <param name="isMatch"></param>
        public void UpdateScore(bool isMatch)
        {
            _score += isMatch ? 1 : -1;
            OnScoreChanged?.Invoke(_score);

            FindObjectOfType<BackgroundManager>()?.UpdateScore(_score);
        }

        public void ResetScore()
        {
            _score = 0;
            OnScoreChanged?.Invoke(_score);
        }
    }
}