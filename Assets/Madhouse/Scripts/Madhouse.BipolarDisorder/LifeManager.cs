using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace Madhouse.BipolarDisorder
{
    public class LifeManager : MonoBehaviour
    {
        public static LifeManager Instance { get; private set; }
        public int Lives => _lives;

        [SerializeField] private int _startLives = 3;
        private int _lives;
        public event Action<int> OnLivesChanged;

        private void Awake()
        {
            Time.timeScale = 1;
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            _lives = _startLives;
            OnLivesChanged?.Invoke(Lives);
        }

        private void OnEnable()
        {
            _lives = _startLives;
            OnLivesChanged?.Invoke(Lives);
        }

        public void LoseLife()
        {
            _lives--;
            OnLivesChanged?.Invoke(Lives);

            if (Lives <= 0)
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            FindObjectOfType<BackgroundManager>().OnLifeLost();
            if (GameOverPanel.Instance != null)
            {
                GameOverPanel.Instance.ShowGameOver();
            }
            else
            {
                Debug.LogError("GameOverPanel.Instance is null!");
            }
        }

        public void ResetLives()
        {
            _lives = _startLives;
            OnLivesChanged?.Invoke(Lives);
        }
    }
}