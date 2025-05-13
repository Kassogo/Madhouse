using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace Madhouse.BipolarDisorder
{
    /// <summary>
    /// Manages player lives.
    /// </summary>
    public class LifeManager : MonoBehaviour
    {
        public static LifeManager Instance { get; private set; }
        public int Lives => _lives;

        [SerializeField] private int _startLives = 3;
        private int _lives;
        public event Action<int> OnLivesChanged;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                // DontDestroyOnLoad(gameObject); // Убедитесь, что этой строки нет или она закомментирована
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
            // Дополнительно сбрасываем жизни при включении объекта (после перезагрузки сцены)
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
            // Показываем панель Game Over
            if (GameOverPanel.Instance != null)
            {
                GameOverPanel.Instance.ShowGameOver();
            }
            else
            {
                Debug.LogError("GameOverPanel.Instance is null!");
                // Можно добавить загрузку сцены Game Over, если она отдельная
            }
        }

        public void ResetLives() // Добавляем метод для явного сброса жизней
        {
            _lives = _startLives;
            OnLivesChanged?.Invoke(Lives);
        }
    }
}